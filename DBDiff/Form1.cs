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
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MnuConnection;
using PluginTypes;
using ULib;

namespace DBDiff
{
    public partial class Form1 : Form
    {
        //private DGVQuery uLib;

        private Connexion.Connexion SourceConnexion;
        private Connexion.Connexion TargetConnexion;
        private DateTime startTime;
        private string tablenames;
        private string SQL;

        public Form1()
        {
            InitializeComponent();
            Version v = Assembly.GetEntryAssembly().GetName().Version;
            this.Text = "DB Diff " + String.Format(v.ToString());
        }

        private void SourceRefreshButton_Click(object sender, EventArgs e)
        {
            if (SourceTablesCheckedListBox.CheckedItems.Count > 0)
            {
                // Open connexions
                tablenames = "";
                foreach (object itemChecked in SourceTablesCheckedListBox.CheckedItems)
                {
                    if (!string.IsNullOrEmpty(tablenames))
                        tablenames += ", ";
                    tablenames += "'" + itemChecked.ToString() + "'";
                    //SourceTablesCheckedListBox.Items.IndexOf(itemChecked)).ToString()                
                }
                SQL = "SELECT   c.tname tablename, " +
                      "         c.CNAME columnname, " +
                      "         c.COLNO, " +
                      "         c.COLTYPE, " +
                      "         c.WIDTH, " +
                      "         c.PRECISION, " +
                      "         c.SCALE, " +
                      "         decode(c.NULLS, 'NULL', 'Y', 'N') Nulls, " +
                      "         c.DEFAULTVAL, " +
                      "         c.CHARACTER_SET_NAME, " +
                      "         (SELECT cols.POSITION " +
                      "            FROM all_constraints cons, all_cons_columns cols " +
                      "           WHERE cols.table_name = c.tname " +
                      "             AND cols.column_name = c.cname " +
                      "             AND cons.constraint_type = 'P' " +
                      "             AND cons.constraint_name = cols.constraint_name " +
                      "             AND cons.owner = cols.owner " +
                      "             AND cols.owner = USER " +
                      "           ) pk, " +
                      "         ucc.comments " +
                      "    FROM user_col_comments ucc, col c " +
                      "   WHERE ucc.table_name in (" + tablenames + ") " +
                      "     AND c.tname = ucc.table_name " +
                      "     AND c.cname = ucc.column_name " +
                      "ORDER BY c.tname, c.colno ";

                getSourceColummn();
                getTargetColumn();
            }
            else
            {
                MessageBox.Show("You must select at least one table", "No table selected", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void getSourceColummn()
        {
            //SourceConnexion = new Connexion.Connexion("Oracle");
            SourceConnexion.Open(SourceConnexion.OracleConnexion.UserId, SourceConnexion.OracleConnexion.Password, SourceConnexion.OracleConnexion.DataSource);

            // Get columns comment of source connexion
            if (SourceConnexion.Cnn.State.ToString() != "Closed")
            {
                DGVQuery querySource = new DGVQuery(SourceDataGridView, SourceConnexion);
                querySource.Sql = SQL;
                toolStripProgressBarQuerySource.Visible = true;
                startTime = DateTime.Now;
                BackgroundWorker backgroundWorkerSrc = new BackgroundWorker();
                backgroundWorkerSrc.DoWork += new DoWorkEventHandler(backgroundWorkerSrc_DoWork);
                backgroundWorkerSrc.ProgressChanged +=
                    new ProgressChangedEventHandler(backgroundWorkerSrc_ProgressChanged);
                backgroundWorkerSrc.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(backgroundWorkerSrc_RunWorkerCompleted);
                backgroundWorkerSrc.WorkerReportsProgress = true;
                backgroundWorkerSrc.WorkerSupportsCancellation = true;

                if (backgroundWorkerSrc.IsBusy)
                    backgroundWorkerSrc.CancelAsync();
                while (backgroundWorkerSrc.IsBusy) ;

                backgroundWorkerSrc.RunWorkerAsync(querySource);
            }
        }

        private void getTargetColumn()
        {
            // Get columns comment of target connexion
            //TargetConnexion = new Connexion.Connexion("Oracle");
            if (TargetConnexion != null)
            {
                TargetConnexion.Open(TargetConnexion.OracleConnexion.UserId, TargetConnexion.OracleConnexion.Password, TargetConnexion.OracleConnexion.DataSource);
                if (TargetConnexion.Cnn.State.ToString() != "Closed")
                {
                    DGVQuery queryTarget = new DGVQuery(TargetDataGridView, TargetConnexion);
                    queryTarget.Sql = SQL;
                    toolStripProgressBarQueryTarget.Visible = true;
                    startTime = DateTime.Now;
                    BackgroundWorker backgroundWorkerTgt = new BackgroundWorker();
                    backgroundWorkerTgt.DoWork += new DoWorkEventHandler(backgroundWorkerSrc_DoWork);
                    backgroundWorkerTgt.ProgressChanged +=
                        new ProgressChangedEventHandler(backgroundWorkerTgt_ProgressChanged);
                    backgroundWorkerTgt.RunWorkerCompleted +=
                        new RunWorkerCompletedEventHandler(backgroundWorkerTgt_RunWorkerCompleted);
                    backgroundWorkerTgt.WorkerReportsProgress = true;
                    backgroundWorkerTgt.WorkerSupportsCancellation = true;

                    if (backgroundWorkerTgt.IsBusy)
                        backgroundWorkerTgt.CancelAsync();
                    while (backgroundWorkerTgt.IsBusy) ;

                    backgroundWorkerTgt.RunWorkerAsync(queryTarget);
                }    
            }            
        }

        private void SourceDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < SourceDataGridView.Rows[e.RowIndex].Cells.Count; i++)
            {
                SourceDataGridView[i, e.RowIndex].Style.BackColor = Color.Yellow;
            }

            if (SourceDataGridView.Rows[e.RowIndex] != null)
                if (SourceDataGridView.Rows[e.RowIndex].Cells["comments"].Value != null)
                    CommentTextBox.Text = SourceDataGridView.Rows[e.RowIndex].Cells["comments"].Value.ToString();
                else
                    CommentTextBox.Text = null;
        }

        private void SourceDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < SourceDataGridView.Rows[e.RowIndex].Cells.Count; i++)
            {
                SourceDataGridView[i, e.RowIndex].Style.BackColor = Color.Empty;
            }
        }

