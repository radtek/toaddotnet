namespace DBDiff
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.SourceRefreshButton = new System.Windows.Forms.Button();
            this.SourceTablesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.buttonSaveComment = new System.Windows.Forms.Button();
            this.buttonSelectSource = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelRecordSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarQuerySource = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelElapsedTimeSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateAllCommentButton = new System.Windows.Forms.Button();
            this.labelSourceTnsnames = new System.Windows.Forms.Label();
            this.UpdateCommentButton = new System.Windows.Forms.Button();
            this.SourceDataGridView = new System.Windows.Forms.DataGridView();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.buttonSelectTarget = new System.Windows.Forms.Button();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelRecordsTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarQueryTarget = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelElapsedTimeTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTargetTnsnames = new System.Windows.Forms.Label();
            this.TargetCommentTextBox = new System.Windows.Forms.TextBox();
            this.TargetDataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceDataGridView)).BeginInit();
            this.statusStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonSelectTarget);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip3);
            this.splitContainer1.Panel2.Controls.Add(this.labelTargetTnsnames);
            this.splitContainer1.Panel2.Controls.Add(this.TargetCommentTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.TargetDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(899, 550);
            this.splitContainer1.SplitterDistance = 538;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.AllCheckBox);
            this.splitContainer2.Panel1.Controls.Add(this.SourceRefreshButton);
            this.splitContainer2.Panel1.Controls.Add(this.SourceTablesCheckedListBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.buttonSaveComment);
            this.splitContainer2.Panel2.Controls.Add(this.buttonSelectSource);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer2.Panel2.Controls.Add(this.UpdateAllCommentButton);
            this.splitContainer2.Panel2.Controls.Add(this.labelSourceTnsnames);
            this.splitContainer2.Panel2.Controls.Add(this.UpdateCommentButton);
            this.splitContainer2.Panel2.Controls.Add(this.SourceDataGridView);
            this.splitContainer2.Panel2.Controls.Add(this.CommentTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(528, 543);
            this.splitContainer2.SplitterDistance = 206;
            this.splitContainer2.TabIndex = 9;
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Location = new System.Drawing.Point(13, 15);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(119, 17);
            this.AllCheckBox.TabIndex = 7;
            this.AllCheckBox.Text = "Check/Uncheck all";
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // SourceRefreshButton
            // 
            this.SourceRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SourceRefreshButton.Location = new System.Drawing.Point(13, 513);
            this.SourceRefreshButton.Name = "SourceRefreshButton";
            this.SourceRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.SourceRefreshButton.TabIndex = 3;
            this.SourceRefreshButton.Text = "Refresh";
            this.SourceRefreshButton.UseVisualStyleBackColor = true;
            this.SourceRefreshButton.Click += new System.EventHandler(this.SourceRefreshButton_Click);
            // 
            // SourceTablesCheckedListBox
            // 
            this.SourceTablesCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceTablesCheckedListBox.FormattingEnabled = true;
            this.SourceTablesCheckedListBox.Location = new System.Drawing.Point(10, 40);
            this.SourceTablesCheckedListBox.Name = "SourceTablesCheckedListBox";
            this.SourceTablesCheckedListBox.Size = new System.Drawing.Size(189, 469);
            this.SourceTablesCheckedListBox.TabIndex = 2;
            this.SourceTablesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.SourceTablesCheckedListBox_SelectedIndexChanged);
            // 
            // buttonSaveComment
            // 
            this.buttonSaveComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveComment.Image = global::DBDiff.Properties.Resources.Save;
            this.buttonSaveComment.Location = new System.Drawing.Point(283, 474);
            this.buttonSaveComment.Name = "buttonSaveComment";
            this.buttonSaveComment.Size = new System.Drawing.Size(28, 23);
            this.buttonSaveComment.TabIndex = 11;
            this.buttonSaveComment.UseVisualStyleBackColor = true;
            this.buttonSaveComment.Click += new System.EventHandler(this.buttonSaveComment_Click);
            // 
            // buttonSelectSource
            // 
            this.buttonSelectSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectSource.Location = new System.Drawing.Point(283, 5);
            this.buttonSelectSource.Name = "buttonSelectSource";
            this.buttonSelectSource.Size = new System.Drawing.Size(28, 23);
            this.buttonSelectSource.TabIndex = 10;
            this.buttonSelectSource.Text = "...";
            this.buttonSelectSource.UseVisualStyleBackColor = true;
            this.buttonSelectSource.Click += new System.EventHandler(this.buttonSelectSource_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelRecordSource,
            this.toolStripProgressBarQuerySource,
            this.toolStripStatusLabelElapsedTimeSource});
            this.statusStrip2.Location = new System.Drawing.Point(0, 517);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(314, 22);
            this.statusStrip2.TabIndex = 9;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabelRecordSource
            // 
            this.toolStripStatusLabelRecordSource.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelRecordSource.Name = "toolStripStatusLabelRecordSource";
            this.toolStripStatusLabelRecordSource.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelRecordSource.Text = "0 record";
            // 
            // toolStripProgressBarQuerySource
            // 
            this.toolStripProgressBarQuerySource.Name = "toolStripProgressBarQuerySource";
            this.toolStripProgressBarQuerySource.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBarQuerySource.Visible = false;
            // 
            // toolStripStatusLabelElapsedTimeSource
            // 
            this.toolStripStatusLabelElapsedTimeSource.Name = "toolStripStatusLabelElapsedTimeSource";
            this.toolStripStatusLabelElapsedTimeSource.Size = new System.Drawing.Size(0, 17);
            // 
            // UpdateAllCommentButton
            // 
            this.UpdateAllCommentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateAllCommentButton.Location = new System.Drawing.Point(283, 445);
            this.UpdateAllCommentButton.Name = "UpdateAllCommentButton";
            this.UpdateAllCommentButton.Size = new System.Drawing.Size(28, 23);
            this.UpdateAllCommentButton.TabIndex = 6;
            this.UpdateAllCommentButton.Text = ">>";
            this.UpdateAllCommentButton.UseVisualStyleBackColor = true;
            this.UpdateAllCommentButton.Click += new System.EventHandler(this.UpdateAllCommentButton_Click);
            // 
            // labelSourceTnsnames
            // 
            this.labelSourceTnsnames.AutoSize = true;
            this.labelSourceTnsnames.Location = new System.Drawing.Point(7, 10);
            this.labelSourceTnsnames.Name = "labelSourceTnsnames";
            this.labelSourceTnsnames.Size = new System.Drawing.Size(41, 13);
            this.labelSourceTnsnames.TabIndex = 8;
            this.labelSourceTnsnames.Text = "Source";
            // 
            // UpdateCommentButton
            // 
            this.UpdateCommentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateCommentButton.Location = new System.Drawing.Point(283, 416);
            this.UpdateCommentButton.Name = "UpdateCommentButton";
            this.UpdateCommentButton.Size = new System.Drawing.Size(28, 23);
            this.UpdateCommentButton.TabIndex = 5;
            this.UpdateCommentButton.Text = ">";
            this.UpdateCommentButton.UseMnemonic = false;
            this.UpdateCommentButton.UseVisualStyleBackColor = true;
            this.UpdateCommentButton.Click += new System.EventHandler(this.UpdateCommentButton_Click);
            // 
            // SourceDataGridView
            // 
            this.SourceDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceDataGridView.Location = new System.Drawing.Point(10, 34);
            this.SourceDataGridView.Name = "SourceDataGridView";
            this.SourceDataGridView.Size = new System.Drawing.Size(301, 376);
            this.SourceDataGridView.TabIndex = 1;
            this.SourceDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SourceDataGridView_RowEnter);
            this.SourceDataGridView.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.SourceDataGridView_RowLeave);
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentTextBox.Location = new System.Drawing.Point(10, 416);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(269, 94);
            this.CommentTextBox.TabIndex = 4;
            // 
            // buttonSelectTarget
            // 
            this.buttonSelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectTarget.Location = new System.Drawing.Point(322, 10);
            this.buttonSelectTarget.Name = "buttonSelectTarget";
            this.buttonSelectTarget.Size = new System.Drawing.Size(28, 23);
            this.buttonSelectTarget.TabIndex = 11;
            this.buttonSelectTarget.Text = "...";
            this.buttonSelectTarget.UseVisualStyleBackColor = true;
            this.buttonSelectTarget.Click += new System.EventHandler(this.buttonSelectTarget_Click);
            // 
            // statusStrip3
            // 
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelRecordsTarget,
            this.toolStripProgressBarQueryTarget,
            this.toolStripStatusLabelElapsedTimeTarget});
            this.statusStrip3.Location = new System.Drawing.Point(0, 524);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(353, 22);
            this.statusStrip3.TabIndex = 10;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabelRecordsTarget
            // 
            this.toolStripStatusLabelRecordsTarget.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelRecordsTarget.Name = "toolStripStatusLabelRecordsTarget";
            this.toolStripStatusLabelRecordsTarget.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabelRecordsTarget.Text = "0 record";
            // 
            // toolStripProgressBarQueryTarget
            // 
            this.toolStripProgressBarQueryTarget.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBarQueryTarget.Name = "toolStripProgressBarQueryTarget";
            this.toolStripProgressBarQueryTarget.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBarQueryTarget.Visible = false;
            // 
            // toolStripStatusLabelElapsedTimeTarget
            // 
            this.toolStripStatusLabelElapsedTimeTarget.Name = "toolStripStatusLabelElapsedTimeTarget";
            this.toolStripStatusLabelElapsedTimeTarget.Size = new System.Drawing.Size(0, 17);
            // 
            // labelTargetTnsnames
            // 
            this.labelTargetTnsnames.AutoSize = true;
            this.labelTargetTnsnames.Location = new System.Drawing.Point(10, 15);
            this.labelTargetTnsnames.Name = "labelTargetTnsnames";
            this.labelTargetTnsnames.Size = new System.Drawing.Size(38, 13);
            this.labelTargetTnsnames.TabIndex = 9;
            this.labelTargetTnsnames.Text = "Target";
            // 
            // TargetCommentTextBox
            // 
            this.TargetCommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetCommentTextBox.Location = new System.Drawing.Point(13, 421);
            this.TargetCommentTextBox.Multiline = true;
            this.TargetCommentTextBox.Name = "TargetCommentTextBox";
            this.TargetCommentTextBox.Size = new System.Drawing.Size(337, 94);
            this.TargetCommentTextBox.TabIndex = 5;
            // 
            // TargetDataGridView
            // 
            this.TargetDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetDataGridView.Location = new System.Drawing.Point(13, 39);
            this.TargetDataGridView.Name = "TargetDataGridView";
            this.TargetDataGridView.Size = new System.Drawing.Size(337, 376);
            this.TargetDataGridView.TabIndex = 1;
            this.TargetDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.TargetDataGridView_RowEnter);
            this.TargetDataGridView.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.TargetDataGridView_RowLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 550);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "DB Diff";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceDataGridView)).EndInit();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox SourceTablesCheckedListBox;
        private System.Windows.Forms.DataGridView SourceDataGridView;
        private System.Windows.Forms.DataGridView TargetDataGridView;
        private System.Windows.Forms.Button SourceRefreshButton;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.TextBox TargetCommentTextBox;
        private System.Windows.Forms.Button UpdateCommentButton;
        private System.Windows.Forms.Button UpdateAllCommentButton;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.Label labelSourceTnsnames;
        private System.Windows.Forms.Label labelTargetTnsnames;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecordSource;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarQuerySource;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecordsTarget;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarQueryTarget;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTimeSource;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTimeTarget;
        private System.Windows.Forms.Button buttonSelectSource;
        private System.Windows.Forms.Button buttonSelectTarget;
        private System.Windows.Forms.Button buttonSaveComment;
    }
}

