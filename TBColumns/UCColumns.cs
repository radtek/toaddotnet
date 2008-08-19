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
using System.ComponentModel;
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
        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        private DGVQuery uLib;
        private DateTime startTime;
        private string CurrentTablename = null;
        private TabPage tp = null;
        private TabControl tc = null;
        
        private static readonly int DEFAULT_TABPOSITION = 0;
        private int tabPosition = DEFAULT_TABPOSITION; // default position for the tab;

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
            tp = new TabPage("Columns");
            // Add the new tab page to the TabControl of the main window's application
            tc = tabControl;
            //tabControl.TabPages.Add(tp);
            string TabPosition = Config.GetText("//alf-solution/plugins/TBColumns/tab/position");
            if (string.IsNullOrEmpty(TabPosition))
            {
                Config.SaveText("/alf-solution/plugins/TBColumns/tab/position", DEFAULT_TABPOSITION.ToString());
                tabPosition = DEFAULT_TABPOSITION;
            }
            else
            {
                tabPosition = Convert.ToInt32(TabPosition);
            }
            if (tabPosition > tc.TabPages.Count)
                tc.TabPages.Add(tp);
            else
                tc.TabPages.Insert(tabPosition, tp);  
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
                        }
                        break;
                    case "getview":
                    case "gettable":
                        if (connexion.IsOpen)
                        {
                            if (!tc.TabPages.Contains(tp))
                            {
                                tc.TabPages.Insert(tabPosition, tp);
                                // Set as the selected tab this one
                                tc.SelectedTab = tp;
                            }
                            string typeAction = xmlNodeAction.InnerText.Substring(3).Replace(" ", "");
                            xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/" + typeAction);
                            if (xmlNode != null)
                            {
                                string newtablename = xmlNode.Attributes.GetNamedItem("id").Value;
                                if (CurrentTablename != newtablename)
                                {
                                    CurrentTablename = newtablename;
                                    getTable();
                                }
                                    
                            }
                        }
                        break;
                    case "getfields":
                    case "getfield":
                    case "getfks":
                    case "getfk":
                        if (!tc.TabPages.Contains(tp))
                        {
                            tc.TabPages.Insert(tabPosition, tp);
                            // Set as the selected tab this one
                            tc.SelectedTab = tp;
                        }
                        
                        break;
                    default:
                        if (tc.TabPages.Contains(tp))
                            tc.TabPages.Remove(tp);                        
                        break;
                }
            }

            /* ---------------------------------- */
        }

        private void getTable()
        {
            string SQL = "SELECT   c.cname,  " +
                         "         c.colno, " +
                         "         (SELECT c1.POSITION " +
                         "            FROM SYS.user_cons_columns c1, SYS.user_constraints a1 " +
                         "           WHERE a1.table_name = c1.table_name " +
                         "             AND a1.constraint_name = c1.constraint_name " +
                         "             AND a1.constraint_type = 'P' " +
                         "             AND a1.table_name = c.tname " +
                         "             and C1.COLUMN_NAME = c.cname ) pk, " +
                         "         DECODE (c.NULLS, 'NULL', 'Y', 'N') NULLS,  " +
                         "         c.coltype,  " +
                         "         c.width, " +
                         "         c.PRECISION,  " +
                         "         c.scale,  " +
                         "         c.defaultval,  " +
                         "         c.character_set_name, " +
                         "         ucc.comments " +
                         "    FROM user_col_comments ucc,  " +
                         "         col c " +
                         "   WHERE ucc.table_name = '" + CurrentTablename + "' " +
                         "     AND c.tname = ucc.table_name " +
                         "     AND c.cname = ucc.column_name " +
                         "ORDER BY c.tname, c.colno ";


           
            //uLib.Start(SQL);
            startTime = DateTime.Now;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker1 = new BackgroundWorker();
                backgroundWorker1.WorkerSupportsCancellation = true;
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
                backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
                backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);

            }
                
            while (backgroundWorker1.IsBusy) ;
            uLib = new DGVQuery(dataGridViewOracleFields, connexion);
            backgroundWorker1.RunWorkerAsync(SQL);
        }

        #endregion

        #region delegate

        private delegate void setElapsedTime(TimeSpan elapsed);

        private void SetElapsedTime(TimeSpan elapsed)
        {
            this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
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

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            toolStripProgressBarQuery.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = uLib.Display(e.Argument.ToString(), backgroundWorker1, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
                                                          System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            SetElapsedTime(elapsed);
            if (e.Cancelled)
            {
                // The user canceled the operation.
                //MessageBox.Show("Operation was canceled.");
                toolStripStatusLabelMessage.Text =
                    string.Format("Aborted by user. {0} records found.", dataGridViewOracleFields.Rows.Count);
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                toolStripStatusLabelMessage.Text = e.Result.ToString();
                dataGridViewOracleFields.AutoResizeColumns();
            }
            toolStripProgressBarQuery.Visible = false;
        }

        private void toolStripButtonAddCol_Click(object sender, EventArgs e)
        {
            FormAddCol frmAddCol = new FormAddCol();
            frmAddCol.labelTablename.Text = CurrentTablename;
            frmAddCol.Tablename = CurrentTablename;
            if (frmAddCol.ShowDialog() == DialogResult.OK)
            {
                // Create the column
                string sql = frmAddCol.textBoxSql.Text;

                try
                {
                    if (this.connexion == null || this.connexion.Cnn == null ||
                        this.connexion.Cnn.State.ToString() == "Closed")
                    {
                        connexion = new Connexion.Connexion("Oracle");
                        connexion.Open();
                    }
                    if (connexion.IsOpen)
                    {
                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.Prepare();
                            //int colno = 0;
                            int result = cmd.ExecuteNonQuery();
                            getTable();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.Message;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        errorMessage += "\n" + ex.Message;
                    }
                    MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            frmAddCol.Close();
            frmAddCol.Dispose();
        }

        private void toolStripButtonDeleteCol_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    string.Format("Are you sure you want to drop the column {0} from the table {1} ?",
                                  dataGridViewOracleFields.CurrentRow.Cells["cname"].Value, CurrentTablename),
                    "Drop column", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql =
                    string.Format("ALTER TABLE {0} DROP COLUMN {1}", CurrentTablename,
                                  dataGridViewOracleFields.CurrentRow.Cells["cname"].Value);

                try
                {
                    if (this.connexion == null || this.connexion.Cnn == null ||
                        this.connexion.Cnn.State.ToString() == "Closed")
                    {
                        connexion = new Connexion.Connexion("Oracle");
                        connexion.Open();
                    }
                    if (connexion.IsOpen)
                    {
                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.Prepare();
                            //int colno = 0;
                            int result = cmd.ExecuteNonQuery();
                            getTable();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.Message;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        errorMessage += "\n" + ex.Message;
                    }
                    MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonModifyCol_Click(object sender, EventArgs e)
        {
        }

        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonAddCol_Click(sender, e);
        }

        private void dropColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDeleteCol_Click(sender, e);
        }

        private void addIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddIndex formAddIndex = new FormAddIndex();
            formAddIndex.labelTableName.Text = CurrentTablename;
            string ColumnName = dataGridViewOracleFields.CurrentRow.Cells["cname"].Value.ToString();
            formAddIndex.textBoxIndexName.Text = string.Format("NDX_{0}", ColumnName);
            formAddIndex.dataGridViewColumnName.Rows.Add(new string[] { ColumnName });

            if (formAddIndex.ShowDialog() == DialogResult.OK)
            {
                if (connexion.DoCmd(formAddIndex.textBoxSql.Text))
                {
                    //Add here the refresh of the tree
                }
            }

        }

        private void dropIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}