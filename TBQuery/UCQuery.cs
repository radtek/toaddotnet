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
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Util;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using PluginTypes;
using ULib;


namespace TBQuery
{
    public partial class UCQuery : UserControl, ITabPageAddOn
    {
        private DGVQuery uLib;
        private DateTime startTime;
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
            ExecuteQuery();
        }
        #region delegate
        
        private delegate void setNumberRecord(int num, int total);
        private void SetNumberRecord(int num, int total)
        {
            this.toolStripStatusLabelRecords.Text = string.Format("Record {0} of {1}", num, total);
        }

        private delegate void setElapsedTime(TimeSpan elapsed);
        private void SetElapsedTime(TimeSpan elapsed)
        {
            this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
        }
        #endregion        

        private void UCQuery_Load(object sender, EventArgs e)
        {            
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            
            ICSharpCode.TextEditor.Document.FileSyntaxModeProvider provider = new ICSharpCode.TextEditor.Document.FileSyntaxModeProvider(appPath);
            ICSharpCode.TextEditor.Document.HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);
            //textEditorControl1.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingManager.Manager.FindHighlighter("SQL");
            textEditorControl1.SetHighlighting("SQL");
            textEditorControl1.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(TextArea_DragDrop);
            textEditorControl1.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(TextArea_DragEnter);
            textEditorControl1.ActiveTextAreaControl.TextArea.Caret.PositionChanged +=new EventHandler(Caret_PositionChanged);
            textEditorControl1.ActiveTextAreaControl.TextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(TextArea_KeyUp);
        }

        private void TextArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecuteQuery();
            }
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            ICSharpCode.TextEditor.Caret caret = (ICSharpCode.TextEditor.Caret) sender;
            toolStripStatusLabelPosition.Text = string.Format("Line {0} Col {1}", caret.Line + 1, caret.Column + 1);
        }

        private void TextArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Copy;
            }
                
        }

        private void TextArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof (TreeNode)))
            {
                TreeNode tn = (TreeNode) e.Data.GetData(typeof (TreeNode));
                Console.WriteLine(textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position);
                int offset = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Offset;
                TextLocation currentLocation = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position;
                textEditorControl1.Text = textEditorControl1.Text.Insert(offset, tn.Text);
                textEditorControl1.ActiveTextAreaControl.Caret.Position = currentLocation;
            }
        }

/*
        private void richTextBoxOracleQuerySQL_TextChanged(object sender, EventArgs e)
        {
            //richTextBoxOracleQuerySQL.ProcessAllLines();
        }
*/

        private void toolStripButtonAbortQuery_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
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

        private void ExecuteQuery()
        {
            if (connexion.IsOpen)
            {
                try
                {
                    string sql = textEditorControl1.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    if (string.IsNullOrEmpty(sql))
                        sql = textEditorControl1.Text; //.Replace("select", "select * from (select ROWNUM n, ") +") s where s.n < 501";
                    
                    // Check if we a ddl or a dml
                    if (sql.ToLower().Contains("create") || 
                        sql.ToLower().Contains("drop") || 
                        sql.ToLower().Contains("alter") ||
                        sql.ToLower().Contains("declare") ||
                        sql.ToLower().Contains("begin"))
                    {
                        // It is a DDL then 
                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.Prepare();
                            int result = cmd.ExecuteNonQuery();

                            string SQL = "SELECT Line, Position, substr(text,1,200) text " +
                                        "FROM ALL_ERRORS " +
                                        "WHERE name={0} and type={1} and owner={2} " +
                                        "ORDER BY Sequence ";


                        }
                    }
                    else
                    {
                        uLib = new DGVQuery(dataGridViewOracleQueryData, connexion);
                        //uLib.Start(textEditorControl1.Text);
                        toolStripProgressBarQuery.Visible = true;
                        startTime = DateTime.Now;
                        if (backgroundWorker1.IsBusy)
                            backgroundWorker1.CancelAsync();
                        while (backgroundWorker1.IsBusy) ;

                        backgroundWorker1.RunWorkerAsync(sql);
                    }
                    
                }
                catch(Exception e)
                {
                    string errorMessage = e.Message;
                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                        errorMessage += "\n" + e.Message;
                    }
                    MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = errorMessage;
                }
                
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
                toolStripStatusLabelMessage.Text = string.Format("Aborted by user. {0} records found.", dataGridViewOracleQueryData.Rows.Count);
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

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            uLib = new DGVQuery(dataGridViewOracleQueryData, connexion);
            uLib.ClearData = false;
            //uLib.Start(string.Format("SELECT * FROM {0}", tablename));
            toolStripProgressBarQuery.Visible = true;
            toolStripProgressBarQuery.Value = 0;
            startTime = DateTime.Now;
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy) ;
            int rowsFetched = dataGridViewOracleQueryData.Rows.Count;
            string sql = textEditorControl1.Text;// .Replace("select", "select * from (select ROWNUM n, ");
            //sql = sql + string.Format(") s where s.n BETWEEN {0} AND {1}", rowsFetched + 1, rowsFetched + 500);
            backgroundWorker1.RunWorkerAsync(sql);
        }

        private void toolStripButtonToEnd_Click(object sender, EventArgs e)
        {
            uLib = new DGVQuery(dataGridViewOracleQueryData, connexion);
            uLib.ClearData = false;
            //uLib.Start(string.Format("SELECT * FROM {0}", tablename));
            toolStripProgressBarQuery.Visible = true;
            toolStripProgressBarQuery.Value = 0;
            startTime = DateTime.Now;
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy) ;
            int rowsFetched = dataGridViewOracleQueryData.Rows.Count;
            //backgroundWorker1.RunWorkerAsync(string.Format("SELECT * FROM (SELECT ROWNUM n, t.* FROM {0} t) s WHERE s.n > {1}", tablename, rowsFetched + 1));
        }              
    }
}
