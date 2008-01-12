using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;
using System.Xml;
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

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCTreeSchema()
        {
            InitializeComponent();
        }

        #region PluginInstall

        /// <summary> 
        /// Required implementation of the interface.
        /// </summary>
        public void Install(TabControl tabControl)
        {
        }

        #endregion

        #region EventProcess

        /// <summary> 
        /// Required implementation of the event interface.
        /// </summary>
        public void EventPlug(PlugEvent e)
        {
            plugSender = e;
            plugSender.evtHandler += new PlugEvent._evtHandler(EventProcess);
        }

        /// <summary> 
        /// Method to execute when event is fired
        /// </summary>
        public void EventProcess(object sender, string data)
        {
            MessageBox.Show("test " + data);
        }

        #endregion

        #region ITabPageLeftAddOn Members

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
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private void buttonGetOracleTables_Click(object sender, EventArgs e)
        {
            bool bConnexion = false;
            Connexion.Connexion connexion = new Connexion.Connexion("Oracle");

            if (!String.IsNullOrEmpty(textBoxOracleUserId.Text) && !String.IsNullOrEmpty(textBoxOraclePassword.Text) &&
                !String.IsNullOrEmpty(textBoxOracleDataSource.Text))
                bConnexion =
                    connexion.Open(textBoxOracleUserId.Text, textBoxOraclePassword.Text, textBoxOracleDataSource.Text);

            if (bConnexion)
            {
                SendConnectionInfo();
                using (DbCommand cmd = connexion.Cnn.CreateCommand())
                {
                    // Get All tables
                    treeViewOracleSchema.Nodes.Clear();
                    TreeNode RootNode = null;
                    TreeNode TablesNode = null;
                    TreeNode node = null;
                    RootNode = treeViewOracleSchema.Nodes.Add(textBoxOracleDataSource.Text);
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
            XmlNode productsNode = doc.CreateElement("ToadDotNet");
            doc.AppendChild(productsNode);

            XmlNode productNode = doc.CreateElement(treeViewOracleSchema.SelectedNode.Tag.ToString());
            XmlAttribute productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = treeViewOracleSchema.SelectedNode.Text;
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);

            productNode = doc.CreateElement("connection");
            productAttribute = doc.CreateAttribute("userid");
            productAttribute.Value = textBoxOracleUserId.Text;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("password");
            productAttribute.Value = textBoxOraclePassword.Text;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("datasource");
            productAttribute.Value = textBoxOracleDataSource.Text;
            productNode.Attributes.Append(productAttribute);

            productsNode.AppendChild(productNode);

            if (plugSender != null)
                plugSender.Send(doc.OuterXml);
            if (treeViewOracleSchema.SelectedNode.Nodes.Count == 0 && treeViewOracleSchema.SelectedNode.Tag.ToString() == "table")
            {
                bool bConnexion = false;
                Connexion.Connexion connexion = new Connexion.Connexion("Oracle");

                if (!String.IsNullOrEmpty(textBoxOracleUserId.Text) && !String.IsNullOrEmpty(textBoxOraclePassword.Text) &&
                    !String.IsNullOrEmpty(textBoxOracleDataSource.Text))
                    bConnexion =
                        connexion.Open(textBoxOracleUserId.Text, textBoxOraclePassword.Text,
                                       textBoxOracleDataSource.Text);

                if (bConnexion)
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
            //Personnes personne = (Personnes)PersonListBox.SelectedItem;
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode productsNode = doc.CreateElement("ToadDotNet");
            doc.AppendChild(productsNode);

            XmlNode productNode = doc.CreateElement("connection");
            XmlAttribute productAttribute = doc.CreateAttribute("userid");
            productAttribute.Value = textBoxOracleUserId.Text;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("password");
            productAttribute.Value = textBoxOraclePassword.Text;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("datasource");
            productAttribute.Value = textBoxOracleDataSource.Text;
            productNode.Attributes.Append(productAttribute);

            productsNode.AppendChild(productNode);

            if (plugSender != null)
                plugSender.Send(doc.OuterXml);
        }
    }
}