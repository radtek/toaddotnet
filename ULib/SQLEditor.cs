using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Intellisense;

namespace ULib
{
    public partial class SQLEditor : UserControl
    {
        private Intellisense.Intelli intellisense;
        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        private DGVQuery uLib;
        private DateTime startTime;
        

        #region Initialize
        public SQLEditor()
        {
            InitializeComponent();
        }

        public Connexion.Connexion SetConnexion
        {
            get { return connexion; }
            set { connexion = value; }
        }

        public String Text
        {
            get { return textEditorControl1.Text; }
            set { textEditorControl1.Text = value; }
        }

        private void SQLEditor_Load(object sender, EventArgs e)
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
            textEditorControl1.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);
            intellisense = new Intelli(this.listBox1);
        }
        #endregion

        private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == '(')
            {
                DisplayIntellisense();
            }
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.ControlKey)
            {
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
                this.toolStripStatusLabelMessage.Text = "";
            }
            if (e.KeyCode == Keys.A && e.Control)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                TextLocation CurrentLocation = ta.Caret.Position;
                int TotalNumberOfLines = ta.Document.TotalNumberOfLines;
                int LastLineTextLength = ta.Document.GetLineSegment(TotalNumberOfLines - 1).Length;
                ta.SelectionManager.SetSelection(new TextLocation(0, 0), new TextLocation(TotalNumberOfLines, LastLineTextLength));
            }
            if (e.KeyCode == Keys.X && e.Control)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                if (ta.SelectionManager.SelectedText != null)
                {
                    Clipboard.SetText(ta.SelectionManager.SelectedText);
                    ta.SelectionManager.RemoveSelectedText();
                    ta.Refresh();
                }

            }
            if (e.KeyCode == Keys.C && e.Control)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                if (ta.SelectionManager.SelectedText != null)
                {
                    Clipboard.SetText(ta.SelectionManager.SelectedText);
                }

            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                TextArea ta = textEditorControl1.ActiveTextAreaControl.TextArea;
                ta.InsertString(Clipboard.GetText());
                ta.Refresh();
            }

            //if (e.KeyData .KeyCode == Keys.OemPeriod || e.KeyValue == 53 || (e.KeyCode == Keys.Tab && e.Control))
            //{
            //    DisplayIntellisense();
            //}            
        }

        private void DisplayIntellisense()
        {
            using (DbCommand cmd = connexion.Cnn.CreateCommand())
            {
                string sql = "select object_type  " +
                            "  from obj " +
                            " where object_name = '{0}' ";
                string PreviousWord =
                    GetWordAt(textEditorControl1.Document, textEditorControl1.ActiveTextAreaControl.Caret.Offset);
                cmd.CommandText = string.Format(sql, PreviousWord.ToUpper());
                cmd.Prepare();
                string Obj_type = Convert.ToString(cmd.ExecuteScalar());
                intellisense.Clear();
                if (!string.IsNullOrEmpty(Obj_type))
                {

                    string SQL = null;
                    switch (Obj_type)
                    {
                        case "TABLE":
                        case "VIEW":
                            SQL = "SELECT cname " +
                                    "FROM COL " +
                                    "WHERE tname='{0}' " +
                                    "ORDER BY 1 ";
                            break;
                        case "PROCEDURE":
                        case "FUNCTION":
                            string ParameterSQL
                                    = "select lower(argument_name||' '||in_out||' '||data_type) param  " +
                                        "  from   all_arguments aa   " +
                                        " where aa.object_name ='{0}'              " +
                                        "   and aa.POSITION > 0          " +
                                        "order by Sequence  ";


                            using (DbCommand cmdParam = connexion.Cnn.CreateCommand())
                            {
                                cmdParam.CommandText =
                                    string.Format(ParameterSQL, PreviousWord.ToUpper());
                                cmdParam.Prepare();

                                string param = null;
                                DbDataReader rdParam = cmdParam.ExecuteReader();
                                while (rdParam.Read())
                                {
                                    if (!string.IsNullOrEmpty(param))
                                    {
                                        param += ", ";
                                    }
                                    param += rdParam.GetString(0);
                                }
                                if (!string.IsNullOrEmpty(param))
                                {
                                    intellisense.AddWord(string.Format("({0})", param));
                                }
                            }
                            break;
                        case "PACKAGE":
                            SQL = "Select distinct " +
                                "       object_name /*, position, data_type, overload, argument_name, " +
                                "       data_level, data_length, data_precision, data_scale, default_value, " +
                                "       in_out, object_id, sequence */" +
                                "from   all_arguments " +
                                "where  object_id = (select object_id " +
                                "         from sys.user_objects  " +
                                "         where object_name ='{0}' " +
                                "         and object_type in ('PACKAGE')) " +
                                "order by Object_Name --, Overload, Sequence ";
                            break;
                    }
                    if (!string.IsNullOrEmpty(SQL))
                    {
                        cmd.CommandText = string.Format(SQL, PreviousWord.ToUpper());
                        cmd.Prepare();
                        DbDataReader rd = cmd.ExecuteReader();
                        textBoxMessage.Text = "";

                        while (rd.Read())
                        {
                            if (Obj_type == "PACKAGE")
                            {
                                string ParameterSQL
                                        = "select lower(argument_name||' '||in_out||' '||data_type) param " +
                                            "  from   all_arguments aa  " +
                                            " where  object_id = (select object_id  " +
                                            "         from sys.user_objects   " +
                                            "         where object_name ='{0}'  " +
                                            "         and object_type in ('PACKAGE', 'PROCEDURE', 'FUNCTION'))  " +
                                            "         and object_name = '{1}'          " +
                                            "         connect by prior aa.POSITION = aa.POSITION - 1 " +
                                            "         start with position = 1          " +
                                            "order by Sequence ";

                                using (DbCommand cmdParam = connexion.Cnn.CreateCommand())
                                {
                                    cmdParam.CommandText =
                                        string.Format(ParameterSQL, PreviousWord.ToUpper(), rd.GetString(0));
                                    cmdParam.Prepare();

                                    string param = null;
                                    DbDataReader rdParam = cmdParam.ExecuteReader();
                                    while (rdParam.Read())
                                    {
                                        if (!string.IsNullOrEmpty(param))
                                        {
                                            param += ", ";
                                        }
                                        param += rdParam.GetString(0);
                                    }
                                    if (!string.IsNullOrEmpty(param))
                                    {
                                        intellisense.AddWord(string.Format("{0}({1})", rd.GetString(0), param));
                                    }
                                    else
                                    {
                                        intellisense.AddWord(rd.GetString(0));
                                    }
                                }
                            }
                            else
                            {
                                intellisense.AddWord(rd.GetString(0));
                            }

                        }
                        rd.Close();
                    }
                }
            }

            if (intellisense.Count() > 0)
            {
                intellisense.Location = new Point(textEditorControl1.ActiveTextAreaControl.Caret.ScreenPosition.X, textEditorControl1.ActiveTextAreaControl.Caret.ScreenPosition.Y + 20);
                intellisense.Visible = true;

                this.listBox1.Focus();
            }
            else
            {
                intellisense.Visible = false;
                textEditorControl1.Focus();
            }
        }

        public string GetCurrentWord()
        {
            int pos = textEditorControl1.ActiveTextAreaControl.Caret.Offset;
            string word = GetWordAt(textEditorControl1.Document, pos);
            if (word.Length == 0 && (textEditorControl1.Text.Length > pos - 1))
                word = GetWordAt(textEditorControl1.Document, pos - 1);

            return word.Trim();
        }

        public static bool IsLetterDigitOrUnderscore(char c)
        {
            if (!Char.IsLetterOrDigit(c))
            {
                return c == '_';
            }
            return true;
        }

        static bool IsWordPart(char ch)
        {
            return IsLetterDigitOrUnderscore(ch); // || ch == '.';
        }

        public static string GetWordAt(IDocument document, int offset)
        {
            if (offset < 0 || offset > document.TextLength || !IsWordPart(document.GetCharAt(offset - 1)))
            {
                return String.Empty;
            }
            int startOffset = offset;
            int endOffset = offset;
            while (startOffset > 0 && IsWordPart(document.GetCharAt(startOffset - 1)))
            {
                --startOffset;
            }

            while (endOffset < document.TextLength - 1 && IsWordPart(document.GetCharAt(endOffset + 1)))
            {
                ++endOffset;
            }

            //Debug.Assert(endOffset >= startOffset);
            return document.GetText(startOffset, endOffset - startOffset);
        }

        public static string GetPreviousWordAt(IDocument document, int offset)
        {
            int startOffset = offset;
            //int endOffset = offset;
            while (startOffset > 0 && IsWordPart(document.GetCharAt(startOffset - 1)))
            {
                startOffset--;
            }
            startOffset -= 2;
            return GetWordAt(document, startOffset);

        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            ICSharpCode.TextEditor.Caret caret = (ICSharpCode.TextEditor.Caret)sender;
            toolStripStatusLabelPosition.Text = string.Format("Ln {0} Col {1}", caret.Line + 1, caret.Column + 1);
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
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode tn = (TreeNode)e.Data.GetData(typeof(TreeNode));
                //Console.WriteLine(textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position);
                int offset = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Offset;
                TextLocation currentLocation = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position;
                textEditorControl1.Text = textEditorControl1.Text.Insert(offset, tn.Text);
                currentLocation.X += tn.Text.Length;
                textEditorControl1.ActiveTextAreaControl.Caret.Position = currentLocation;
            }
        }

        private void listBoxIntellisense_DoubleClick(object sender, EventArgs e)
        {
            GetIntellisense();
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetIntellisense();
            }
            if (e.KeyCode == Keys.Escape)
            {
                intellisense.Visible = false;
            }
        }

        private void GetIntellisense()
        {
            int offset = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Offset;
            //string CurrentWord = this.GetCurrentWord();
            TextLocation currentLocation = textEditorControl1.ActiveTextAreaControl.TextArea.Caret.Position;
            //Select(offset - CurrentWord.Length, CurrentWord.Length);
            //offset -= CurrentWord.Length;
            if (offset < 0)
                offset = 0;
            if (offset > textEditorControl1.Text.Length)
                offset = textEditorControl1.Text.Length;
            if (textEditorControl1.Text.Substring(offset - 1, 1) == "(")
            {
                offset--;
                textEditorControl1.Text = textEditorControl1.Text.Remove(offset, 1);
                //CurrentWord += "(";
            }

            //textEditorControl1.Text = textEditorControl1.Text.Remove(offset, CurrentWord.Length);            
            textEditorControl1.Text = textEditorControl1.Text.Insert(offset, listBox1.SelectedItem.ToString());
            currentLocation.X += listBox1.SelectedItem.ToString().Length;
            textEditorControl1.ActiveTextAreaControl.Caret.Position = currentLocation;
            intellisense.Visible = false;
            textEditorControl1.Focus();
        }

        public void Select(int startPos, int length)
        {
            TextLocation startPoint = textEditorControl1.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos);
            TextLocation endPoint = textEditorControl1.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos + length);

            textEditorControl1.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPoint, endPoint);
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
                    if (sql.ToLower().Contains("create ") ||
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

                            if (sql.ToLower().Contains("create"))
                            {
                                string objName;
                                string objType;
                                string objOwner = connexion.OracleConnexion.UserId;
                                if (sql.ToLower().Contains("package"))
                                {
                                    string[] sqlSplited = sql.Split(new string[] { "as" }, StringSplitOptions.None);
                                    sqlSplited = sqlSplited[0].Trim().Split(new string[] { " " }, StringSplitOptions.None);
                                    objName = sqlSplited[sqlSplited.Length - 1];
                                    if (sql.ToLower().Contains("body"))
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
                                    string[] sqlSplited = sql.ToLower().Split(new string[] { " is", "\nis", "is\n" }, StringSplitOptions.RemoveEmptyEntries);
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

                            using (DbCommand cmdDbms = connexion.Cnn.CreateCommand())
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
                catch (Exception e)
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
