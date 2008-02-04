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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor;
using PluginTypes;
using ULib;

namespace TBSource
{
    public partial class UCSource : UserControl, ITabPageAddOn
    {
        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        private DateTime startTime;
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        private TabPage tp;
        private TabControl tc;
        private static readonly int DEFAULT_TABPOSITION = 2;
        private int tabPosition = DEFAULT_TABPOSITION;


        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCSource()
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
            tp = new TabPage("Source");
            // Add the new tab page to the TabControl of the main window's application
            tc = tabControl;
            string TabPosition = Config.GetText("//alf-solution/plugins/TBSource/tab/position");
            if (string.IsNullOrEmpty(TabPosition))
            {
                Config.SaveText("/alf-solution/plugins/TBSource/tab/position", DEFAULT_TABPOSITION.ToString());
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
                    case "getfunction":
                    case "getprocedure":
                    case "getpackage":
                    case "getpackagespecs":
                    case "getpackagebodys":                    
                    case "gettrigger":
                        if (!tc.TabPages.Contains(tp))
                        {
                            tc.TabPages.Insert(0, tp);
                            tc.SelectedTab = tp;
                        }
                            
                        string typeAction = xmlNodeAction.InnerText.Substring(3).Replace(" ", "");
                        if (connexion.IsOpen)
                        {
                            xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/" + typeAction);
                            if (xmlNode != null)
                            {
                                string functionname = xmlNode.Attributes.GetNamedItem("id").Value;
                                string NodeName = xmlNode.Name.ToUpper();
                                if (NodeName.Contains("PACKAGEBODY"))
                                    NodeName = "PACKAGE BODY";
                                if (NodeName.Contains("PACKAGESPEC"))
                                    NodeName = "PACKAGE";
                                string SQL = "SELECT text " +
                                            "  FROM SYS.user_source " +
                                            " WHERE NAME = '" + functionname + "' AND TYPE = '" + NodeName + "' ";

                                //uLib = new DGVQuery(dataGridViewOracleFields, connexion);
                                //uLib.Start(SQL);
                                textEditorControl1.Text = "CREATE OR REPLACE ";
                                textEditorControl1.Refresh();                                
                                startTime = DateTime.Now;
                                if (backgroundWorker1.IsBusy)
                                    backgroundWorker1.CancelAsync();
                                while (backgroundWorker1.IsBusy) ;
                                backgroundWorker1.RunWorkerAsync(SQL);
                            }
                        }
                        break;
                    case "getpackagespec":
                    case "getpackagebody":
                    case "getproc_funcs":
                    case "getproc_func":
                    case "getparameters":
                    case "getparameter":
                        if (!tc.TabPages.Contains(tp))
                        {
                            tc.TabPages.Insert(0, tp);
                            tc.SelectedTab = tp;
                        }
                        break;
                    default:
                        if (tc.TabPages.Contains(tp))
                            tc.TabPages.Remove(tp);
                        break;
                }
            }                        
        }
        #endregion

        #region delegate
        
        private delegate void setSourceText(string text);
        private void SetSourceText(string text)
        {
            textEditorControl1.Text = textEditorControl1.Text + text;
        }

        private delegate void setElapsedTime(TimeSpan elapsed);
        private void SetElapsedTime(TimeSpan elapsed)
        {
            this.toolStripStatusLabelElapsedTime.Text = string.Format("Elapsed time: {0} s", elapsed.TotalSeconds);
        }
        
        #endregion
        
        private void UCSource_Load(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            ICSharpCode.TextEditor.Document.FileSyntaxModeProvider provider = new ICSharpCode.TextEditor.Document.FileSyntaxModeProvider(appPath);
            ICSharpCode.TextEditor.Document.HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);
            //textEditorControl1.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingManager.Manager.FindHighlighter("SQL");
            textEditorControl1.SetHighlighting("SQL");
            textEditorControl1.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(TextArea_DragDrop);
            textEditorControl1.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(TextArea_DragEnter);
            textEditorControl1.ActiveTextAreaControl.TextArea.Caret.PositionChanged += new EventHandler(Caret_PositionChanged);
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
                Clipboard.SetText(ta.SelectionManager.SelectedText);
                ta.SelectionManager.RemoveSelectedText();
            }
            if (e.KeyCode == Keys.C && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                Clipboard.SetText(ta.SelectionManager.SelectedText);
            }
            if (e.KeyCode == Keys.X && ctrlDonw)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                ta.InsertString(Clipboard.GetText());
            }
        }        

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            ICSharpCode.TextEditor.Caret caret = (ICSharpCode.TextEditor.Caret)sender;
            toolStripStatusLabelPosition.Text = string.Format("Line {0} Col {1}", caret.Line + 1, caret.Column + 1);
        }

        #region dragdrop
        private void TextArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Copy;
            }

        }

        private void TextArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode tn = (TreeNode)e.Data.GetData(typeof(TreeNode));
                Console.WriteLine(textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position);
                int offset = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Offset;
                TextLocation currentLocation = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position;
                textEditorControl1.Text = textEditorControl1.Text.Insert(offset, tn.Text);
                textEditorControl1.ActiveTextAreaControl.Caret.Position = currentLocation;
            }
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            using (DbCommand cmd = connexion.Cnn.CreateCommand())
            {
                string SQL = e.Argument.ToString();
                int NumRec = 0;
                try
                {
                    string SQLCount = "SELECT count(*) " + SQL.Substring(SQL.ToUpper().IndexOf("FROM"));
                    cmd.CommandText = SQLCount; // string.Format("SELECT count(*) FROM {0}", SelectedTable);
                    cmd.Prepare();
                    NumRec = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception erreur)
                {
                    NumRec = 0;
                }

                cmd.CommandText = SQL;
                cmd.Prepare();
                //int colno = 0;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    int CurrentNumRec = 0;
                    
                    while (rd.Read() && !worker.CancellationPending)
                    {
                        CurrentNumRec++;
                        string text = rd.GetString(0);
                        if (textEditorControl1.InvokeRequired)
                        {
                            textEditorControl1.Invoke(new setSourceText(SetSourceText), new object[] {text});
                        }
                        else
                        {
                            SetSourceText(rd.GetString(0));
                        }
                        if (NumRec != 0)
                        {
                            int percentComplete = (int)((float)CurrentNumRec / (float)NumRec * 100);
                            worker.ReportProgress(percentComplete);
                        }
                        else
                        {
                            worker.ReportProgress(CurrentNumRec % 100);
                        } 
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        e.Result = string.Format("Aborted by user.");
                    }
                    else
                    {
                        e.Result = string.Format("Function retrieved completely.");
                    }

                    rd.Close();
                }

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            toolStripProgressBarQuery.Value = e.ProgressPercentage;
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

        private void toolStripButtonExecQuery_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }

        private void ExecuteQuery()
        {
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
                        string sql = textEditorControl1.Text.ToLower();
                        // Remove all comments from the sql script
                        Regex regReplaceAll = new Regex(@"(/\*(.|\s)*?\*/)|\-\-(.)*\s?");
                        sql = regReplaceAll.Replace(sql, "").Trim().Replace("\r", "");
                        
                        
                        cmd.CommandText = sql;

                        cmd.Prepare();
                        //int colno = 0;
                        
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
                                    textBoxMessage.Text +=string.Format("Line {0} Col {1} : {2}{3}", Rerr.GetValue(0), Rerr.GetValue(1), Rerr.GetString(2), Environment.NewLine);
                                }
                                if (string.IsNullOrEmpty(textBoxMessage.Text))
                                    textBoxMessage.Text = "Successfully compiled";
                                Rerr.Close();
                            }
                        }
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
                this.textBoxMessage.Text = errorMessage;
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
