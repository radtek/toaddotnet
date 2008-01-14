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

        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
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
            foreach (XmlNode xmlNodeAction in xmlData.GetElementsByTagName("action"))
            {
                switch (xmlNodeAction.InnerText)
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
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        private void toolStripButtonExecQuery_Click(object sender, EventArgs e)
        {            
            if (connexion.IsOpen)
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
