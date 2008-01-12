using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using ULib;

namespace TBQuery
{
    public partial class UCQuery : UserControl, ITabPageAddOn
    {
        private DGVQuery uLib;
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        private string userid; // = "ABSIS";
        private string password; // = "property";
        private string datasource; // = "FT.LOCALPDL-ASOCS";
        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCQuery()
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
            TabPage tp = new TabPage("Query");
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

            // Get Info for the oracle connection
            foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("connection"))
            {
                userid = xmlNode.Attributes.GetNamedItem("userid").Value;
                password = xmlNode.Attributes.GetNamedItem("password").Value;
                datasource = xmlNode.Attributes.GetNamedItem("datasource").Value;
            }
        }
        #endregion

        private void toolStripButtonExecQuery_Click(object sender, EventArgs e)
        {
            bool bConnexion = false;
            Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
            if (!String.IsNullOrEmpty(userid) && !String.IsNullOrEmpty(password) &&
                !String.IsNullOrEmpty(datasource))
                bConnexion = connexion.Open(userid, password, datasource);
            if (bConnexion)
            {
                uLib = new DGVQuery(dataGridViewOracleQueryData, connexion);
                uLib.Start(richTextBoxOracleQuerySQL.Text);
            }
        }
        #region delegate
        
        private delegate void setNumberRecord(int num, int total);
        private void SetNumberRecord(int num, int total)
        {
            this.toolStripStatusLabelRecords.Text = string.Format("Record {0} of {1}", num, total);
        }

        #endregion        

        private void UCQuery_Load(object sender, EventArgs e)
        {
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("SELECT");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("FROM");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("WHERE");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("AND");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("OR");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("IN");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("DECODE");
            richTextBoxOracleQuerySQL.Settings.Keywords.Add("SUB");            

             richTextBoxOracleQuerySQL.Settings.Tablewords.Add("COMMANDES");
             richTextBoxOracleQuerySQL.Settings.Tablewords.Add("DUAL");

             richTextBoxOracleQuerySQL.Settings.Columnwords.Add("SYSDATE");

            richTextBoxOracleQuerySQL.Settings.Comment = "--";

            richTextBoxOracleQuerySQL.Settings.KeywordColor = Color.DarkBlue;
            richTextBoxOracleQuerySQL.Settings.TablewordColor = Color.DarkTurquoise;
            richTextBoxOracleQuerySQL.Settings.ColumnwordColor = Color.DarkGray;
            richTextBoxOracleQuerySQL.Settings.CommentColor = Color.DarkGreen;
            richTextBoxOracleQuerySQL.Settings.StringColor = Color.Gray;
            richTextBoxOracleQuerySQL.Settings.IntegerColor = Color.Red;

            // Let's not process strings and integers.

            richTextBoxOracleQuerySQL.Settings.EnableStrings = true;
            richTextBoxOracleQuerySQL.Settings.EnableIntegers = true;

            richTextBoxOracleQuerySQL.CompileKeywords();
            richTextBoxOracleQuerySQL.CompileTablewords();
            richTextBoxOracleQuerySQL.CompileColumnwords();

            richTextBoxOracleQuerySQL.Text = "SELECT * FROM TEST";
            richTextBoxOracleQuerySQL.ProcessAllLines();
        }

        private void richTextBoxOracleQuerySQL_TextChanged(object sender, EventArgs e)
        {
            //richTextBoxOracleQuerySQL.ProcessAllLines();
        }

        private void toolStripButtonAbortQuery_Click(object sender, EventArgs e)
        {
            if (uLib != null)
                uLib.Stop();
        }

        private void dataGridViewOracleQueryData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int CurrentNumRec = e.RowIndex + 1;
            int NumRec = dataGridViewOracleQueryData.Rows.Count;
            if (dataGridViewOracleQueryData.InvokeRequired)
            {
                dataGridViewOracleQueryData.Invoke(new setNumberRecord(SetNumberRecord),
                                              new object[] { CurrentNumRec, NumRec });
            }
            else
            {
                SetNumberRecord(CurrentNumRec, NumRec);
            }
        }
    }
}
