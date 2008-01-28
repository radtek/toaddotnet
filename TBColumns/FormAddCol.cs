using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TBColumns
{
    public partial class FormAddCol : Form
    {
        private string tablename;
        public FormAddCol()
        {
            InitializeComponent();
        }

        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Text == "Sql")
            {
                textBoxSql.Text =
                    string.Format("ALTER TABLE {0} ADD (\n{1} {2}", tablename, textBoxcolumnName.Text, comboBoxType.Text);
                textBoxSql.Text += (!string.IsNullOrEmpty(textBoxSize.Text) ? string.Format("({0}", textBoxSize.Text) : "");
                textBoxSql.Text += (!string.IsNullOrEmpty(textBoxScale.Text) ? string.Format(",{0}", textBoxScale.Text) : "");
                textBoxSql.Text += (!string.IsNullOrEmpty(textBoxSize.Text) ? ")"  : "");
                textBoxSql.Text += (!string.IsNullOrEmpty(textBoxDefaultValue.Text) ? string.Format(" DEFAULT {0}", textBoxDefaultValue.Text) : "");
                textBoxSql.Text += ")";
            }
        }

        private void FormAddCol_Load(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            ICSharpCode.TextEditor.Document.FileSyntaxModeProvider provider = new ICSharpCode.TextEditor.Document.FileSyntaxModeProvider(appPath);
            ICSharpCode.TextEditor.Document.HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);
            //textEditorControl1.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingManager.Manager.FindHighlighter("SQL");
            textBoxSql.SetHighlighting("SQL");  
        }
    }
}