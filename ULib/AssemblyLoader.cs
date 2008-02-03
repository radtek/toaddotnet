/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte — Tous droits réservés.

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
  ----------------------------------------------------------------------------- 
 
  Ce programme est un logiciel libre ; vous pouvez le redistribuer ou le
  modifier suivant les termes de la “GNU General Public License” telle que
  publiée par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ultérieure.
  
  Ce programme est distribué dans l’espoir qu’il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans même la garantie implicite de COMMERCIALISABILITÉ
  ni d’ADÉQUATION À UN OBJECTIF PARTICULIER. Consultez la Licence Générale
  Publique GNU pour plus de détails.
  
  Vous devriez avoir reçu une copie de la Licence Générale Publique GNU avec
  ce programme ; si ce n’est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using PluginTypes;

namespace ULib
{
    public class AssemblyLoader
    {
        #region Variables
        //message d'erreur
        private string errorMessage = null;
        //référence vers la fenêtre principale
        private Form form = null;
        //référence vers le menu de la fenêtre principale
        private MenuStrip menuStrip = null;
        //référence vers le tabControl de droite
        private TabControl tabControl = null;
        //référence vers le tabControl de gauche
        private TabControl tabControlLeft = null;
        //référence vers le plugEvent
        private PlugEvent plugEvent = null;
        //assembly qui va être charger plus tard
        private Assembly asm = null;
        //information sur l'assembly
        private ModuleInfoAttribute moduleInfo = null;
        //
        public CultureInfo CurrentCulture = null;
        private ResourceManager m_ResourceManager = new ResourceManager("Membs.Taduction", System.Reflection.Assembly.GetExecutingAssembly());
        #endregion

