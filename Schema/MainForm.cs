/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte � Tous droits r�serv�s.

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
  modifier suivant les termes de la �GNU General Public License� telle que
  publi�e par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ult�rieure.
  
  Ce programme est distribu� dans l�espoir qu�il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans m�me la garantie implicite de COMMERCIALISABILIT�
  ni d�AD�QUATION � UN OBJECTIF PARTICULIER. Consultez la Licence G�n�rale
  Publique GNU pour plus de d�tails.
  
  Vous devriez avoir re�u une copie de la Licence G�n�rale Publique GNU avec
  ce programme ; si ce n�est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using Register;
using ULib;

namespace Schema
{
    public partial class MainForm : Form
    {
        // Needed to send event to the libraries
        public PlugEvent plugEvent = new PlugEvent();

        public Connexion.Connexion connexion = null;

        /// <summary>
        /// Define the language to be use 
        /// </summary>
        public CultureInfo CurrentCulture = null;

        public MainForm()
        {
            // D�finition de la culture par d�faut
            //System.Threading.Thread.CurrentThread.CurrentUICulture = FrenchCulture;
            CurrentCulture = new CultureInfo(Config.GetInnerTextValue(Config.Load(), "/alf-solution/AppConfig/lang"));
            System.Threading.Thread.CurrentThread.CurrentUICulture = CurrentCulture;
            InitializeComponent();
            //plugEvent = new PlugEvent();
            plugEvent.evtHandler += new PlugEvent._evtHandler(EventProcess);
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MembsForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.Nom()))
            {
                FormRegister formRegister = new FormRegister();
                if (formRegister.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
            AssemblyLoader asmLoader = new AssemblyLoader(this, this.ParentForm.Controls["menuStrip"] as MenuStrip, this.rightTabControl, this.leftTabControl, plugEvent);
            string PluginsPath = Config.GetElement(Config.Load(), "/alf-solution/AppConfig/plugin").GetAttribute("path"); //@".\plugins";//
            if (Directory.Exists(PluginsPath))
            {
                DirectoryInfo di = new DirectoryInfo(PluginsPath);
                string[] files = Directory.GetFiles(di.FullName, "*.dll");
                foreach (string file in files)
                {
                    if (asmLoader.Load(file))
                    {

                    }
                    else if (asmLoader.GetErrorMessage() != null)
                    {
                        //MessageBox.Show(asmLoader.GetErrorMessage(), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }                            
        }

        private void EventProcess(object sender, string data)
        {
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(data);
            XmlNode xmlNode = null;
            foreach (XmlNode xmlNodeAction in xmlData.GetElementsByTagName("action"))
            {
                switch (xmlNodeAction.InnerText)
                {
                    case "connect":
                        // Get Info for the oracle connection
                        xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/connection");
                        if (xmlNode != null)
                        {
                            connexion = new Connexion.Connexion("Oracle");
                            connexion.OracleConnexion.UserId = xmlNode.Attributes.GetNamedItem("userid").Value;
                            connexion.OracleConnexion.Password = xmlNode.Attributes.GetNamedItem("password").Value;
                            connexion.OracleConnexion.DataSource = xmlNode.Attributes.GetNamedItem("datasource").Value;
                            this.Text =
                                string.Format("{0}@{1}", connexion.OracleConnexion.UserId,
                                              connexion.OracleConnexion.DataSource);
                        }
                        break;
                    default:                        
                        break;
                }
            }
        }

        private void enregistrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }
    }
}