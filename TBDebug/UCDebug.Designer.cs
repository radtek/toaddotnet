namespace TBDebug
{
    partial class UCDebug
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
            this.PersonneGroupBox = new System.Windows.Forms.GroupBox();
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.treeViewDebug = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PersonneGroupBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PersonneGroupBox
            // 
            this.PersonneGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonneGroupBox.Controls.Add(this.splitContainer1);
            this.PersonneGroupBox.Location = new System.Drawing.Point(4, 4);
            this.PersonneGroupBox.Name = "PersonneGroupBox";
            this.PersonneGroupBox.Size = new System.Drawing.Size(663, 484);
            this.PersonneGroupBox.TabIndex = 0;
            this.PersonneGroupBox.TabStop = false;
            this.PersonneGroupBox.Text = "Debug";
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDebug.Location = new System.Drawing.Point(3, 3);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.Size = new System.Drawing.Size(321, 459);
            this.textBoxDebug.TabIndex = 0;
            // 
            // treeViewDebug
            // 
            this.treeViewDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDebug.Location = new System.Drawing.Point(3, 3);
            this.treeViewDebug.Name = "treeViewDebug";
            this.treeViewDebug.Size = new System.Drawing.Size(320, 459);
            this.treeViewDebug.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewDebug);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxDebug);
            this.splitContainer1.Size = new System.Drawing.Size(657, 465);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 2;
            // 
            // UCDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PersonneGroupBox);
            this.Name = "UCDebug";
            this.Size = new System.Drawing.Size(681, 491);
            this.PersonneGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PersonneGroupBox;
        private System.Windows.Forms.TextBox textBoxDebug;
        private System.Windows.Forms.TreeView treeViewDebug;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
