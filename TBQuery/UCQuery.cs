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
using System.Data;
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
        
        private TabControl tc;
        private static readonly int DEFAULT_TABPOSITION = 10;
        private int tabPosition = DEFAULT_TABPOSITION;

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
            tc = tabControl;
            string TabPosition = Config.GetText("//alf-solution/plugins/TBQuery/tab/position");
            if (string.IsNullOrEmpty(TabPosition))
            {
                Config.SaveText("/alf-solution/plugins/TBQuery/tab/position", DEFAULT_TABPOSITION.ToString());
                tabPosition = DEFAULT_TABPOSITION;
            }
            else
            {
                tabPosition = Convert.ToInt32(TabPosition);
            }
            // Insert the new tab page to the TabControl of the main window's application           
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
            textEditorControl1.ActiveTextAreaControl.TextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(TextArea_KeyDown);

        }

        private bool ctrlDonw = false;

        private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrlDonw = true;
                this.toolStripStatusLabelMessage.Text = "Ctrl";
            }
                
        }

        private void TextArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecuteQuery();
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrlDonw = false;
                this.toolStripStatusLabelMessage.Text = "";
            }
            if (e.KeyCode == Keys.A && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                TextLocation CurrentLocation = ta.Caret.Position;
                int TotalNumberOfLines = ta.Document.TotalNumberOfLines;
                int LastLineTextLength = ta.Document.GetLineSegment(TotalNumberOfLines - 1).Length;
                ta.SelectionManager.SetSelection(new TextLocation(0, 0), new TextLocation(TotalNumberOfLines, LastLineTextLength));
            }
            if (e.KeyCode == Keys.X && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                if (ta.SelectionManager.SelectedText != null)
                {
                    Clipboard.SetText(ta.SelectionManager.SelectedText);
                    ta.SelectionManager.RemoveSelectedText();
                    ta.Refresh();    
                }
                
            }
            if (e.KeyCode == Keys.C && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                if (ta.SelectionManager.SelectedText != null)
                {
                    Clipboard.SetText(ta.SelectionManager.SelectedText);
                }
                
            }
            if (e.KeyCode == Keys.V && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                ta.InsertString(Clipboard.GetText());
                ta.Refresh();
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
                //Console.WriteLine(textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position);
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
                    sql = sql.Replace("\r", "");

                    // Check if we a ddl or a dml
                    if (sql.ToLower().Contains("create") || 
                        sql.ToLower().Contains("drop") || 
                        sql.ToLower().Contains("alter") ||
                        sql.ToLower().Contains("declare") ||
                        sql.ToLower().Contains("begin"))
                    {
                        // It is a DDL then
                        // Enabling dbms_output.
                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = "begin dbms_output.enable(20000); end;";
                            cmd.Prepare();
                            int result = cmd.ExecuteNonQuery();
                        }

                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.Prepare();
                            int result = cmd.ExecuteNonQuery();

                            if (sql.Contains("create"))
                            {
                                string objName;
                                string objType;
                                string objOwner = connexion.OracleConnexion.UserId;
                                if (sql.Contains("package"))
                                {
                                    string[] sqlSplited = sql.Split(new string[] { "as" }, StringSplitOptions.None);
                                    sqlSplited = sqlSplited[0].Trim().Split(new string[] { " " }, StringSplitOptions.None);
                                    objName = sqlSplited[sqlSplited.Length - 1];
                                    if (sql.Contains("body"))
                                    {
                                        cmd.CommandText =
                                            string.Format("ALTER {0} {1} COMPILE BODY", "PACKAGE", objName);
                                        objType = "PACKAGE BODY";
                                    }
                                    else
                                    {
                                        cmd.CommandText =
                                        string.Format("ALTER {0} {1} COMPILE", "PACKAGE", objName);
                                        objType = "PACKAGE";
                                    }

                                }
                                else
                                {
                                    // Find the name of the procedure/function/package
                                    string[] sqlSplited = sql.Split(new string[] { " is", "\nis", "is\n" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (sqlSplited[0].Trim().Contains("function"))
                                        objType = "function";
                                    else
                                        if (sqlSplited[0].Trim().Contains("procedure"))
                                            objType = "procedure";
                                        else
                                            if (sqlSplited[0].Trim().Contains("trigger"))
                                                objType = "trigger";
                                            else
                                                objType = "";
                                    sqlSplited = sqlSplited[0].Trim().Split(new string[] { "function", "procedure", "trigger" }, StringSplitOptions.None);
                                    sqlSplited = sqlSplited[1].Trim().Split(new string[] { "(", " ", "\n" }, StringSplitOptions.None);
                                    objName = sqlSplited[0].Trim();
                                    cmd.CommandText = string.Format("ALTER {0} {1} COMPILE", objType, objName);
                                }


                                cmd.Prepare();
                                result = cmd.ExecuteNonQuery();

                                using (DbCommand cmdAllError = connexion.Cnn.CreateCommand())
                                {
                                    string SQL = "SELECT Line, Position, text " +
                                                    "FROM ALL_ERRORS " +
                                                    "WHERE name='{0}' and type='{1}' and owner='{2}' " +
                                                    "ORDER BY Sequence ";
                                    cmdAllError.CommandText = string.Format(SQL, objName.ToUpper(), objType.ToUpper(), objOwner.ToUpper());
                                    cmdAllError.Prepare();
                                    DbDataReader Rerr = cmdAllError.ExecuteReader();
                                    textBoxMessage.Text = "";
                                    while (Rerr.Read())
                                    {
                                        textBoxMessage.Text += string.Format("Line {0} Col {1} : {2}{3}", Rerr.GetValue(0), Rerr.GetValue(1), Rerr.GetString(2), Environment.NewLine);
                                    }
                                    if (string.IsNullOrEmpty(textBoxMessage.Text))
                                        textBoxMessage.Text = "Successfully compiled";
                                    Rerr.Close();
                                }
                            }

                            using(DbCommand cmdDbms = connexion.Cnn.CreateCommand())
                            {
                                cmdDbms.CommandText = "begin dbms_output.get_line(:line, :statut); end;";
                                DbParameter lineParameter = cmdDbms.CreateParameter();
                                lineParameter.Direction = ParameterDirection.Output;
                                lineParameter.DbType = DbType.String;
                                lineParameter.ParameterName = ":line";
                                lineParameter.Size = 255;
                                cmdDbms.Parameters.Add(lineParameter);
                                
                                DbParameter statusParameter = cmdDbms.CreateParameter();
                                statusParameter.Direction = ParameterDirection.Output;
                                statusParameter.DbType = DbType.Int32;
                                statusParameter.ParameterName = ":statut";
                                statusParameter.Size = 12;
                                cmdDbms.Parameters.Add(statusParameter);
                                bool run = true;
                                while (run)
                                {
                                    cmdDbms.ExecuteNonQuery();
                                    if (Convert.ToInt32(statusParameter.Value) == 1)
                                        run = false;
                                    else
                                        textBoxMessage.Text += lineParameter.Value.ToString() + Environment.NewLine;
                                }
                                

                            }
                        }
                        // Disabling dbms_output.
                        using (DbCommand cmd = connexion.Cnn.CreateCommand())
                        {
                            cmd.CommandText = "begin dbms_output.disable; end;";
                            cmd.Prepare();
                            int result = cmd.ExecuteNonQuery();
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
                    textBoxMessage.Text = errorMessage;
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
