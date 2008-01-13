using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Membs;
using PluginTypes;
using ToadDotNet;

namespace PlugIn
{
    class AssemblyLoader
    {
        #region Variables
        //message d'erreur
        private string errorMessage = null;
        //r�f�rence vers la fen�tre principale
        private Form form = null;
        //r�f�rence vers le menu de la fen�tre principale
        private MenuStrip menuStrip = null;
        //r�f�rence vers le tabControl de droite
        private TabControl tabControl = null;
        //r�f�rence vers le tabControl de gauche
        private TabControl tabControlLeft = null;
        //r�f�rence vers le plugEvent
        private PlugEvent plugEvent = null;
        //assembly qui va �tre charger plus tard
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
            CurrentCulture = new CultureInfo(Config.GetInnerTextValue(Config.Load(), "/membs/AppConfig/lang"));
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
            //on v�rifie que l'assembly � charger existe
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
                    "Le fichier assembly est corrompu. Le fichier doit �tre une dll .net afin d'�tre charg� correctement.";
                return false;
            }
            //on r�cup�re tous les types contenus dans l'assembly
            //type = class, enum, interface, delegate et struct
            Type[] types = asm.GetTypes();
            //on extrait l'info de l'assembly en r�cup�rant l'attribut ModuleInfo
            ExtractInfo();

            //ce bool�an nous informe si oui ou non on a trouv� au moins une classe qui impl�mente
            //l'interface IFormAddOn ou IMenuAddOn
            bool foundInterface = false;            
            if (moduleInfo != null)
            {
                //if (MessageBox.Show("On ne dispose d'aucune information sur ce module. Voulez-vous continuer le chargement ?", "Aucune Information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                //    return false;
            

                    

                    //2 - V�rifier si on a la licence
                    string xmlData = Config.Load();
                    string key = Config.GetInnerTextValue(xmlData, "//membs/plugins/" + moduleInfo.Name + "/key");
                    if (string.IsNullOrEmpty(key))
                    {
                        key = Utils.GetKey(moduleInfo.Name, Config.PublicKey(), Config.Nom(), Config.Email());
                        xmlData = Config.SetValue(xmlData, "plugins/" + moduleInfo.Name, "key", key);
                        Config.Save(xmlData);
                    }

                    // V�rifier si la cl� est bonne
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
                            //si notre type (une classe ici) impl�mente l'une de ces deux interfaces alosr...
                            if (IFormAddOnType != null)
                            {
                                //1 - instancier la classe
                                object o = asm.CreateInstance(types[i].FullName);
                                IFormAddOn formAddOn = o as IFormAddOn;
                                //2 - invoker la m�thode d'installation
                                formAddOn.Install(form);
                                foundInterface = true;
                            }
                            if (IMenuAddOnType != null)
                            {
                                //1 - instancier la classe
                                object o = asm.CreateInstance(types[i].FullName);
                                IMenuAddOn menuAddOn = o as IMenuAddOn;
                                //2 - invoker la m�thode d'installation
                                menuAddOn.Install(menuStrip);
                                foundInterface = true;
                            }
                            if (ITabPageAddOnType != null)
                            {
                                //1 - instancier la classe
                                object o = asm.CreateInstance(types[i].FullName);
                                ITabPageAddOn tabPageAddOn = o as ITabPageAddOn;
                                //2 - invoker la m�thode d'installation
                                tabPageAddOn.Install(tabControl);
                                tabPageAddOn.EventPlug(this.plugEvent);
                                foundInterface = true;
                            }
                            if (ITabPageLeftAddOnType != null)
                            {
                                //1 - instancier la classe
                                object o = asm.CreateInstance(types[i].FullName);
                                ITabPageLeftAddOn tabPageLeftAddOn = o as ITabPageLeftAddOn;
                                //2 - invoker la m�thode d'installation                    
                                tabPageLeftAddOn.Install(tabControlLeft);
                                tabPageLeftAddOn.EventPlug(this.plugEvent);
                                foundInterface = true;
                            }
                            if (IGroupBoxAddOnType != null)
                            {
                                //1 - instancier la classe
                                object o = asm.CreateInstance(types[i].FullName);
                                IGroupBoxAddOn groupBoxAddOn = o as IGroupBoxAddOn;

                                //3 - invoker la m�thode d'installation
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
                                    // Set automatic resizing of the UserControl
                                    //tabControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
                                    //tabControl.Height = tp.Height - 10;
                                    //tabControl.Width = tp.Width - 10;
                                    //tabControl.Top = 5;
                                    //tabControl.Left = 5;
                                    // Add the UserControl to the tab page
                                    //tabControl.TabPages.Add(tp);
                                }
                                groupBoxAddOn.Install(tp);
                                groupBoxAddOn.EventPlug(this.plugEvent);
                                object[] VersionInfo = asm.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
                                if (VersionInfo.Length > 0)
                                {
                                    string version = ((AssemblyFileVersionAttribute) VersionInfo[0]).Version;
                                    string versionDll =
                                        Config.GetInnerTextValue(xmlData, "//membs/plugins/" + moduleInfo.Name + "/version");
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
                        errorMessage = string.Format("Votre cl� est invalide pour le module {0}", moduleInfo.Name);
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
                errorMessage = "L'assemby sp�cifi� ne contient aucun module.";
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