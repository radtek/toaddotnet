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
            this.TableDataGroupBox = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.dataGridViewOracleFields = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TableDataGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOracleFields)).BeginInit();
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
            this.toolStripStatusLabelElapsedTime,
            this.toolStripStatusLabelRecords,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(707, 22);
            this.statusStrip1.TabIndex = 1;
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
            // dataGridViewOracleFields
            // 
            this.dataGridViewOracleFields.AllowUserToAddRows = false;
            this.dataGridViewOracleFields.AllowUserToDeleteRows = false;
            this.dataGridViewOracleFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOracleFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOracleFields.Location = new System.Drawing.Point(7, 12);
            this.dataGridViewOracleFields.Name = "dataGridViewOracleFields";
            this.dataGridViewOracleFields.ReadOnly = true;
            this.dataGridViewOracleFields.Size = new System.Drawing.Size(700, 306);
            this.dataGridViewOracleFields.TabIndex = 0;
            this.dataGridViewOracleFields.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOracleFields_RowEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(720, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox TableDataGroupBox;
        private System.Windows.Forms.DataGridView dataGridViewOracleFields;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRecords;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
