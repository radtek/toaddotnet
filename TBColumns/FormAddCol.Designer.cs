namespace TBColumns
{
    partial class FormAddCol
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.tabPageSql = new System.Windows.Forms.TabPage();
            this.labelTablename = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.labelColumnname = new System.Windows.Forms.Label();
            this.textBoxcolumnName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.radioButtonNullable = new System.Windows.Forms.RadioButton();
            this.radioButtonNotNullable = new System.Windows.Forms.RadioButton();
            this.textBoxSql = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.tabPageSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageOptions);
            this.tabControl1.Controls.Add(this.tabPageSql);
            this.tabControl1.Location = new System.Drawing.Point(12, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(322, 274);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.radioButtonNotNullable);
            this.tabPageOptions.Controls.Add(this.radioButtonNullable);
            this.tabPageOptions.Controls.Add(this.textBoxDefaultValue);
            this.tabPageOptions.Controls.Add(this.label5);
            this.tabPageOptions.Controls.Add(this.textBox2);
            this.tabPageOptions.Controls.Add(this.label4);
            this.tabPageOptions.Controls.Add(this.textBox1);
            this.tabPageOptions.Controls.Add(this.label3);
            this.tabPageOptions.Controls.Add(this.label2);
            this.tabPageOptions.Controls.Add(this.comboBoxType);
            this.tabPageOptions.Controls.Add(this.label1);
            this.tabPageOptions.Controls.Add(this.textBoxcolumnName);
            this.tabPageOptions.Controls.Add(this.labelColumnname);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(314, 248);
            this.tabPageOptions.TabIndex = 0;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // tabPageSql
            // 
            this.tabPageSql.Controls.Add(this.textBoxSql);
            this.tabPageSql.Location = new System.Drawing.Point(4, 22);
            this.tabPageSql.Name = "tabPageSql";
            this.tabPageSql.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSql.Size = new System.Drawing.Size(314, 248);
            this.tabPageSql.TabIndex = 1;
            this.tabPageSql.Text = "Sql";
            this.tabPageSql.UseVisualStyleBackColor = true;
            // 
            // labelTablename
            // 
            this.labelTablename.AutoSize = true;
            this.labelTablename.Location = new System.Drawing.Point(13, 13);
            this.labelTablename.Name = "labelTablename";
            this.labelTablename.Size = new System.Drawing.Size(65, 13);
            this.labelTablename.TabIndex = 1;
            this.labelTablename.Text = "Table Name";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(255, 314);
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
            this.buttonExecute.Location = new System.Drawing.Point(174, 314);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 3;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            // 
            // labelColumnname
            // 
            this.labelColumnname.AutoSize = true;
            this.labelColumnname.Location = new System.Drawing.Point(4, 7);
            this.labelColumnname.Name = "labelColumnname";
            this.labelColumnname.Size = new System.Drawing.Size(73, 13);
            this.labelColumnname.TabIndex = 0;
            this.labelColumnname.Text = "Column Name";
            // 
            // textBoxcolumnName
            // 
            this.textBoxcolumnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcolumnName.Location = new System.Drawing.Point(7, 24);
            this.textBoxcolumnName.Name = "textBoxcolumnName";
            this.textBoxcolumnName.Size = new System.Drawing.Size(301, 20);
            this.textBoxcolumnName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "VARCHAR2",
            "NUMBER",
            "INTEGER",
            "DATE",
            "CLOB",
            "CHAR",
            "BLOB",
            "LONG"});
            this.comboBoxType.Location = new System.Drawing.Point(10, 68);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(298, 21);
            this.comboBoxType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Scale";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 113);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(69, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = ".";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(86, 116);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(35, 20);
            this.textBox2.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Default value";
            // 
            // textBoxDefaultValue
            // 
            this.textBoxDefaultValue.Location = new System.Drawing.Point(10, 157);
            this.textBoxDefaultValue.Name = "textBoxDefaultValue";
            this.textBoxDefaultValue.Size = new System.Drawing.Size(298, 20);
            this.textBoxDefaultValue.TabIndex = 10;
            // 
            // radioButtonNullable
            // 
            this.radioButtonNullable.AutoSize = true;
            this.radioButtonNullable.Location = new System.Drawing.Point(158, 96);
            this.radioButtonNullable.Name = "radioButtonNullable";
            this.radioButtonNullable.Size = new System.Drawing.Size(63, 17);
            this.radioButtonNullable.TabIndex = 11;
            this.radioButtonNullable.TabStop = true;
            this.radioButtonNullable.Text = "Nullable";
            this.radioButtonNullable.UseVisualStyleBackColor = true;
            // 
            // radioButtonNotNullable
            // 
            this.radioButtonNotNullable.AutoSize = true;
            this.radioButtonNotNullable.Location = new System.Drawing.Point(158, 120);
            this.radioButtonNotNullable.Name = "radioButtonNotNullable";
            this.radioButtonNotNullable.Size = new System.Drawing.Size(83, 17);
            this.radioButtonNotNullable.TabIndex = 12;
            this.radioButtonNotNullable.TabStop = true;
            this.radioButtonNotNullable.Text = "Not Nullable";
            this.radioButtonNotNullable.UseVisualStyleBackColor = true;
            // 
            // textBoxSql
            // 
            this.textBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSql.Location = new System.Drawing.Point(7, 7);
            this.textBoxSql.Multiline = true;
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(301, 235);
            this.textBoxSql.TabIndex = 0;
            // 
            // FormAddCol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 349);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelTablename);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormAddCol";
            this.Text = "FormAddCol";
            this.tabControl1.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageOptions.PerformLayout();
            this.tabPageSql.ResumeLayout(false);
            this.tabPageSql.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxcolumnName;
        private System.Windows.Forms.Label labelColumnname;
        private System.Windows.Forms.TabPage tabPageSql;
        private System.Windows.Forms.Label labelTablename;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.RadioButton radioButtonNotNullable;
        private System.Windows.Forms.RadioButton radioButtonNullable;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxSql;
    }
}