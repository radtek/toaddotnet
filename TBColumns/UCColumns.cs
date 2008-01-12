using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using ULib;

namespace TBColumns
{
    public partial class UCColumns : UserControl, ITabPageAddOn
    {
        private DGVQuery uLib;        
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;
        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCColumns()
        {
            InitializeComponent();
        }

        #region PluginInstall
        /// <summary> 
        /// Required implementation of the interface.
        /// </summary>
        public void Install(TabControl tabControl)
        {
            // Create a new tab page as we implement a ITabPageAddOn
            TabPage tp = new TabPage("Columns");
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
            //MessageBox.Show("test " + data);
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(data);

            string userid = null;
            string password = null;
            string datasource = null;

            foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("connection"))
            {
                userid = xmlNode.Attributes.GetNamedItem("userid").Value;
                password = xmlNode.Attributes.GetNamedItem("password").Value;
                datasource = xmlNode.Attributes.GetNamedItem("datasource").Value;
            }

            
            
            foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("table"))
            {
                string tablename = xmlNode.Attributes.GetNamedItem("id").Value;
                bool bConnexion = false;
                Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
                if (!String.IsNullOrEmpty(userid) && !String.IsNullOrEmpty(password) &&
                    !String.IsNullOrEmpty(datasource))
                    bConnexion =
                        connexion.Open(userid, password, datasource);
                if (bConnexion)
                {
                    string SQL = "SELECT   c.cname,  " +
                                 "         c.colno,   " +
                                 "         (SELECT c1.POSITION  " +
                                 "            FROM SYS.cdef$ cd,  " +
                                 "                 SYS.con$ cn,  " +
                                 "                 SYS.obj$ o,  " +
                                 "                 SYS.user$ u,  " +
                                 "                 SYS.dba_cons_columns c1  " +
                                 "           WHERE cd.type# = 2  " +
                                 "             AND cd.con# = cn.con#  " +
                                 "             AND cd.obj# = o.obj#  " +
                                 "             AND o.owner# = u.user#  " +
                                 "             AND u.NAME = 'ABSIS'  " +
                                 "             AND o.NAME = c.tname  " +
                                 "             AND c1.column_name = c.cname  " +
                                 "             AND c1.table_name = c.tname  " +
                                 "             AND c1.constraint_name = cn.NAME  " +
                                 "             AND c1.owner = u.NAME) pk,  " +
                                 "         DECODE (c.NULLS, 'NULL', 'Y', 'N') NULLS,  " +
                                 "         c.coltype,  " +
                                 "         c.width,  " +
                                 "         c.PRECISION,  " +
                                 "         c.scale,  " +
                                 "         c.defaultval,  " +
                                 "         c.character_set_name,  " +
                                 "         ucc.comments  " +
                                 "    FROM user_col_comments ucc, col c  " +
                                 "   WHERE ucc.table_name = '" + tablename + "'  " +
                                 "     AND c.tname = ucc.table_name  " +
                                 "     AND c.cname = ucc.column_name  " +
                                 "ORDER BY c.tname, c.colno ";
                    
                    uLib = new DGVQuery(dataGridViewOracleFields, connexion);
                    uLib.Start(SQL);                    
                }                
            }
        }
        #endregion

        
        private void dataGridViewOracleFields_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < dataGridViewOracleFields.Rows[e.RowIndex].Cells.Count; i++)
            //{
            //    dataGridViewOracleFields[i, e.RowIndex].Style.BackColor = Color.Yellow;
            //}

            if (dataGridViewOracleFields.Rows[e.RowIndex] != null)
                if (dataGridViewOracleFields.Rows[e.RowIndex].Cells["comments"].Value != null)
                    textBoxFieldComment.Text =
                        dataGridViewOracleFields.Rows[e.RowIndex].Cells["comments"].Value.ToString();
                else
                    textBoxFieldComment.Text = null;
        }

        private void dataGridViewOracleFields_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < dataGridViewOracleFields.Rows[e.RowIndex].Cells.Count; i++)
            //{
            //    dataGridViewOracleFields[i, e.RowIndex].Style.BackColor = Color.Empty;
            //}
        }
    }
}
