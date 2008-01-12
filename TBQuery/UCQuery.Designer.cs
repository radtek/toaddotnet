namespace TBQuery
{
    partial class UCQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCQuery));
            this.QueryGroupBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxOracleQuerySQL = new SyntaxHighlighter.SyntaxRichTextBox();
            this.dataGridViewOracleQueryData = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExecQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbortQuery = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.QueryGroupBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleQueryData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // QueryGroupBox
            // 
            this.QueryGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryGroupBox.Controls.Add(this.splitContainer1);
            this.QueryGroupBox.Location = new System.Drawing.Point(4, 38);
            this.QueryGroupBox.Name = "QueryGroupBox";
            this.QueryGroupBox.Size = new System.Drawing.Size(662, 500);
            this.QueryGroupBox.TabIndex = 0;
            this.QueryGroupBox.TabStop = false;
            this.QueryGroupBox.Text = "Query";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxOracleQuerySQL);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewOracleQueryData);
            this.splitContainer1.Size = new System.Drawing.Size(656, 481);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 0;
            // 
            // richTextBoxOracleQuerySQL
            // 
            this.richTextBoxOracleQuerySQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOracleQuerySQL.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxOracleQuerySQL.Name = "richTextBoxOracleQuerySQL";
            this.richTextBoxOracleQuerySQL.Size = new System.Drawing.Size(652, 289);
            this.richTextBoxOracleQuerySQL.TabIndex = 1;
            this.richTextBoxOracleQuerySQL.Text = "";
            this.richTextBoxOracleQuerySQL.WordWrap = false;
            this.richTextBoxOracleQuerySQL.TextChanged += new System.EventHandler(this.richTextBoxOracleQuerySQL_TextChanged);
            // 
            // dataGridViewOracleQueryData
            // 
            this.dataGridViewOracleQueryData.AllowUserToAddRows = false;
            this.dataGridViewOracleQueryData.AllowUserToDeleteRows = false;
            this.dataGridViewOracleQueryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOracleQueryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOracleQueryData.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewOracleQueryData.Name = "dataGridViewOracleQueryData";
            this.dataGridViewOracleQueryData.ReadOnly = true;
            this.dataGridViewOracleQueryData.Size = new System.Drawing.Size(652, 180);
            this.dataGridViewOracleQueryData.TabIndex = 0;
            this.dataGridViewOracleQueryData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleQueryData_RowEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExecQuery,
            this.toolStripButtonAbortQuery});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(669, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExecQuery
            // 
            this.toolStripButtonExecQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExecQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExecQuery.Image")));
            this.toolStripButtonExecQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExecQuery.Name = "toolStripButtonExecQuery";
            this.toolStripButtonExecQuery.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExecQuery.Text = "Execute";
            this.toolStripButtonExecQuery.ToolTipText = "Execute";
            this.toolStripButtonExecQuery.Click += new System.EventHandler(this.toolStripButtonExecQuery_Click);
            // 
            // toolStripButtonAbortQuery
            // 
            this.toolStripButtonAbortQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbortQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbortQuery.Image")));
            this.toolStripButtonAbortQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbortQuery.Name = "toolStripButtonAbortQuery";
            this.toolStripButtonAbortQuery.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAbortQuery.Text = "Abort";
            this.toolStripButtonAbortQuery.ToolTipText = "Abort query";
            this.toolStripButtonAbortQuery.Click += new System.EventHandler(this.toolStripButtonAbortQuery_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelElapsedTime,
            this.toolStripStatusLabelRecords,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 541);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(669, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelElapsedTime
            // 
            this.toolStripStatusLabelElapsedTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelElapsedTime.Name = "toolStripStatusLabelElapsedTime";
            this.toolStripStatusLabelElapsedTime.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripStatusLabelRecords
            // 
            this.toolStripStatusLabelRecords.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelRecords.Name = "toolStripStatusLabelRecords";
            this.toolStripStatusLabelRecords.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelRecords.Text = "0 record";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // UCQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.QueryGroupBox);
            this.Name = "UCQuery";
            this.Size = new System.Drawing.Size(669, 563);
            this.Load += new System.EventHandler(this.UCQuery_Load);
            this.QueryGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleQueryData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox QueryGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewOracleQueryData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExecQuery;
        private SyntaxHighlighter.SyntaxRichTextBox richTextBoxOracleQuerySQL;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbortQuery;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecords;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}