        #region Constructeur
        public AssemblyLoader(Form form, MenuStrip menuStrip, TabControl tabControl, TabControl tabControlLeft, PlugEvent plugEvent)
        {
            CurrentCulture = new CultureInfo(Config.GetInnerTextValue(Config.Load(), "/alf-solution/AppConfig/lang"));
            System.Threading.Thread.CurrentThread.CurrentUICulture = CurrentCulture;
            
            this.form = form;
            this.menuStrip = menuStrip;
            this.tabControl = tabControl;
            this.tabControlLeft = tabControlLeft;
            this.plugEvent = plugEvent;
        }
        #endregion
        #region Load
        public bool Load(string filepath)
        {
            //on vérifie que l'assembly à charger existe
            if (!File.Exists(filepath))
            {
                errorMessage = "Le fichier n'existe pas.";
                return false;
            }
            //on charge l'assemby en prenant soin de s'assurer qu'elle est valide avec un try catch
            try
            {
                asm = Assembly.LoadFrom(filepath);
            }
            catch
            {
                errorMessage =
                    "Le fichier assembly est corrompu. Le fichier doit être une dll .net afin d'être chargé correctement.";
                return false;
            }
            //on récupère tous les types contenus dans l'assembly
            //type = class, enum, interface, delegate et struct
            Type[] types = asm.GetTypes();
            //on extrait l'info de l'assembly en récupérant l'attribut ModuleInfo
            ExtractInfo();

            //ce booléan nous informe si oui ou non on a trouvé au moins une classe qui implèmente
            //l'interface IFormAddOn ou IMenuAddOn
            bool foundInterface = false;            
            if (moduleInfo != null)
            {
                //if (MessageBox.Show("On ne dispose d'aucune information sur ce module. Voulez-vous continuer le chargement ?", "Aucune Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                //    return false;                    
                //2 - Vérifier si on a la licence
                string xmlData = Config.Load();
                string key = Config.GetInnerTextValue(xmlData, "//alf-solution/plugins/" + moduleInfo.Name + "/key");
                if (string.IsNullOrEmpty(key))
                {
                    key = Utils.GetKey(moduleInfo.Name, Config.PublicKey(), Config.Nom(), Config.Email());
                    xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "key", key);
                    Config.Save(xmlData);
                }

                // Vérifier si la clé est bonne
                Vigenere oVigenere = new Vigenere(System.Environment.MachineName.ToLower());
                string tmpPluginName = Config.PublicKey();
                if (moduleInfo.Name.Length < 25)
                {
                    tmpPluginName = moduleInfo.Name +
                                    tmpPluginName.Substring(tmpPluginName.Length - (25 - moduleInfo.Name.Length));
                }
                if (oVigenere.Encrypt(tmpPluginName) == key)
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        Type IFormAddOnType = types[i].GetInterface("IFormAddOn");                        
                        Type IMenuAddOnType = types[i].GetInterface("IMenuAddOn");
                        Type ITabPageAddOnType = types[i].GetInterface("ITabPageAddOn");
                        Type ITabPageLeftAddOnType = types[i].GetInterface("ITabPageLeftAddOn");
                        Type IGroupBoxAddOnType = types[i].GetInterface("IGroupBoxAddOn");

                        //si notre type (une classe ici) implémente l'une de ces deux interfaces alosr...
                        object obj = null;
                        if (IFormAddOnType != null)
                        {
                            //1 - instancier la classe
                            if (obj == null)
                                obj = asm.CreateInstance(types[i].FullName);
                            IFormAddOn formAddOn = obj as IFormAddOn;
                            //2 - invoker la méthode d'installation
                            formAddOn.Install(form);
                            foundInterface = true;
                        }
                        if (IMenuAddOnType != null)
                        {
                            //1 - instancier la classe
                            if (obj == null)                            
                                obj = asm.CreateInstance(types[i].FullName);
                            IMenuAddOn menuAddOn = obj as IMenuAddOn;
                            //2 - invoker la méthode d'installation
                            menuAddOn.Install(menuStrip);
                            menuAddOn.EventPlug(this.plugEvent);
                            foundInterface = true;
                        }
                        if (ITabPageAddOnType != null)
                        {
                            //1 - instancier la classe
                            if (obj == null)
                            obj = asm.CreateInstance(types[i].FullName);
                            ITabPageAddOn tabPageAddOn = obj as ITabPageAddOn;
                            //2 - invoker la méthode d'installation
                            tabPageAddOn.Install(tabControl);
                            tabPageAddOn.EventPlug(this.plugEvent);
                            foundInterface = true;
                        }
                        if (ITabPageLeftAddOnType != null)
                        {
                            //1 - instancier la classe
                            if (obj == null)                            
                                obj = asm.CreateInstance(types[i].FullName);
                            ITabPageLeftAddOn tabPageLeftAddOn = obj as ITabPageLeftAddOn;
                            //2 - invoker la méthode d'installation                    
                            tabPageLeftAddOn.Install(tabControlLeft);
                            tabPageLeftAddOn.EventPlug(this.plugEvent);
                            foundInterface = true;
                        }
                        if (IGroupBoxAddOnType != null)
                        {
                            //1 - instancier la classe
                            if (obj == null)
                                obj = asm.CreateInstance(types[i].FullName);
                            IGroupBoxAddOn groupBoxAddOn = obj as IGroupBoxAddOn;

                            //3 - invoker la méthode d'installation
                            TabPage tp = null;
                            foreach (TabPage tabPage in tabControl.TabPages)
                            {
                                if (tabPage.Text == m_ResourceManager.GetString("Fiche"))
                                    tp = tabPage;
                            }

                            if (tp == null)
                            {

                                tp = new TabPage(m_ResourceManager.GetString("Fiche"));
                                tp.AutoScroll = true;

                                // Add the new tab page to the TabControl of the main window's application
                                tabControl.TabPages.Add(tp);                                
                            }
                            groupBoxAddOn.Install(tp);
                            groupBoxAddOn.EventPlug(this.plugEvent);
                            object[] VersionInfo = asm.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
                            if (VersionInfo.Length > 0)
                            {
                                string version = ((AssemblyFileVersionAttribute) VersionInfo[0]).Version;
                                string versionDll =
                                    Config.GetInnerTextValue(xmlData, "//alf-solution/plugins/" + moduleInfo.Name + "/version");
                                if (string.IsNullOrEmpty(versionDll) || !versionDll.Equals(versionDll))
                                {
                                    //xmlData = Config.SetValue(xmlData, "plugins", moduleInfo.Name, "");
                                    xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "version", version);
                                    Config.Save(xmlData);
                                }

                                Console.Out.WriteLine(version);
                            }
                            foundInterface = true;
                        }
                    }
                }
                else
                {
                    errorMessage = string.Format("Votre clé est invalide pour le module {0}", moduleInfo.Name);
                    return false;
                }
            }
            else
            {
                foundInterface = false;
                //ModuleInfoForm moduleInfoForm = new ModuleInfoForm();
                //moduleInfoForm.setInfo(moduleInfo);
                //if (moduleInfoForm.ShowDialog() == DialogResult.No) return false;
                //string xmlData = Config.Load();
                //xmlData = Config.SetValue(xmlData, "plugins", moduleInfo.Name, "");
                //xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "Path", filepath);
                //xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "Author", moduleInfo.Author);
                //xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "Language", moduleInfo.Language);
                //xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "Description", moduleInfo.Description);
                //Config.Save(xmlData);
            }

            if (foundInterface)
            {                
                return true;
            }
            else
            {
                errorMessage = "L'assemby spécifié ne contient aucun module.";
                return false;
            }

        }

        
        #endregion
        #region ExtractInfo
        private void ExtractInfo()
        {
            object[] moduleInfoArray = asm.GetCustomAttributes(typeof(ModuleInfoAttribute), false);
            if (moduleInfoArray.Length != 0) moduleInfo = (ModuleInfoAttribute)moduleInfoArray[0];
        }
        #endregion
        #region GetErrorMessage
        public string GetErrorMessage() { return errorMessage; } 
        #endregion
    }
}