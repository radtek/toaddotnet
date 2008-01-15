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
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;
using System.Xml;
using Connexion;
using PluginTypes;
using ULib;

namespace TBTreeSchema
{
    public partial class UCTreeSchema : UserControl, ITabPageLeftAddOn
    {
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCTreeSchema()
        {
            InitializeComponent();
            
        }

        #region PluginInstall

        void ITabPageLeftAddOn.Install(TabControl tabControl)
        {
            // Create a new tab page as we implement a ITabPageAddOn
            TabPage tp = new TabPage("Schema");
            // Add the new tab page to the TabControl of the main window's application
            tabControl.TabPages.Add(tp);
            // Set automatic resizing of the UserControl
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            this.Height = tp.Height - 10;
            this.Width = tp.Width - 10;
            this.Top = 5;
            this.Left = 5;
            // Add the UserControl to the tab page
            tp.Controls.Add(this);
        }

        void ITabPageLeftAddOn.EventPlug(PlugEvent e)
        {
            plugSender = e;
            plugSender.evtHandler += new PlugEvent._evtHandler(EventProcess);
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region EventProcess

        

        /// <summary> 
        /// Method to execute when event is fired
        /// </summary>
        public void EventProcess(object sender, string data)
        {
            //MessageBox.Show("test " + data);
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(data);
            foreach (XmlNode xmlNodeAction in xmlData.GetElementsByTagName("action"))
            {
                switch(xmlNodeAction.InnerText)
                {
                    case "connect":
                        // Get Info for the oracle connection
                        XmlNode xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/connection");
                        if (xmlNode != null)
                        {
                            //textBoxOracleUserId.Text = xmlNode.Attributes.GetNamedItem("userid").Value;
                            //textBoxOraclePassword.Text = xmlNode.Attributes.GetNamedItem("password").Value;
                            //textBoxOracleDataSource.Text = xmlNode.Attributes.GetNamedItem("datasource").Value;
                            connexion.OracleConnexion.UserId = xmlNode.Attributes.GetNamedItem("userid").Value;
                            connexion.OracleConnexion.Password = xmlNode.Attributes.GetNamedItem("password").Value;
                            connexion.OracleConnexion.DataSource = xmlNode.Attributes.GetNamedItem("datasource").Value;
                            if (connexion.IsOpen)
                            {
                                connexion.Close();
                            }
                            else
                            {
                                connexion.Open();
                            }
                            GetTables();
                        }
                        break;
                    default:
                        break;
                }
            }            
        }

        #endregion
        
        private void buttonGetOracleTables_Click(object sender, EventArgs e)
        {
            GetTables();
        }

        private void GetTables()
        {
            if (connexion.IsOpen)
            {
                SendConnectionInfo();
                using (DbCommand cmd = connexion.Cnn.CreateCommand())
                {
                    // Get All tables
                    treeViewOracleSchema.Nodes.Clear();
                    TreeNode RootNode = null;
                    TreeNode TablesNode = null;
                    //TreeNode node = null;
                    RootNode = treeViewOracleSchema.Nodes.Add(connexion.OracleConnexion.DataSource);
                    RootNode.Tag = "datasource";
                    TablesNode = new TreeNode("Tables");
                    TablesNode.Tag = "tables";
                    RootNode.Nodes.Add(TablesNode);

                    TreeQuery uLib = new TreeQuery(TablesNode, connexion);
                    uLib.Start("SELECT distinct tname FROM col");

                    // Get All views
                    TreeNode ViewsNode = new TreeNode("Views");
                    ViewsNode.Tag = "views";
                    RootNode.Nodes.Add(ViewsNode);
                    TreeQuery uLibView = new TreeQuery(ViewsNode, connexion);
                    uLibView.Start("SELECT view_name FROM user_views");

                    // Get All Function
                    TreeNode FunctionsNode = new TreeNode("Functions");
                    FunctionsNode.Tag = "functions";
                    RootNode.Nodes.Add(FunctionsNode);
                    TreeQuery uLibFunc = new TreeQuery(FunctionsNode, connexion);
                    uLibFunc.Start("SELECT distinct name FROM user_source where type = 'FUNCTION'");

                    // Get All Procedure
                    TreeNode ProceduresNode = new TreeNode("Procedures");
                    ProceduresNode.Tag = "procedures";
                    RootNode.Nodes.Add(ProceduresNode);
                    TreeQuery uLibProc = new TreeQuery(ProceduresNode, connexion);
                    uLibProc.Start("SELECT distinct name FROM user_source where type = 'PROCEDURE'");

                    // Get All Packages
                    TreeNode PackagesNode = new TreeNode("Packages");
                    PackagesNode.Tag = "packages";
                    RootNode.Nodes.Add(PackagesNode);
                    TreeQuery uLibPkg = new TreeQuery(PackagesNode, connexion);
                    uLibPkg.Start("SELECT distinct name FROM user_source where type = 'PACKAGE'");

                    // Get All Triggers
                    TreeNode TriggersNode = new TreeNode("Triggers");
                    TriggersNode.Tag = "triggers";
                    RootNode.Nodes.Add(TriggersNode);
                    TreeQuery uLibTrg = new TreeQuery(TriggersNode, connexion);
                    uLibTrg.Start("SELECT distinct name FROM user_source where type = 'TRIGGER'");
                }
                //connexion.Close();
            }
            else
            {
                MessageBox.Show("Unable to connect to Oracle", "Connextion failed", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private void treeViewOracleSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Personnes personne = (Personnes)PersonListBox.SelectedItem;
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode rootNode = doc.CreateElement("ToadDotNet");
            doc.AppendChild(rootNode);

            XmlNode actionNode = doc.CreateElement("action");
            actionNode.InnerText = "gettable";
            //XmlAttribute actionAttr = doc.CreateAttribute("connection")
            rootNode.AppendChild(actionNode);

            XmlNode productNode = doc.CreateElement(treeViewOracleSchema.SelectedNode.Tag.ToString());
            XmlAttribute productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = treeViewOracleSchema.SelectedNode.Text;
            productNode.Attributes.Append(productAttribute);
            actionNode.AppendChild(productNode);

            productNode = doc.CreateElement("connection");
            productAttribute = doc.CreateAttribute("userid");
            productAttribute.Value = connexion.OracleConnexion.UserId;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("password");
            productAttribute.Value = connexion.OracleConnexion.Password;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("datasource");
            productAttribute.Value = connexion.OracleConnexion.DataSource;
            productNode.Attributes.Append(productAttribute);

            rootNode.AppendChild(productNode);

            if (plugSender != null)
                plugSender.Send(doc.OuterXml);
            if (treeViewOracleSchema.SelectedNode.Nodes.Count == 0 && treeViewOracleSchema.SelectedNode.Tag.ToString() == "table")
            {
                //bool bConnexion = false;
                if (!connexion.IsOpen && !String.IsNullOrEmpty(connexion.OracleConnexion.UserId) && !String.IsNullOrEmpty(connexion.OracleConnexion.Password) &&
                    !String.IsNullOrEmpty(connexion.OracleConnexion.DataSource))
                    connexion.Open(connexion.OracleConnexion.UserId, connexion.OracleConnexion.Password,
                                       connexion.OracleConnexion.DataSource);

                if (connexion.IsOpen)
                {
                    TreeNode FieldsNode = new TreeNode("Fields");
                    FieldsNode.Tag = "fields";
                    treeViewOracleSchema.SelectedNode.Nodes.Add(FieldsNode);
                    TreeQuery FieldsNodeQry = new TreeQuery(FieldsNode, connexion);
                    FieldsNodeQry.Start(string.Format("SELECT cname FROM col where tname = '{0}'", treeViewOracleSchema.SelectedNode.Text));
                }
            }
        }

        private void SendConnectionInfo()
        {
            if (connexion.IsOpen && !String.IsNullOrEmpty(connexion.OracleConnexion.UserId) && !String.IsNullOrEmpty(connexion.OracleConnexion.Password) &&
                    !String.IsNullOrEmpty(connexion.OracleConnexion.DataSource))
            {
                //Personnes personne = (Personnes)PersonListBox.SelectedItem;
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                XmlNode productsNode = doc.CreateElement("ToadDotNet");
                doc.AppendChild(productsNode);

                XmlNode connectionNode = doc.CreateElement("connection");
                XmlAttribute productAttribute = doc.CreateAttribute("userid");
                productAttribute.Value = connexion.OracleConnexion.UserId;
                connectionNode.Attributes.Append(productAttribute);

                productAttribute = doc.CreateAttribute("password");
                productAttribute.Value = connexion.OracleConnexion.Password;
                connectionNode.Attributes.Append(productAttribute);

                productAttribute = doc.CreateAttribute("datasource");
                productAttribute.Value = connexion.OracleConnexion.DataSource;
                connectionNode.Attributes.Append(productAttribute);

                productsNode.AppendChild(connectionNode);

                if (plugSender != null)
                    plugSender.Send(doc.OuterXml);
            }
        }
    }
}