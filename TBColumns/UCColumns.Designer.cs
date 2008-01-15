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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCColumns));
            this.ColumnsGroupBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewOracleFields = new System.Windows.Forms.DataGridView();
            this.textBoxFieldComment = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarQuery = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddCol = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteCol = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonModifyCol = new System.Windows.Forms.ToolStripButton();
            this.ColumnsGroupBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ColumnsGroupBox
            // 
            this.ColumnsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ColumnsGroupBox.Controls.Add(this.splitContainer1);
            this.ColumnsGroupBox.Location = new System.Drawing.Point(4, 35);
            this.ColumnsGroupBox.Name = "ColumnsGroupBox";
            this.ColumnsGroupBox.Size = new System.Drawing.Size(610, 318);
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
            this.splitContainer1.Size = new System.Drawing.Size(597, 293);
            this.splitContainer1.SplitterDistance = 202;
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
            this.dataGridViewOracleFields.Size = new System.Drawing.Size(587, 192);
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
            this.textBoxFieldComment.Size = new System.Drawing.Size(587, 77);
            this.textBoxFieldComment.TabIndex = 2;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMessage,
            this.toolStripStatusLabelElapsedTime,
            this.toolStripStatusLabelRecords,
            this.toolStripProgressBarQuery});
            this.statusStrip1.Location = new System.Drawing.Point(0, 356);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMessage
            // 
            this.toolStripStatusLabelMessage.AutoToolTip = true;
            this.toolStripStatusLabelMessage.Name = "toolStripStatusLabelMessage";
            this.toolStripStatusLabelMessage.Size = new System.Drawing.Size(0, 17);
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
            // toolStripProgressBarQuery
            // 
            this.toolStripProgressBarQuery.Name = "toolStripProgressBarQuery";
            this.toolStripProgressBarQuery.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBarQuery.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddCol,
            this.toolStripButtonDeleteCol,
            this.toolStripButtonModifyCol});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(617, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAddCol
            // 
            this.toolStripButtonAddCol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddCol.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddCol.Image")));
            this.toolStripButtonAddCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddCol.Name = "toolStripButtonAddCol";
            this.toolStripButtonAddCol.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddCol.Text = "Add column";
            this.toolStripButtonAddCol.Click += new System.EventHandler(this.toolStripButtonAddCol_Click);
            // 
            // toolStripButtonDeleteCol
            // 
            this.toolStripButtonDeleteCol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteCol.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteCol.Image")));
            this.toolStripButtonDeleteCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteCol.Name = "toolStripButtonDeleteCol";
            this.toolStripButtonDeleteCol.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDeleteCol.Text = "Delete column";
            this.toolStripButtonDeleteCol.Click += new System.EventHandler(this.toolStripButtonDeleteCol_Click);
            // 
            // toolStripButtonModifyCol
            // 
            this.toolStripButtonModifyCol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonModifyCol.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonModifyCol.Image")));
            this.toolStripButtonModifyCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonModifyCol.Name = "toolStripButtonModifyCol";
            this.toolStripButtonModifyCol.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonModifyCol.Text = "Modify column";
            this.toolStripButtonModifyCol.Click += new System.EventHandler(this.toolStripButtonModifyCol_Click);
            // 
            // UCColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ColumnsGroupBox);
            this.Name = "UCColumns";
            this.Size = new System.Drawing.Size(617, 378);
            this.ColumnsGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ColumnsGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewOracleFields;
        private System.Windows.Forms.TextBox textBoxFieldComment;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecords;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarQuery;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddCol;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteCol;
        private System.Windows.Forms.ToolStripButton toolStripButtonModifyCol;
    }
}
