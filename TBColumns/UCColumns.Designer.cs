namespace TBColumns
{
    partial class UCColumns
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ColumnsGroupBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewOracleFields = new System.Windows.Forms.DataGridView();
            this.textBoxFieldComment = new System.Windows.Forms.TextBox();
            this.ColumnsGroupBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).BeginInit();
            this.SuspendLayout();
            // 
            // ColumnsGroupBox
            // 
            this.ColumnsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnsGroupBox.Controls.Add(this.splitContainer1);
            this.ColumnsGroupBox.Location = new System.Drawing.Point(4, 4);
            this.ColumnsGroupBox.Name = "ColumnsGroupBox";
            this.ColumnsGroupBox.Size = new System.Drawing.Size(346, 321);
            this.ColumnsGroupBox.TabIndex = 0;
            this.ColumnsGroupBox.TabStop = false;
            this.ColumnsGroupBox.Text = "Columns";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(6, 19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewOracleFields);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxFieldComment);
            this.splitContainer1.Size = new System.Drawing.Size(333, 296);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridViewOracleFields
            // 
            this.dataGridViewOracleFields.AllowUserToAddRows = false;
            this.dataGridViewOracleFields.AllowUserToDeleteRows = false;
            this.dataGridViewOracleFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOracleFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOracleFields.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewOracleFields.Name = "dataGridViewOracleFields";
            this.dataGridViewOracleFields.ReadOnly = true;
            this.dataGridViewOracleFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOracleFields.Size = new System.Drawing.Size(323, 195);
            this.dataGridViewOracleFields.TabIndex = 1;
            this.dataGridViewOracleFields.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleFields_RowEnter);
            this.dataGridViewOracleFields.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleFields_RowLeave);
            // 
            // textBoxFieldComment
            // 
            this.textBoxFieldComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFieldComment.Location = new System.Drawing.Point(3, 3);
            this.textBoxFieldComment.Multiline = true;
            this.textBoxFieldComment.Name = "textBoxFieldComment";
            this.textBoxFieldComment.ReadOnly = true;
            this.textBoxFieldComment.Size = new System.Drawing.Size(323, 77);
            this.textBoxFieldComment.TabIndex = 2;
            // 
            // UCColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColumnsGroupBox);
            this.Name = "UCColumns";
            this.Size = new System.Drawing.Size(353, 328);
            this.ColumnsGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ColumnsGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewOracleFields;
        private System.Windows.Forms.TextBox textBoxFieldComment;
    }
}
