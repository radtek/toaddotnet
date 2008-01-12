using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
                mythread = new Thread((Display));
                mythread.Start(Convert.ToString(obj));
                
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
        public void Display(object obj)
        {
            DateTime startTime = DateTime.Now;
            bool bConnexion = true;
            if (this.connexion == null || this.connexion.Cnn == null || this.connexion.Cnn.State.ToString() == "Closed")
            {
                connexion = new Connexion.Connexion("Oracle");
                bConnexion = connexion.Open();
            }
            if (bConnexion)
            {
                DisplayQueryData(connexion, Convert.ToString(obj), dgv);
                //connexion.Close();
            }
            TimeSpan elapsed = DateTime.Now - startTime;

            //Console.WriteLine("response time: {0}", elapsed.TotalSeconds);
            if (dgv.InvokeRequired)
                dgv.Invoke(new setElapsedTime(SetElapsedTime), new object[] { elapsed });
            else
                SetElapsedTime(elapsed);
        }
        #endregion

        #region display
        private void DisplayQueryData(Connexion.Connexion connexion, string SQL, DataGridView dataGridViewOracleData)
        {
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
                        while (rd.Read())
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
                        }
                        rd.Close();
                    }
                    SetNumberRecord(dataGridViewOracleData.Rows.Count, NumRec);
                    //dataGridViewOracleData.AutoResizeColumns();
                }
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
        #endregion
    }
}
