/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte � Tous droits r�serv�s.

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
  modifier suivant les termes de la �GNU General Public License� telle que
  publi�e par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ult�rieure.
  
  Ce programme est distribu� dans l�espoir qu�il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans m�me la garantie implicite de COMMERCIALISABILIT�
  ni d�AD�QUATION � UN OBJECTIF PARTICULIER. Consultez la Licence G�n�rale
  Publique GNU pour plus de d�tails.
  
  Vous devriez avoir re�u une copie de la Licence G�n�rale Publique GNU avec
  ce programme ; si ce n�est pas le cas, consultez :
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
                        if (connexion.IsOpen)
                        {                            
                            xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/table");
                            if (xmlNode != null)
                            {
                                string tablename = xmlNode.Attributes.GetNamedItem("id").Value;                                

                                string SQL =    "SELECT   c.cname,  " +
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
                                                "   WHERE ucc.table_name = '" + tablename + "' " +
                                                "     AND c.tname = ucc.table_name " +
                                                "     AND c.cname = ucc.column_name " +
                                                "ORDER BY c.tname, c.colno ";




                                uLib = new DGVQuery(dataGridViewOracleFields, connexion);
                                //uLib.Start(SQL);
                                startTime = DateTime.Now;
                                if (backgroundWorker1.IsBusy)
                                    backgroundWorker1.CancelAsync();
                                while (backgroundWorker1.IsBusy) ;
                                backgroundWorker1.RunWorkerAsync(SQL);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            /* ---------------------------------- */
            
            
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
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = uLib.Display(e.Argument.ToString(), worker, e);
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
            }
            toolStripProgressBarQuery.Visible = false;
        }

        private void toolStripButtonAddCol_Click(object sender, EventArgs e)
        {
            FormAddCol frmAddCol = new FormAddCol();
            if (frmAddCol.ShowDialog() == DialogResult.OK)
            {
                // Create the column
            }
            frmAddCol.Close();
            frmAddCol.Dispose();
        }

        private void toolStripButtonDeleteCol_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonModifyCol_Click(object sender, EventArgs e)
        {

        }
    }
}
