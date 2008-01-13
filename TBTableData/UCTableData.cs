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
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using ULib;

namespace TBTableData
{
    public partial class UCTableData : UserControl, ITabPageAddOn
    {
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
            TabPage tp = new TabPage("Data");
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
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml((string)data);

            string userid = null;
            string password = null;
            string datasource = null;

            // Get Info for the oracle connection
            foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("connection"))
            {
                userid = xmlNode.Attributes.GetNamedItem("userid").Value;
                password = xmlNode.Attributes.GetNamedItem("password").Value;
                datasource = xmlNode.Attributes.GetNamedItem("datasource").Value;
            }

            bool bConnexion = false;
            Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
            if (!String.IsNullOrEmpty(userid) && !String.IsNullOrEmpty(password) &&
                !String.IsNullOrEmpty(datasource))
                bConnexion =
                    connexion.Open(userid, password, datasource);
            if (bConnexion)
            {
                string tablename = null;
                foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("table"))
                {
                    tablename = xmlNode.Attributes.GetNamedItem("id").Value;
                    DGVQuery uLib = new DGVQuery(dataGridViewOracleFields, connexion);
                    uLib.Start(string.Format("SELECT * FROM {0}", tablename));
                    //if (mythread != null && mythread.IsAlive)
                    //    mythread.Abort();
                    //mythread = new Thread((uLib.Display));
                    //mythread.Start(string.Format("SELECT * FROM {0}", tablename));
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
            this.toolStripProgressBar1.Value = num;
        }

        private delegate void setElapsedTime(TimeSpan elapsed);
        private void SetElapsedTime(TimeSpan elapsed)
        {
            this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
        }
        #endregion

        //public void Display(object data)
        //{
        //    DateTime startTime = DateTime.Now;
        //    XmlDocument xmlData = new XmlDocument();
        //    xmlData.LoadXml((string) data);

        //    string userid = null;
        //    string password = null;
        //    string datasource = null;

        //    // Get Info for the oracle connection
        //    foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("connection"))
        //    {
        //        userid = xmlNode.Attributes.GetNamedItem("userid").Value;
        //        password = xmlNode.Attributes.GetNamedItem("password").Value;
        //        datasource = xmlNode.Attributes.GetNamedItem("datasource").Value;
        //    }

        //    bool bConnexion = false;
        //    Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        //    if (!String.IsNullOrEmpty(userid) && !String.IsNullOrEmpty(password) &&
        //        !String.IsNullOrEmpty(datasource))
        //        bConnexion =
        //            connexion.Open(userid, password, datasource);

        //    foreach (XmlNode xmlNode in xmlData.GetElementsByTagName("table"))
        //    {
        //        string tablename = xmlNode.Attributes.GetNamedItem("id").Value;

        //        if (bConnexion)
        //            DisplayQueryData(connexion, string.Format("{0}", tablename), dataGridViewOracleFields);
        //    }
        //    TimeSpan elapsed = DateTime.Now - startTime;

        //    //Console.WriteLine("response time: {0}", elapsed.TotalSeconds);
        //    if (dataGridViewOracleFields.InvokeRequired)
        //        dataGridViewOracleFields.Invoke(new setElapsedTime(SetElapsedTime), new object[] { elapsed });            
        //    else
        //        this.toolStripStatusLabelElapsedTime.Text = string.Format("elapsed time: {0} s", elapsed.TotalSeconds);
        //}

        //public void DisplayQueryData(Connexion.Connexion connexion, string SelectedTable, DataGridView dataGridViewOracleData)
        //{
        //    try
        //    {
        //        //string SelectedTable = treeViewOracleSchema.SelectedNode.Text;
        //        using (DbCommand cmd = connexion.Cnn.CreateCommand())
        //        {
        //            cmd.CommandText = string.Format("SELECT count(*) FROM {0}", SelectedTable);
        //            cmd.Prepare();
        //            int NumRec = Convert.ToInt32(cmd.ExecuteScalar());
        //            string SQL = string.Format("SELECT * FROM {0}", SelectedTable);
        //            cmd.CommandText = SQL;
        //            cmd.Prepare();
        //            //int colno = 0;
        //            using (DbDataReader rd = cmd.ExecuteReader())
        //            {
        //                if (dataGridViewOracleData.InvokeRequired)
        //                {
        //                    dataGridViewOracleData.Invoke(new datagridClear(ClearDataGrid));
        //                }
        //                else
        //                {
        //                    dataGridViewOracleData.Rows.Clear();
        //                    dataGridViewOracleData.Columns.Clear();
        //                }
        //                for (int i = 0; i < rd.FieldCount; i++)
        //                {
        //                    if (dataGridViewOracleData.InvokeRequired)
        //                    {
        //                        dataGridViewOracleData.Invoke(new datagridAddCol(AddColDataGrid),
        //                                                      new object[] {rd.GetName(i), rd.GetName(i)});
        //                    }
        //                    else
        //                        dataGridViewOracleData.Columns.Add(rd.GetName(i), rd.GetName(i));
        //                }
        //                while (rd.Read())
        //                {
        //                    DataGridViewRow dgrv = new DataGridViewRow();
        //                    for (int i = 0; i < dataGridViewOracleData.Columns.Count; i++)
        //                    {
        //                        dgrv.Cells.Add(new DataGridViewTextBoxCell());
        //                        dgrv.Cells[i].Value = rd.GetValue(i);
        //                    }
        //                    if (dataGridViewOracleData.InvokeRequired)
        //                    {
        //                        dataGridViewOracleData.Invoke(new datagridAddRow(AddRowDataGrid), new object[] {dgrv});
        //                    }
        //                    else
        //                        dataGridViewOracleData.Rows.Add(dgrv);

        //                    int CurrentNumRec = dataGridViewOracleData.Rows.Count;
        //                    if (dataGridViewOracleData.InvokeRequired)
        //                    {
        //                        dataGridViewOracleData.Invoke(new setNumberRecord(SetNumberRecord),
        //                                                      new object[] {CurrentNumRec, NumRec});
        //                        dataGridViewOracleData.Invoke(new setPercentCompleted(SetPercentCompleted),
        //                                                      new object[] {(CurrentNumRec*100/NumRec)});
        //                    }
        //                    else
        //                    {
        //                        SetNumberRecord(CurrentNumRec, NumRec);
        //                        SetPercentCompleted((CurrentNumRec * 100 / NumRec));
        //                    }
        //                }
        //            }
        //            SetNumberRecord(dataGridViewOracleData.Rows.Count, NumRec);
        //            //dataGridViewOracleData.AutoResizeColumns();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string errorMessage = e.Message;
        //        while (e.InnerException != null)
        //        {
        //            e = e.InnerException;
        //            errorMessage += "\n" + e.Message;
        //        }
        //        MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
    }
}