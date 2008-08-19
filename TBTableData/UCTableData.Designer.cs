namespace TBTableData
{
    partial class UCTableData
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
            this.components = new System.ComponentModel.Container();
            this.TableDataGroupBox = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarQuery = new System.Windows.Forms.ToolStripProgressBar();
            this.dataGridViewOracleFields = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonToEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCommit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRollback = new System.Windows.Forms.ToolStripButton();
            this.TableDataGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableDataGroupBox
            // 
            this.TableDataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TableDataGroupBox.Controls.Add(this.statusStrip1);
            this.TableDataGroupBox.Controls.Add(this.dataGridViewOracleFields);
            this.TableDataGroupBox.Location = new System.Drawing.Point(4, 35);
            this.TableDataGroupBox.Name = "TableDataGroupBox";
            this.TableDataGroupBox.Size = new System.Drawing.Size(713, 346);
            this.TableDataGroupBox.TabIndex = 0;
            this.TableDataGroupBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMessage,
            this.toolStripStatusLabelElapsedTime,
            this.toolStripStatusLabelRecords,
            this.toolStripProgressBarQuery});
            this.statusStrip1.Location = new System.Drawing.Point(3, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(707, 22);
            this.statusStrip1.TabIndex = 1;
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
            // dataGridViewOracleFields
            // 
            this.dataGridViewOracleFields.AllowUserToAddRows = false;
            this.dataGridViewOracleFields.AllowUserToDeleteRows = false;
            this.dataGridViewOracleFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOracleFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOracleFields.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewOracleFields.Location = new System.Drawing.Point(7, 12);
            this.dataGridViewOracleFields.Name = "dataGridViewOracleFields";
            this.dataGridViewOracleFields.ReadOnly = true;
            this.dataGridViewOracleFields.Size = new System.Drawing.Size(700, 306);
            this.dataGridViewOracleFields.TabIndex = 0;
            this.dataGridViewOracleFields.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleFields_RowEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCancel,
            this.toolStripButtonNextPage,
            this.toolStripButtonToEnd,
            this.toolStripButtonCommit,
            this.toolStripButtonRollback});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(720, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedRowsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 26);
            // 
            // deleteSelectedRowsToolStripMenuItem
            // 
            this.deleteSelectedRowsToolStripMenuItem.Name = "deleteSelectedRowsToolStripMenuItem";
            this.deleteSelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteSelectedRowsToolStripMenuItem.Text = "Delete selected rows";
            this.deleteSelectedRowsToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedRowsToolStripMenuItem_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCancel.Image = global::TBTableData.Properties.Resources.cancel;
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCancel.Text = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButtonNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextPage.Image = global::TBTableData.Properties.Resources.DataContainer_MoveNextHS;
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
            this.toolStripButtonToEnd.Image = global::TBTableData.Properties.Resources.DataContainer_MoveLastHS;
            this.toolStripButtonToEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToEnd.Name = "toolStripButtonToEnd";
            this.toolStripButtonToEnd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonToEnd.Text = ">>|";
            this.toolStripButtonToEnd.ToolTipText = "Last page";
            this.toolStripButtonToEnd.Click += new System.EventHandler(this.toolStripButtonToEnd_Click);
            // 
            // toolStripButtonCommit
            // 
            this.toolStripButtonCommit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCommit.Enabled = false;
            this.toolStripButtonCommit.Image = global::TBTableData.Properties.Resources.commit;
            this.toolStripButtonCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCommit.Name = "toolStripButtonCommit";
            this.toolStripButtonCommit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCommit.Text = "Commit";
            this.toolStripButtonCommit.Click += new System.EventHandler(this.toolStripButtonCommit_Click);
            // 
            // toolStripButtonRollback
            // 
            this.toolStripButtonRollback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRollback.Enabled = false;
            this.toolStripButtonRollback.Image = global::TBTableData.Properties.Resources.rollback;
            this.toolStripButtonRollback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRollback.Name = "toolStripButtonRollback";
            this.toolStripButtonRollback.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRollback.Text = "Rollback";
            this.toolStripButtonRollback.Click += new System.EventHandler(this.toolStripButtonRollback_Click);
            // 
            // UCTableData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.TableDataGroupBox);
            this.Name = "UCTableData";
            this.Size = new System.Drawing.Size(720, 384);
            this.TableDataGroupBox.ResumeLayout(false);
            this.TableDataGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox TableDataGroupBox;
        private System.Windows.Forms.DataGridView dataGridViewOracleFields;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecords;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarQuery;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessage;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonToEnd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonCommit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRollback;
    }
}
