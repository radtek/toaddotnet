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
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageResult = new System.Windows.Forms.TabPage();
            this.dataGridViewOracleQueryData = new System.Windows.Forms.DataGridView();
            this.tabPageMessage = new System.Windows.Forms.TabPage();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExecQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbortQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonToEnd = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarQuery = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.QueryGroupBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleQueryData)).BeginInit();
            this.tabPageMessage.SuspendLayout();
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
            this.QueryGroupBox.Location = new System.Drawing.Point(4, 28);
            this.QueryGroupBox.Name = "QueryGroupBox";
            this.QueryGroupBox.Size = new System.Drawing.Size(758, 491);
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
            this.splitContainer1.Panel1.Controls.Add(this.textEditorControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(752, 472);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.AllowDrop = true;
            this.textEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditorControl1.IsReadOnly = false;
            this.textEditorControl1.Location = new System.Drawing.Point(3, 3);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(742, 277);
            this.textEditorControl1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageResult);
            this.tabControl1.Controls.Add(this.tabPageMessage);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(741, 170);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageResult
            // 
            this.tabPageResult.Controls.Add(this.dataGridViewOracleQueryData);
            this.tabPageResult.Location = new System.Drawing.Point(4, 22);
            this.tabPageResult.Name = "tabPageResult";
            this.tabPageResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResult.Size = new System.Drawing.Size(733, 144);
            this.tabPageResult.TabIndex = 0;
            this.tabPageResult.Text = "Result";
            this.tabPageResult.UseVisualStyleBackColor = true;
            // 
            // dataGridViewOracleQueryData
            // 
            this.dataGridViewOracleQueryData.AllowUserToAddRows = false;
            this.dataGridViewOracleQueryData.AllowUserToDeleteRows = false;
            this.dataGridViewOracleQueryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOracleQueryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOracleQueryData.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewOracleQueryData.Name = "dataGridViewOracleQueryData";
            this.dataGridViewOracleQueryData.ReadOnly = true;
            this.dataGridViewOracleQueryData.Size = new System.Drawing.Size(727, 138);
            this.dataGridViewOracleQueryData.TabIndex = 1;
            this.dataGridViewOracleQueryData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleQueryData_RowEnter);
            // 
            // tabPageMessage
            // 
            this.tabPageMessage.Controls.Add(this.textBoxMessage);
            this.tabPageMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessage.Name = "tabPageMessage";
            this.tabPageMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessage.Size = new System.Drawing.Size(733, 144);
            this.tabPageMessage.TabIndex = 1;
            this.tabPageMessage.Text = "Message";
            this.tabPageMessage.UseVisualStyleBackColor = true;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Location = new System.Drawing.Point(7, 7);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessage.Size = new System.Drawing.Size(720, 131);
            this.textBoxMessage.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExecQuery,
            this.toolStripButtonAbortQuery,
            this.toolStripButtonNextPage,
            this.toolStripButtonToEnd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(765, 25);
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
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButtonNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextPage.Image = global::TBQuery.Properties.Resources.DataContainer_MoveNextHS;
            this.toolStripButtonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
            this.toolStripButtonNextPage.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNextPage.Text = ">>";
            this.toolStripButtonNextPage.ToolTipText = "Next page";
            this.toolStripButtonNextPage.Click += new System.EventHandler(this.toolStripButtonNextPage_Click);
            // 
            // toolStripButtonToEnd
            // 
            this.toolStripButtonToEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButtonToEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonToEnd.Image = global::TBQuery.Properties.Resources.DataContainer_MoveLastHS;
            this.toolStripButtonToEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToEnd.Name = "toolStripButtonToEnd";
            this.toolStripButtonToEnd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonToEnd.Text = ">>|";
            this.toolStripButtonToEnd.ToolTipText = "Last page";
            this.toolStripButtonToEnd.Click += new System.EventHandler(this.toolStripButtonToEnd_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMessage,
            this.toolStripStatusLabelElapsedTime,
            this.toolStripStatusLabelRecords,
            this.toolStripProgressBarQuery,
            this.toolStripStatusLabelPosition});
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(765, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMessage
            // 
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
            this.toolStripStatusLabelRecords.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.toolStripStatusLabelRecords.Name = "toolStripStatusLabelRecords";
            this.toolStripStatusLabelRecords.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelRecords.Text = "0 record";
            // 
            // toolStripProgressBarQuery
            // 
            this.toolStripProgressBarQuery.Name = "toolStripProgressBarQuery";
            this.toolStripProgressBarQuery.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBarQuery.Visible = false;
            // 
            // toolStripStatusLabelPosition
            // 
            this.toolStripStatusLabelPosition.AutoSize = false;
            this.toolStripStatusLabelPosition.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelPosition.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.toolStripStatusLabelPosition.Margin = new System.Windows.Forms.Padding(300, 3, 0, 2);
            this.toolStripStatusLabelPosition.Name = "toolStripStatusLabelPosition";
            this.toolStripStatusLabelPosition.Size = new System.Drawing.Size(150, 17);
            this.toolStripStatusLabelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // UCQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.QueryGroupBox);
            this.Name = "UCQuery";
            this.Size = new System.Drawing.Size(765, 544);
            this.Load += new System.EventHandler(this.UCQuery_Load);
            this.QueryGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleQueryData)).EndInit();
            this.tabPageMessage.ResumeLayout(false);
            this.tabPageMessage.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExecQuery;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbortQuery;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecords;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageResult;
        private System.Windows.Forms.DataGridView dataGridViewOracleQueryData;
        private System.Windows.Forms.TabPage tabPageMessage;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarQuery;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessage;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonToEnd;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPosition;
    }
}
