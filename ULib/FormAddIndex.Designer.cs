namespace ULib
{
    partial class FormAddIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTableName = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageIndex = new System.Windows.Forms.TabPage();
            this.tabPageSQL = new System.Windows.Forms.TabPage();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.textBoxSql = new ICSharpCode.TextEditor.TextEditorControl();
            this.labelIndexName = new System.Windows.Forms.Label();
            this.textBoxIndexName = new System.Windows.Forms.TextBox();
            this.checkBoxUnique = new System.Windows.Forms.CheckBox();
            this.dataGridViewColumnName = new System.Windows.Forms.DataGridView();
            this.labelColumns = new System.Windows.Forms.Label();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTable = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageIndex.SuspendLayout();
            this.tabPageSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnName)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTableName
            // 
            this.labelTableName.AutoSize = true;
            this.labelTableName.Location = new System.Drawing.Point(85, 11);
            this.labelTableName.Name = "labelTableName";
            this.labelTableName.Size = new System.Drawing.Size(60, 13);
            this.labelTableName.TabIndex = 0;
            this.labelTableName.Text = "Tablename";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageIndex);
            this.tabControl1.Controls.Add(this.tabPageSQL);
            this.tabControl1.Location = new System.Drawing.Point(13, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(317, 319);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPageIndex
            // 
            this.tabPageIndex.Controls.Add(this.labelColumns);
            this.tabPageIndex.Controls.Add(this.dataGridViewColumnName);
            this.tabPageIndex.Controls.Add(this.checkBoxUnique);
            this.tabPageIndex.Controls.Add(this.textBoxIndexName);
            this.tabPageIndex.Controls.Add(this.labelIndexName);
            this.tabPageIndex.Location = new System.Drawing.Point(4, 22);
            this.tabPageIndex.Name = "tabPageIndex";
            this.tabPageIndex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIndex.Size = new System.Drawing.Size(309, 293);
            this.tabPageIndex.TabIndex = 0;
            this.tabPageIndex.Text = "Index";
            this.tabPageIndex.UseVisualStyleBackColor = true;
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.Controls.Add(this.textBoxSql);
            this.tabPageSQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSQL.Size = new System.Drawing.Size(289, 238);
            this.tabPageSQL.TabIndex = 1;
            this.tabPageSQL.Text = "SQL";
            this.tabPageSQL.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(257, 361);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonExecute.Location = new System.Drawing.Point(176, 361);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 3;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            // 
            // textBoxSql
            // 
            this.textBoxSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSql.IsReadOnly = false;
            this.textBoxSql.Location = new System.Drawing.Point(3, 3);
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(283, 232);
            this.textBoxSql.TabIndex = 1;
            // 
            // labelIndexName
            // 
            this.labelIndexName.AutoSize = true;
            this.labelIndexName.Location = new System.Drawing.Point(7, 7);
            this.labelIndexName.Name = "labelIndexName";
            this.labelIndexName.Size = new System.Drawing.Size(62, 13);
            this.labelIndexName.TabIndex = 0;
            this.labelIndexName.Text = "Index name";
            // 
            // textBoxIndexName
            // 
            this.textBoxIndexName.Location = new System.Drawing.Point(6, 23);
            this.textBoxIndexName.Name = "textBoxIndexName";
            this.textBoxIndexName.Size = new System.Drawing.Size(277, 20);
            this.textBoxIndexName.TabIndex = 1;
            // 
            // checkBoxUnique
            // 
            this.checkBoxUnique.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxUnique.AutoSize = true;
            this.checkBoxUnique.Location = new System.Drawing.Point(4, 270);
            this.checkBoxUnique.Name = "checkBoxUnique";
            this.checkBoxUnique.Size = new System.Drawing.Size(89, 17);
            this.checkBoxUnique.TabIndex = 2;
            this.checkBoxUnique.Text = "Unique Index";
            this.checkBoxUnique.UseVisualStyleBackColor = true;
            // 
            // dataGridViewColumnName
            // 
            this.dataGridViewColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewColumnName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumnName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName});
            this.dataGridViewColumnName.Location = new System.Drawing.Point(5, 69);
            this.dataGridViewColumnName.Name = "dataGridViewColumnName";
            this.dataGridViewColumnName.Size = new System.Drawing.Size(298, 195);
            this.dataGridViewColumnName.TabIndex = 3;
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(7, 50);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(78, 13);
            this.labelColumns.TabIndex = 4;
            this.labelColumns.Text = "Column\'s name";
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnName.HeaderText = "Column name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Width = 77;
            // 
            // labelTable
            // 
            this.labelTable.AutoSize = true;
            this.labelTable.Location = new System.Drawing.Point(13, 11);
            this.labelTable.Name = "labelTable";
            this.labelTable.Size = new System.Drawing.Size(66, 13);
            this.labelTable.TabIndex = 4;
            this.labelTable.Text = "Tablename :";
            // 
            // FormAddIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 396);
            this.Controls.Add(this.labelTable);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelTableName);
            this.MinimumSize = new System.Drawing.Size(350, 430);
            this.Name = "FormAddIndex";
            this.Text = "Add an index";
            this.Load += new System.EventHandler(this.FormAddIndex_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageIndex.ResumeLayout(false);
            this.tabPageIndex.PerformLayout();
            this.tabPageSQL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelTableName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageIndex;
        private System.Windows.Forms.TabPage tabPageSQL;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.CheckBox checkBoxUnique;
        private System.Windows.Forms.Label labelIndexName;
        public ICSharpCode.TextEditor.TextEditorControl textBoxSql;
        private System.Windows.Forms.Label labelColumns;
        public System.Windows.Forms.TextBox textBoxIndexName;
        public System.Windows.Forms.DataGridView dataGridViewColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.Label labelTable;
    }
}