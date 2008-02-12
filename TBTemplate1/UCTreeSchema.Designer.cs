namespace TBTreeSchema
{
    partial class UCTreeSchema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTreeSchema));
            this.treeViewOracleSchema = new System.Windows.Forms.TreeView();
            this.contextMenuStripTreeSchema = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStripTreeSchema.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewOracleSchema
            // 
            this.treeViewOracleSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewOracleSchema.ContextMenuStrip = this.contextMenuStripTreeSchema;
            this.treeViewOracleSchema.FullRowSelect = true;
            this.treeViewOracleSchema.HideSelection = false;
            this.treeViewOracleSchema.ImageIndex = 0;
            this.treeViewOracleSchema.ImageList = this.imageList1;
            this.treeViewOracleSchema.Location = new System.Drawing.Point(3, 3);
            this.treeViewOracleSchema.Name = "treeViewOracleSchema";
            this.treeViewOracleSchema.SelectedImageIndex = 0;
            this.treeViewOracleSchema.Size = new System.Drawing.Size(230, 386);
            this.treeViewOracleSchema.TabIndex = 27;
            this.treeViewOracleSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOracleSchema_AfterSelect);
            this.treeViewOracleSchema.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewOracleSchema_MouseDown);
            this.treeViewOracleSchema.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewOracleSchema_KeyUp);
            // 
            // contextMenuStripTreeSchema
            // 
            this.contextMenuStripTreeSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStripTreeSchema.Name = "contextMenuStripTreeSchema";
            this.contextMenuStripTreeSchema.Size = new System.Drawing.Size(113, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "none");
            this.imageList1.Images.SetKeyName(1, "schema.png");
            this.imageList1.Images.SetKeyName(2, "table.png");
            this.imageList1.Images.SetKeyName(3, "function");
            this.imageList1.Images.SetKeyName(4, "procedure");
            this.imageList1.Images.SetKeyName(5, "package.png");
            this.imageList1.Images.SetKeyName(6, "trigger.png");
            this.imageList1.Images.SetKeyName(7, "sequence.png");
            this.imageList1.Images.SetKeyName(8, "field.png");
            this.imageList1.Images.SetKeyName(9, "DoubleRightArrowHS.png");
            this.imageList1.Images.SetKeyName(10, "refreshLeft.png");
            this.imageList1.Images.SetKeyName(11, "refreshRight.png");
            this.imageList1.Images.SetKeyName(12, "error.ico");
            // 
            // UCTreeSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewOracleSchema);
            this.Name = "UCTreeSchema";
            this.Size = new System.Drawing.Size(236, 392);
            this.contextMenuStripTreeSchema.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewOracleSchema;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeSchema;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;

    }
}