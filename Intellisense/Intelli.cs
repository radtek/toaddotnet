using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;


namespace Intellisense
{
    public partial class Intelli : ListBox
    {
        private ListBox listBox1;
        //private bool visible;
        List<Keyword> keyword = new List<Keyword>();

        public Intelli()
        {
            InitializeComponent();
            InitListKeyword();
        }

        public Intelli(ListBox lb)
        {
            InitializeComponent();
            listBox1 = lb;
            InitListKeyword();
        }

        public Intelli(Control container)
        {
            container.Controls.Add(this);

            InitializeComponent();
            InitListKeyword();
        }

        public void InitListKeyword()
        {
            Keyword kword = new Keyword();
            kword.Name = "SELECT";
            kword.Membre = "DML";
            keyword.Add(kword);
            keyword.Add(new Keyword("FORM", "", "DML"));
            keyword.Add(new Keyword("WHERE", "", "DML"));
            keyword.Add(new Keyword("AND", "", "DML"));
            keyword.Add(new Keyword("OR", "", "DML"));
            keyword.Add(new Keyword("ORDER BY", "", "DML"));
        }

        public bool Visible
        {
            get { return this.listBox1.Visible; }
            set { 
                //visible = value;
                this.listBox1.Visible = value;
                this.listBox1.ResumeLayout(true);
            }
        }

        public System.Drawing.Point Location
        {
            get { return this.listBox1.Location;  }
            set { 
                this.listBox1.Location = value; 
                this.listBox1.ResumeLayout(true);
            }
        }

        public int AddWord(string word)
        {
            return this.listBox1.Items.Add(word);
        }

        public void Clear()
        {
            this.listBox1.Items.Clear();
        }

        public int Count()
        {
            return listBox1.Items.Count;
        }

        public int Filter(string word)
        {
            Clear();
            if (string.IsNullOrEmpty(word))
                return 0;
            for (int i=0;i < keyword.Count; i++)
            {
                if (keyword[i].Name.ToLower().Contains(word.ToLower()))
                {
                    AddWord(keyword[i].Name);                    
                }
                    
            }
            this.listBox1.Sorted = true;
            return this.listBox1.Items.Count;
        }
    }
}