        private void TargetDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < TargetDataGridView.Rows[e.RowIndex].Cells.Count; i++)
            {
                TargetDataGridView[i, e.RowIndex].Style.BackColor = Color.Yellow;
            }

            if (TargetDataGridView.Rows[e.RowIndex] != null)
                if (TargetDataGridView.Rows[e.RowIndex].Cells["comments"].Value != null)
                    TargetCommentTextBox.Text = TargetDataGridView.Rows[e.RowIndex].Cells["comments"].Value.ToString();
                else
                    TargetCommentTextBox.Text = null;
        }

        private void TargetDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < TargetDataGridView.Rows[e.RowIndex].Cells.Count; i++)
            {
                TargetDataGridView[i, e.RowIndex].Style.BackColor = Color.Empty;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TnsParser parser = new TnsParser();
            //parser.Parse();
            //TnsEntryCollectionType SourceEntries = parser.TnsFileEntries;
            //foreach (TnsEntryType tnsEntry in SourceEntries)
            //{
            //    SourceTNSNamesComboBox.Items.Add(tnsEntry.TnsnameEntry);
            //    TargetTNSNamesComboBox.Items.Add(tnsEntry.TnsnameEntry);
            //}            
        }

        //private void SourceTNSNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SourceConnexion = new Connexion.Connexion("Oracle");
        //    // Open connexions
        //    SourceConnexion.Open("ABSIS", "PROPERTY", SourceTNSNamesComboBox.Text);
        //    // Get Tables of source connexion
        //    if (SourceConnexion.Cnn.State.ToString() != "Closed")
        //    {
        //        using (DbCommand cmd = SourceConnexion.Cnn.CreateCommand())
        //        {
        //            string SQL = "select distinct table_name  " +
        //                         "  from user_tables " +
        //                         " order by table_name ";

        //            cmd.CommandText = SQL;
        //            cmd.Prepare();

        //            using (DbDataReader rd = cmd.ExecuteReader())
        //            {
        //                while (rd.Read())
        //                {
        //                    SourceTablesCheckedListBox.Items.Add(rd.GetValue(0), false);
        //                    listBoxSourceTables.Items.Add(rd.GetValue(0));
        //                }
        //            }
        //        }
        //    }
        //    // close connexions
        //    SourceConnexion.Close();
        //}

        private void UpdateCommentButton_Click(object sender, EventArgs e)
        {
            if (SourceDataGridView.CurrentRow != null && SourceDataGridView.CurrentRow.Cells["comments"].Value != null && TargetConnexion != null)
            {
                //TargetConnexion = new Connexion.Connexion("Oracle");
                if (TargetConnexion.Open(TargetConnexion.OracleConnexion.UserId, TargetConnexion.OracleConnexion.Password, TargetConnexion.OracleConnexion.DataSource))
                {
                    TargetConnexion.DoCmd(
                        string.Format("COMMENT ON COLUMN {0}.{1} IS '{2}'",
                                      SourceDataGridView.CurrentRow.Cells["tablename"].Value,
                                      SourceDataGridView.CurrentRow.Cells["columnname"].Value,
                                      SourceDataGridView.CurrentRow.Cells["comments"].Value.ToString().Replace("'", "''")));
                    TargetConnexion.Close();
                }
            }
        }

        private void UpdateAllCommentButton_Click(object sender, EventArgs e)
        {
            //TargetConnexion = new Connexion.Connexion("Oracle");
            if (TargetConnexion != null && TargetConnexion.Open(TargetConnexion.OracleConnexion.UserId, TargetConnexion.OracleConnexion.Password, TargetConnexion.OracleConnexion.DataSource))
            {
                toolStripProgressBarQuerySource.Value = 0;
                toolStripProgressBarQuerySource.Visible = true;
                for (int i = 0; i < SourceDataGridView.Rows.Count; i++)
                {
                    toolStripProgressBarQuerySource.Value = (i * 100) / SourceDataGridView.Rows.Count;
                    if (SourceDataGridView.Rows[i].Cells["comments"].Value != null)
                        TargetConnexion.DoCmd(
                            string.Format("COMMENT ON COLUMN {0}.{1} IS '{2}'",
                                          SourceDataGridView.Rows[i].Cells["tablename"].Value,
                                          SourceDataGridView.Rows[i].Cells["columnname"].Value,
                                          SourceDataGridView.Rows[i].Cells["comments"].Value.ToString().Replace("'",
                                                                                                                "''")));
                }
                toolStripProgressBarQuerySource.Visible = false;
                TargetConnexion.Close();
                getTargetColumn();
            }
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < SourceTablesCheckedListBox.Items.Count; i++)
                SourceTablesCheckedListBox.SetItemChecked(i, AllCheckBox.Checked);
        }

        private void backgroundWorkerSrc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBarQuerySource.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerTgt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBarQueryTarget.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerSrc_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DGVQuery uLib = (DGVQuery)e.Argument;
            e.Result = uLib.Display(uLib.Sql, worker, e);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void backgroundWorkerSrc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            this.toolStripStatusLabelElapsedTimeSource.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
            if (e.Cancelled)
            {
                // The user canceled the operation.
                //MessageBox.Show("Operation was canceled.");
                toolStripStatusLabelRecordSource.Text = string.Format("Aborted by user.");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                toolStripStatusLabelRecordSource.Text = e.Result.ToString();
            }
            toolStripProgressBarQuerySource.Value = 100;
            toolStripProgressBarQuerySource.Visible = false;
            SourceConnexion.Close();
        }

        private void backgroundWorkerTgt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            this.toolStripStatusLabelElapsedTimeTarget.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
            if (e.Cancelled)
            {
                // The user canceled the operation.
                //MessageBox.Show("Operation was canceled.");
                toolStripStatusLabelRecordsTarget.Text = string.Format("Aborted by user.");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                toolStripStatusLabelRecordsTarget.Text = e.Result.ToString();
            }
            toolStripProgressBarQueryTarget.Value = 100;
            toolStripProgressBarQueryTarget.Visible = false;
            TargetConnexion.Close();
        }        

        private void buttonSelectSource_Click(object sender, EventArgs e)
        {
            FormConnection formConnection = new FormConnection();
            //formConnection.MdiParent = this.ParentForm;
            if (formConnection.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(formConnection.textBoxOracleUserId.Text)
                    && !string.IsNullOrEmpty(formConnection.textBoxOraclePassword.Text)
                    && !string.IsNullOrEmpty(formConnection.TNSNamesComboBox.Text))
                {
                    labelSourceTnsnames.Text = string.Format("Source {0}@{1}", formConnection.textBoxOracleUserId.Text, formConnection.TNSNamesComboBox.Text);
                    SourceConnexion = new Connexion.Connexion("Oracle");
                    // Open connexions
                    SourceConnexion.Open(formConnection.textBoxOracleUserId.Text, formConnection.textBoxOraclePassword.Text, formConnection.TNSNamesComboBox.Text);
                    // Get Tables of source connexion
                    if (SourceConnexion.Cnn.State.ToString() != "Closed")
                    {
                        using (DbCommand cmd = SourceConnexion.Cnn.CreateCommand())
                        {
                            string SQL = "select distinct table_name  " +
                                         "  from user_tables " +
                                         " order by table_name ";

                            cmd.CommandText = SQL;
                            cmd.Prepare();

                            using (DbDataReader rd = cmd.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    SourceTablesCheckedListBox.Items.Add(rd.GetValue(0), false);
                                }
                            }
                        }
                    }
                    // close connexions
                    SourceConnexion.Close();
                }
            }
        }

        private void buttonSelectTarget_Click(object sender, EventArgs e)
        {
            FormConnection formConnection = new FormConnection();
            //formConnection.MdiParent = this.ParentForm;
            if (formConnection.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(formConnection.textBoxOracleUserId.Text)
                    && !string.IsNullOrEmpty(formConnection.textBoxOraclePassword.Text)
                    && !string.IsNullOrEmpty(formConnection.TNSNamesComboBox.Text))
                {
                    Config.SaveLastConnectionInfo(formConnection.textBoxOracleUserId.Text,
                                           formConnection.textBoxOraclePassword.Text,
                                           formConnection.TNSNamesComboBox.Text);
                    
                    labelTargetTnsnames.Text = string.Format("Source {0}@{1}", formConnection.textBoxOracleUserId.Text, formConnection.TNSNamesComboBox.Text);
                    TargetConnexion = new Connexion.Connexion("Oracle");
                    // Open connexions
                    TargetConnexion.Open(formConnection.textBoxOracleUserId.Text,
                                         formConnection.textBoxOraclePassword.Text, formConnection.TNSNamesComboBox.Text);
                    TargetConnexion.Close();
                }
            }
        }

        private void buttonSaveComment_Click(object sender, EventArgs e)
        {
            if (SourceDataGridView.CurrentRow != null && SourceDataGridView.CurrentRow.Cells["comments"].Value != null)
            {
                //TargetConnexion = new Connexion.Connexion("Oracle");
                if (SourceConnexion.Open(SourceConnexion.OracleConnexion.UserId, SourceConnexion.OracleConnexion.Password, SourceConnexion.OracleConnexion.DataSource))
                {
                    SourceConnexion.DoCmd(
                        string.Format("COMMENT ON COLUMN {0}.{1} IS '{2}'",
                                      SourceDataGridView.CurrentRow.Cells["tablename"].Value,
                                      SourceDataGridView.CurrentRow.Cells["columnname"].Value,
                                      CommentTextBox.Text.Replace("'", "''")));
                    SourceConnexion.Close();
                    SourceDataGridView.CurrentRow.Cells["comments"].Value = CommentTextBox.Text;
                }
            }
        }

        private void SourceTablesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SourceTablesCheckedListBox.SelectedItems.Count > 0)
            {
                // Open connexions
                tablenames = "";
                foreach (object itemChecked in SourceTablesCheckedListBox.SelectedItems)
                {
                    if (!string.IsNullOrEmpty(tablenames))
                        tablenames += ", ";
                    tablenames += "'" + itemChecked.ToString() + "'";
                    //SourceTablesCheckedListBox.Items.IndexOf(itemChecked)).ToString()                
                }
                SQL = "SELECT   c.tname tablename, " +
                      "         c.CNAME columnname, " +
                      "         c.COLNO, " +
                      "         c.COLTYPE, " +
                      "         c.WIDTH, " +
                      "         c.PRECISION, " +
                      "         c.SCALE, " +
                      "         decode(c.NULLS, 'NULL', 'Y', 'N') Nulls, " +
                      "         c.DEFAULTVAL, " +
                      "         c.CHARACTER_SET_NAME, " +
                      "         (SELECT cols.POSITION " +
                      "            FROM all_constraints cons, all_cons_columns cols " +
                      "           WHERE cols.table_name = c.tname " +
                      "             AND cols.column_name = c.cname " +
                      "             AND cons.constraint_type = 'P' " +
                      "             AND cons.constraint_name = cols.constraint_name " +
                      "             AND cons.owner = cols.owner " +
                      "             AND cols.owner = USER " +
                      "           ) pk, " +
                      "         ucc.comments " +
                      "    FROM user_col_comments ucc, col c " +
                      "   WHERE ucc.table_name in (" + tablenames + ") " +
                      "     AND c.tname = ucc.table_name " +
                      "     AND c.cname = ucc.column_name " +
                      "ORDER BY c.tname, c.colno ";

                getSourceColummn();
                getTargetColumn();
            }
            else
            {
                MessageBox.Show("You must select at least one table", "No table selected", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            TargetConnexion.Close();
            SourceConnexion.Close();            
        }
    }
}