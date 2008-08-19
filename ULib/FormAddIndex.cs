using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ULib
{
    public partial class FormAddIndex : Form
    {
        public FormAddIndex()
        {
            InitializeComponent();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Text == "SQL")
            {
                if (checkBoxUnique.Checked)
                    textBoxSql.Text = "CREATE UNIQUE INDEX ";
                else
                    textBoxSql.Text = "CREATE INDEX ";
                textBoxSql.Text += string.Format("{0} ON {1} (", textBoxIndexName.Text, labelTableName.Text);
                for (int i = 0; i < dataGridViewColumnName.Rows.Count; i++ )
                {
                    if (dataGridViewColumnName.Rows[i].Cells[0].Value != null)
                    {
                        if (i > 0)
                            textBoxSql.Text += ", ";
                        textBoxSql.Text += string.Format("{0}", dataGridViewColumnName.Rows[i].Cells[0].Value.ToString());   
                    }                    
                }

                textBoxSql.Text += ")";
            }
        }

        private void FormAddIndex_Load(object sender, EventArgs e)
        {            
        }
    }
}