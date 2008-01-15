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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace ULib
{
    public class DGVQuery
    {
        private System.Windows.Forms.DataGridView dgv;
        private Connexion.Connexion connexion;
        public Thread mythread;

        #region consteurcteur
        public DGVQuery()
        {
            
        }

        public DGVQuery(DataGridView dgv)
        {
            this.dgv = dgv;
        }

        public DGVQuery(Connexion.Connexion connexion)
        {
            this.connexion = connexion;
        }

        public DGVQuery(DataGridView dgv, Connexion.Connexion connexion)
        {
            this.dgv = dgv;
            this.connexion = connexion;
        }
        #endregion

        #region properties
        public System.Windows.Forms.DataGridView DataGridView
        {
            get { return dgv; }
            set { dgv = value; }
        }

        public Connexion.Connexion Connexion
        {
            get { return connexion; }
            set { connexion = value; }
        }
        #endregion

        #region Delegate
        private delegate void datagridClear();
        private void ClearDataGrid()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }

        private delegate void datagridAddCol(string ColumnName, string HeaderText);
        private void AddColDataGrid(string ColumnName, string HeaderText)
        {
            dgv.Columns.Add(ColumnName, HeaderText);
        }

        private delegate void datagridAddRow(DataGridViewRow dgrv);
        private void AddRowDataGrid(DataGridViewRow dgrv)
        {
            dgv.Rows.Add(dgrv);
        }

        private delegate void setNumberRecord(int num, int total);
        private void SetNumberRecord(int num, int total)
        {
            //this.toolStripStatusLabelRecords.Text = string.Format("Record {0} of {1}", num, total);
        }

        private delegate void setPercentCompleted(int num, int total);
        private void SetPercentCompleted(int num, int total)
        {
            //if (total = 0)
            if (num > 100)
                num = 1;
            //this.toolStripProgressBar1.Value = num;
        }

        private delegate void setElapsedTime(TimeSpan elapsed);
        private void SetElapsedTime(TimeSpan elapsed)
        {
            //this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
        }
        
        #endregion

        #region publicMethode
        public void Start(object obj)
        {
            try
            {
                if (mythread != null && mythread.IsAlive)
                    mythread.Abort();
                //mythread = new Thread((Display));
                //mythread.Start(Convert.ToString(obj));
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Stop()
        {
            try
            {
                if (mythread != null && mythread.IsAlive)
                    mythread.Abort();
            }
            catch (Exception e)
            {                
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string Display(object obj, BackgroundWorker worker, DoWorkEventArgs eArgs)
        {
            string result = null;
            bool bConnexion = true;
            if (this.connexion == null || this.connexion.Cnn == null || this.connexion.Cnn.State.ToString() == "Closed")
            {
                connexion = new Connexion.Connexion("Oracle");
                bConnexion = connexion.Open();
            }
            if (bConnexion)
            {
                result = DisplayQueryData(connexion, Convert.ToString(obj), dgv, worker, eArgs);
            }
            
            return result;
        }
        #endregion

        #region display
        private string DisplayQueryData(Connexion.Connexion connexion, string SQL, DataGridView dataGridViewOracleData, BackgroundWorker worker, DoWorkEventArgs eArgs)
        {
            string result = null;
            try
            {                
                //string SelectedTable = treeViewOracleSchema.SelectedNode.Text;
                using (DbCommand cmd = connexion.Cnn.CreateCommand())
                {
                    int NumRec = 0;
                    try
                    {
                        string SQLCount = "SELECT count(*) " + SQL.Substring(SQL.ToUpper().IndexOf("FROM"));
                        cmd.CommandText = SQLCount; // string.Format("SELECT count(*) FROM {0}", SelectedTable);
                        cmd.Prepare();
                        NumRec = Convert.ToInt32(cmd.ExecuteScalar());   
                    }
                    catch (Exception e)
                    {
                        NumRec = 0;
                    }
                    
                    //string SQL = string.Format("SELECT * FROM {0}", SelectedTable);
                    cmd.CommandText = SQL;
                    cmd.Prepare();
                    //int colno = 0;
                    using (DbDataReader rd = cmd.ExecuteReader())
                    {
                        if (dataGridViewOracleData.InvokeRequired)
                        {
                            dataGridViewOracleData.Invoke(new datagridClear(ClearDataGrid));
                        }
                        else
                        {
                            dataGridViewOracleData.Rows.Clear();
                            dataGridViewOracleData.Columns.Clear();
                        }
                        for (int i = 0; i < rd.FieldCount; i++)
                        {
                            if (dataGridViewOracleData.InvokeRequired)
                            {
                                dataGridViewOracleData.Invoke(new datagridAddCol(AddColDataGrid),
                                                              new object[] { rd.GetName(i), rd.GetName(i) });
                            }
                            else
                                dataGridViewOracleData.Columns.Add(rd.GetName(i), rd.GetName(i));
                        }
                        while (rd.Read() && !worker.CancellationPending)
                        {
                            DataGridViewRow dgrv = new DataGridViewRow();
                            for (int i = 0; i < dataGridViewOracleData.Columns.Count; i++)
                            {
                                dgrv.Cells.Add(new DataGridViewTextBoxCell());
                                dgrv.Cells[i].Value = rd.GetValue(i);
                            }
                            if (dataGridViewOracleData.InvokeRequired)
                            {
                                dataGridViewOracleData.Invoke(new datagridAddRow(AddRowDataGrid), new object[] { dgrv });
                            }
                            else
                                dataGridViewOracleData.Rows.Add(dgrv);

                            int CurrentNumRec = dataGridViewOracleData.Rows.Count;
                            if (dataGridViewOracleData.InvokeRequired)
                            {
                                dataGridViewOracleData.Invoke(new setNumberRecord(SetNumberRecord),
                                                              new object[] { CurrentNumRec, NumRec });
                                dataGridViewOracleData.Invoke(new setPercentCompleted(SetPercentCompleted),
                                                              new object[] { CurrentNumRec, NumRec });
                            }
                            else
                            {
                                SetNumberRecord(CurrentNumRec, NumRec);
                                SetPercentCompleted(CurrentNumRec, NumRec);
                            }
                            if (NumRec != 0)
                            {
                                int percentComplete = (int)((float)CurrentNumRec / (float)NumRec * 100);
                                worker.ReportProgress(percentComplete);    
                            } else
                            {
                                worker.ReportProgress(CurrentNumRec % 100);
                            }                                                        
                        }
                        if (worker.CancellationPending)
                        {
                            eArgs.Cancel = true;
                            result = string.Format("Aborted by user. {0} records found", dataGridViewOracleData.Rows.Count);
                        } else
                        {
                            result = string.Format("{0} records found", dataGridViewOracleData.Rows.Count);    
                        }
                        
                        rd.Close();
                    }                    
                }
                return result;
            }
            catch (Exception e)
            {
                Exception ee = e;
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return errorMessage;
            }            
        }
        #endregion
    }
}
