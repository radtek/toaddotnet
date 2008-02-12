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
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Connexion;
using PluginTypes;
using Schema;
using ULib;

namespace TBTreeSchema
{
    public partial class UCTreeSchema : UserControl, ITabPageLeftAddOn, IFormAddOn
    {
        //private bool Dragging;
        //private int mouseX, mouseY;
        //private int clipLeft, clipTop, clipWidth, clipHeight;  
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;
        /// <summary>
        /// Private connection information
        /// </summary>
        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        private Form parentForm;

        public Form ParentForm
        {
            get { return parentForm; }
            set { parentForm = value; }
        }

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCTreeSchema()
        {
            InitializeComponent();            
        }
        #region IFormAddOn Members

        void IFormAddOn.Install(Form form)
        {
            ParentForm = form;
            //throw new Exception("The method or operation is not implemented.");
        }

        void IFormAddOn.EventPlug(PlugEvent e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion
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
                            GetSchema();
                        }
                        break;
                    default:
                        break;
                }
            }            
        }

        #endregion
        
        private void GetObj(DbObjectItem DbOI, TreeNode tNode)
        {
            TreeNode TablesNode = new TreeNode(DbOI.Name);
            TablesNode.Tag = DbOI; // obj_label.ToLower();
            switch (DbOI.Type.ToUpper())
            {
                case  "DATASOURCES":
                    TablesNode.ImageIndex = 1;
                    TablesNode.SelectedImageIndex = 1;
                    break;
                case "TABLES":
                    TablesNode.ImageIndex = 2;
                    TablesNode.SelectedImageIndex = 2;
                    break;
                case "VIEWS":
                    TablesNode.ImageIndex = 2;
                    TablesNode.SelectedImageIndex = 2;
                    break;
                case "FUNCTIONS":
                    TablesNode.ImageIndex = 3;
                    TablesNode.SelectedImageIndex = 3;
                    break;
                case "PROCEDURES":
                    TablesNode.ImageIndex = 4;
                    TablesNode.SelectedImageIndex = 4;
                    break;
                case "PACKAGES":
                    TablesNode.ImageIndex = 5;
                    TablesNode.SelectedImageIndex = 5;
                    break;
                case "TRIGGERS":
                    TablesNode.ImageIndex = 6;
                    TablesNode.SelectedImageIndex = 6;
                    break;
                case "SEQUENCES":
                    TablesNode.ImageIndex = 7;
                    TablesNode.SelectedImageIndex = 7;
                    break;
                
                default:
                    TablesNode.ImageIndex = 0;
                    TablesNode.SelectedImageIndex = 0;
                    break;
            }
            tNode.Nodes.Add(TablesNode);
            TreeQuery uLib = new TreeQuery(TablesNode, connexion);
            uLib.Start(string.Format("select object_name, status from obj where object_type = '{0}'", DbOI.Type.Substring(0, DbOI.Type.Length - 1).ToUpper()));
        }

        private void GetSchema()
        {
            if (connexion.IsOpen)
            {
                //SendConnectionInfo(plugSender);
                using (DbCommand cmd = connexion.Cnn.CreateCommand())
                {
                    // Create the tree structure
                    treeViewOracleSchema.Nodes.Clear();
                    TreeNode RootNode = null;
                    DbObjectItem RootDbOI = new DbObjectItem(connexion.OracleConnexion.DataSource, "datasource");             
                    RootNode = treeViewOracleSchema.Nodes.Add(connexion.OracleConnexion.DataSource);
                    RootNode.ImageIndex = 1;
                    RootNode.SelectedImageIndex = 1;
                    RootNode.Tag = RootDbOI; //"datasource";
                    
                    // Get All tables
                    GetObj(new DbObjectItem("Tables", "tables"), RootNode);
                    
                    // Get All views
                    GetObj(new DbObjectItem("Views", "views"), RootNode);
                    
                    // Get All Function
                    GetObj(new DbObjectItem("Functions", "functions"), RootNode);                    
                    
                    // Get All Procedure
                    GetObj(new DbObjectItem("Procedures", "procedures"), RootNode);
                    
                    // Get All Packages
                    GetObj(new DbObjectItem("Packages", "packages"), RootNode);
                    
                    // Get All Triggers
                    GetObj(new DbObjectItem("Triggers", "triggers"), RootNode);
                    
                    // Get All Sequences
                    GetObj(new DbObjectItem("Sequences", "sequences"), RootNode);
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
            Utils.SendSelectedObject((DbObjectItem)e.Node.Tag, plugSender); // SendSelectedObject(plugSender);
            GetTreeChildDetail(e.Node);
        }

        //private void SendSelectedObject(PlugEvent sender)
        //{
        //    //Personnes personne = (Personnes)PersonListBox.SelectedItem;
        //    XmlDocument doc = new XmlDocument();
        //    XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //    doc.AppendChild(docNode);
        //    XmlNode rootNode = doc.CreateElement("ToadDotNet");
        //    doc.AppendChild(rootNode);

        //    XmlNode actionNode = doc.CreateElement("action");
        //    string tagType = treeViewOracleSchema.SelectedNode.Tag.ToString().Replace(" ", "");

        //    actionNode.InnerText = "get" + tagType.ToLower();
        //    rootNode.AppendChild(actionNode);

        //    XmlNode productNode = doc.CreateElement(tagType.Replace(" ", ""));
        //    XmlAttribute productAttribute = doc.CreateAttribute("id");
        //    switch (tagType)
        //    {
        //        case "packagebodys":
        //        case "packagespecs":
        //            productAttribute.Value = treeViewOracleSchema.SelectedNode.Parent.Text;
        //            break;
        //        case "packagebody":
        //        case "packagespec":
        //            productAttribute.Value = treeViewOracleSchema.SelectedNode.Parent.Parent.Text;
        //            break;
        //        default:
        //            productAttribute.Value = treeViewOracleSchema.SelectedNode.Text;
        //            break;
        //    }

        //    productNode.Attributes.Append(productAttribute);
        //    actionNode.AppendChild(productNode);
        //    //Console.WriteLine(string.Format("tagtype = {0} - producAttribute = {1}", tagType, productAttribute.Value));
        //    if (sender != null)
        //        sender.Send(doc.OuterXml);
        //}

        private void GetTreeChildDetail(TreeNode SelectedNode)
        {
            if (SelectedNode.Nodes.Count == 0)
            {
                string tagType = ((DbObjectItem) SelectedNode.Tag).Type;
                if (!connexion.IsOpen && !String.IsNullOrEmpty(connexion.OracleConnexion.UserId) && !String.IsNullOrEmpty(connexion.OracleConnexion.Password) &&
                            !String.IsNullOrEmpty(connexion.OracleConnexion.DataSource))
                    connexion.Open(connexion.OracleConnexion.UserId, connexion.OracleConnexion.Password,
                                       connexion.OracleConnexion.DataSource);


                switch (tagType.Trim().ToLower())
                {
                    case "view":
                    case "table":
                        //bool bConnexion = false;
                        if (connexion.IsOpen)
                        {
                            DbObjectItem DbOi = new DbObjectItem("Fields", "fields");
                            TreeNode FieldsNode = new TreeNode(DbOi.Name);
                            FieldsNode.Tag = DbOi;
                            FieldsNode.SelectedImageIndex = 8;
                            FieldsNode.ImageIndex = 8;
                            SelectedNode.Nodes.Add(FieldsNode);
                            TreeQuery FieldsNodeQry = new TreeQuery(FieldsNode, connexion);
                            FieldsNodeQry.Start(string.Format("SELECT cname, 'VALID' FROM col where tname = '{0}'", SelectedNode.Text));
                        }
                        break;
                    case "field":
                        if (connexion.IsOpen)
                        {
                            DbObjectItem DbOi = new DbObjectItem("FKs", "fks");
                            TreeNode FieldsNode = new TreeNode(DbOi.Name);
                            FieldsNode.Tag = DbOi;
                            FieldsNode.SelectedImageIndex = 9;
                            FieldsNode.ImageIndex = 9;
                            TreeQuery FKsNodeQry = new TreeQuery(FieldsNode, connexion);
                            string SQL = "SELECT r.table_name||'.'||a.COLUMN_NAME cname, 'VALID' " +
                                            "FROM user_constraints t, user_constraints r, user_cons_columns b, user_cons_columns a " +
                                            "WHERE t.r_constraint_name = r.constraint_name " +
                                            "and a.CONSTRAINT_NAME = r.CONSTRAINT_NAME " +
                                            "and b.CONSTRAINT_NAME = t.CONSTRAINT_NAME " +
                                            "AND t.r_owner = r.owner " +
                                            "AND t.constraint_type='R' " +
                                            "AND t.table_name = '{0}' " +
                                            "and b.COLUMN_NAME = '{1}' ";
                            //FKsNodeQry.Start(string.Format(SQL, SelectedNode.Parent.Parent.Text, SelectedNode.Text));
                            FKsNodeQry.Display(string.Format(SQL, SelectedNode.Parent.Parent.Text, SelectedNode.Text));
                            if (FieldsNode.Nodes.Count > 0)
                                SelectedNode.Nodes.Add(FieldsNode);

                        }
                        break;
                    case "package":
                        if (connexion.IsOpen)
                        {
                            DbObjectItem DbOi = new DbObjectItem("Package Spec", "packagespecs");
                            TreeNode PackagesNode = new TreeNode(DbOi.Name);
                            PackagesNode.Tag = DbOi;
                            SelectedNode.Nodes.Add(PackagesNode);

                            // Get all procedure and functions
                            TreeQuery PFNodeQry = new TreeQuery(PackagesNode, connexion);
                            string SQL = "Select distinct " +
                                         "       object_name, 'VALID' /*, position, data_type, overload, argument_name, " +
                                         "       data_level, data_length, data_precision, data_scale, default_value, " +
                                         "       in_out, object_id, sequence */" +
                                         "from   all_arguments " +
                                         "where  object_id = (select object_id " +
                                         "         from sys.user_objects  " +
                                         "         where object_name ='{0}' " +
                                         "         and object_type in ('PACKAGE')) " +
                                         "order by Object_Name --, Overload, Sequence ";

                            PFNodeQry.Display(string.Format(SQL, SelectedNode.Text));

                            DbObjectItem DbOiPackageBody = new DbObjectItem("Package body", "packagebodys");
                            TreeNode PackagesBodyNode = new TreeNode(DbOiPackageBody.Name);
                            PackagesBodyNode.Tag = DbOiPackageBody;
                            SelectedNode.Nodes.Add(PackagesBodyNode);

                            // Get all procedure and functions
                            TreeQuery PFBNodeQry = new TreeQuery(PackagesBodyNode, connexion);
                            PFBNodeQry.Display(string.Format(SQL, SelectedNode.Text));
                        }
                        break;
                    case "function":
                    case "procedure":
                        if (connexion.IsOpen)
                        {
                            DbObjectItem DbOi = new DbObjectItem("Parameters", "parameters");
                            TreeNode ParametersNode = new TreeNode(DbOi.Name);
                            ParametersNode.Tag = DbOi;
                            SelectedNode.Nodes.Add(ParametersNode);

                            // Get all procedure and functions
                            TreeQuery ParamNodeQry = new TreeQuery(ParametersNode, connexion);
                            string SQL = "Select lower(argument_name||': '||in_out||' '||data_type) param, 'VALID' " +
                                         "from   all_arguments " +
                                         "where  object_id = (select object_id " +
                                         "         from sys.user_objects  " +
                                         "         where object_name ='{0}' " +
                                         "         and object_type in ('PROCEDURE', 'FUNCTION')) " +
                                         "order by Sequence ";


                            ParamNodeQry.Display(
                                string.Format(SQL, SelectedNode.Text));
                            //if (PackagesNode.Nodes.Count > 0)
                            //    SelectedNode.Nodes.Add(PackagesNode);
                        }
                        break;
                    case "packagespec":
                    case "packagebody":
                    case "proc_func":
                        if (connexion.IsOpen)
                        {
                            DbObjectItem DbOi = new DbObjectItem("Parameters", "Parameters");
                            TreeNode ParametersNode = new TreeNode(DbOi.Name);
                            ParametersNode.Tag = DbOi;
                            SelectedNode.Nodes.Add(ParametersNode);

                            // Get all procedure and functions
                            TreeQuery ParamNodeQry = new TreeQuery(ParametersNode, connexion);
                            string SQL = "Select lower(argument_name||': '||in_out||' '||data_type) param, 'VALID' " +
                                         "from   all_arguments " +
                                         "where  object_id = (select object_id " +
                                         "         from sys.user_objects  " +
                                         "         where object_name ='{0}' " +
                                         "         and object_type in ('PACKAGE', 'PROCEDURE', 'FUNCTION')) " +
                                         "       and object_name = '{1}' " +
                                         "order by Sequence ";


                            ParamNodeQry.Display(
                                string.Format(SQL, SelectedNode.Parent.Parent.Text,
                                              SelectedNode.Text));
                            //if (PackagesNode.Nodes.Count > 0)
                            //    SelectedNode.Nodes.Add(PackagesNode);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void treeViewOracleSchema_MouseDown(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left) 
               {
                   TreeView tree = (TreeView)sender;

                   // Get the node underneath the mouse.
                   TreeNode node = tree.GetNodeAt(e.X, e.Y);
                   tree.SelectedNode = node;
                   // Start the drag-and-drop operation with a cloned copy of the node.
                   if (node != null)
                   {
                       tree.DoDragDrop(node, DragDropEffects.Copy);                       
                   }                   
               }              
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = treeViewOracleSchema.SelectedNode;
            TreeNode RootNode = currentNode.Parent;
            

            string tagType = currentNode.Tag.ToString();

            switch(tagType)
            {
                case "tables":
                    currentNode.Remove();
                    // Get All tables
                    GetObj(new DbObjectItem("Tables", "table"), RootNode);
                    break;
                case "table":
                    currentNode.Nodes.Clear();
                    GetTreeChildDetail(currentNode);
                    break;
                case "views":
                    currentNode.Remove();
                    // Get All views
                    GetObj(new DbObjectItem("Views", "view"), RootNode);
                    break;
                case "functions":
                    currentNode.Remove();
                    // Get All Function
                    GetObj(new DbObjectItem("Functions", "function"), RootNode);
                    break;
                    // Get All Procedure
                case "procedures":
                    currentNode.Remove();
                    GetObj(new DbObjectItem("Procedures", "procedure"), RootNode);
                    break;
                case "packages":
                    currentNode.Remove();
                    // Get All Packages
                    GetObj(new DbObjectItem("Packages", "package"), RootNode);
                    break;
                case "triggers":
                    currentNode.Remove();
                    // Get All Triggers
                    GetObj(new DbObjectItem("Triggers", "trigger"), RootNode);
                    break;
                case "sequences":
                    currentNode.Remove();
                    // Get All Sequences
                    GetObj(new DbObjectItem("Sequences", "sequence"), RootNode);
                    break;
                case "datasource":
                    currentNode.Remove();
                    GetSchema();
                    break;
                default:
                    break;
            }
        }

        private void treeViewOracleSchema_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DbObjectItem DbOI = (DbObjectItem) treeViewOracleSchema.SelectedNode.Tag;
                if (DbOI.Type.ToLower() == "fk")
                {
                    DbOI.Type = "table";
                    DbOI.Name = DbOI.Name.Substring(0, DbOI.Name.IndexOf('.'));
                }
                if (DbOI.Type.ToLower() == "table" || DbOI.Type.ToLower() == "view")
                {
                    MainForm TableForm = new MainForm();
                    TableForm.splitContainer1.Panel1Collapsed = true;
                    TableForm.connexion = this.connexion;
                    TableForm.ParentForm = this.ParentForm;
                    
                    //TableForm.MdiParent = this.ParentForm.ParentForm;
                    TableForm.Show();
                    Utils.SendConnectionInfo(connexion, TableForm.plugEvent);
                    Utils.SendSelectedObject(DbOI, TableForm.plugEvent);
                    TableForm.Text = DbOI.Name + " " + TableForm.Text;
                }
                
            }
        }
    }
}