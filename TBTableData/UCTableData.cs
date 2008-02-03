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
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using ULib;

namespace TBTableData
{
    public partial class UCTableData : UserControl, ITabPageAddOn
    {
        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        private DGVQuery uLib;
        private DateTime startTime;
        private string tablename;

        private TabPage tp;
        private TabControl tc;

        public Thread mythread;

        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCTableData()
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
            tp = new TabPage("Data");
            // Add the new tab page to the TabControl of the main window's application
            tc = tabControl;
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
                    case "gettable":
                        if (!tc.TabPages.Contains(tp))
                            tc.TabPages.Insert(1, tp);
                        if (connexion.IsOpen)
                        {                            
                            xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/table");
                            if (xmlNode != null)
                            {
                                string newtablename = xmlNode.Attributes.GetNamedItem("id").Value;
                                if (newtablename != tablename)
                                {
                                    tablename = newtablename;
                                    uLib = new DGVQuery(dataGridViewOracleFields, connexion);
                                    //uLib.Start(string.Format("SELECT * FROM {0}", tablename));
                                    toolStripProgressBarQuery.Visible = true;
                                    startTime = DateTime.Now;
                                    if (backgroundWorker1.IsBusy)
                                        backgroundWorker1.CancelAsync();
                                    while (backgroundWorker1.IsBusy) ;
                                    backgroundWorker1.RunWorkerAsync(string.Format("SELECT * FROM (SELECT ROWNUM n, t.* FROM {0} t) s WHERE s.n BETWEEN {1} AND {2}", tablename, 0, 500));    
                                }                                
                            }
                        }
                        break;
                    case "getfields":
                    case "getfield":
                    case "getfks":
                    case "getfk":
                        if (!tc.TabPages.Contains(tp))
                            tc.TabPages.Insert(1, tp);
                        break;
                    default:
                        if (tc.TabPages.Contains(tp))
                            tc.TabPages.Remove(tp);
                        break;
                }
            }                        
        }

        #endregion

        #region Delegate
        private delegate void datagridClear();
        private void ClearDataGrid()
        {
            dataGridViewOracleFields.Rows.Clear();
            dataGridViewOracleFields.Columns.Clear();
        }

        private delegate void datagridAddCol(string ColumnName, string HeaderText);
        private void AddColDataGrid(string ColumnName, string HeaderText)
        {
            dataGridViewOracleFields.Columns.Add(ColumnName, HeaderText);
        }

        private delegate void datagridAddRow(DataGridViewRow dgrv);
        private void AddRowDataGrid(DataGridViewRow dgrv)
        {
            dataGridViewOracleFields.Rows.Add(dgrv);
        }

        private delegate void setNumberRecord(int num, int total);
        private void SetNumberRecord(int num,int total)
        {
            this.toolStripStatusLabelRecords.Text = string.Format("Record {0} of {1}", num, total);
        }

        private delegate void setPercentCompleted(int num);
        private void SetPercentCompleted(int num)
        {
            if (num > 100)
                num = 1;
            this.toolStripProgressBarQuery.Value = num;
        }

        private delegate void setElapsedTime(TimeSpan elapsed);
        private void SetElapsedTime(TimeSpan elapsed)
        {
            this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
        }
        #endregion        

        private void dataGridViewOracleFields_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int CurrentNumRec = e.RowIndex + 1;
            int NumRec = dataGridViewOracleFields.Rows.Count;
            if (dataGridViewOracleFields.InvokeRequired)
            {
                dataGridViewOracleFields.Invoke(new setNumberRecord(SetNumberRecord),
                                              new object[] { CurrentNumRec, NumRec });
            }
            else
            {
                SetNumberRecord(CurrentNumRec, NumRec);                
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            toolStripProgressBarQuery.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = uLib.Display(e.Argument.ToString(), worker, e);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            SetElapsedTime(elapsed);
            if (e.Cancelled)
            {
                // The user canceled the operation.
                //MessageBox.Show("Operation was canceled.");
                toolStripStatusLabelMessage.Text = string.Format("Aborted by user. {0} records found.", dataGridViewOracleFields.Rows.Count);
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

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            uLib = new DGVQuery(dataGridViewOracleFields, connexion);
            uLib.ClearData = false;
            //uLib.Start(string.Format("SELECT * FROM {0}", tablename));
            toolStripProgressBarQuery.Visible = true;
            toolStripProgressBarQuery.Value = 0;
            startTime = DateTime.Now;
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy) ;
            int rowsFetched = dataGridViewOracleFields.Rows.Count;
            backgroundWorker1.RunWorkerAsync(string.Format("SELECT * FROM (SELECT ROWNUM n, t.* FROM {0} t) s WHERE s.n BETWEEN {1} AND {2}", tablename, rowsFetched + 1, rowsFetched + 500));
        }

        private void toolStripButtonToEnd_Click(object sender, EventArgs e)
        {
            uLib = new DGVQuery(dataGridViewOracleFields, connexion);
            uLib.ClearData = false;
            //uLib.Start(string.Format("SELECT * FROM {0}", tablename));
            toolStripProgressBarQuery.Visible = true;
            toolStripProgressBarQuery.Value = 0;
            startTime = DateTime.Now;
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy) ;
            int rowsFetched = dataGridViewOracleFields.Rows.Count;
            backgroundWorker1.RunWorkerAsync(string.Format("SELECT * FROM (SELECT ROWNUM n, t.* FROM {0} t) s WHERE s.n > {1}", tablename, rowsFetched + 1));
        }
    }
}