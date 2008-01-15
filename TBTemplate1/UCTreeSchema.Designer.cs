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
            this.treeViewOracleSchema = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewOracleSchema
            // 
            this.treeViewOracleSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewOracleSchema.FullRowSelect = true;
            this.treeViewOracleSchema.HideSelection = false;
            this.treeViewOracleSchema.Location = new System.Drawing.Point(3, 3);
            this.treeViewOracleSchema.Name = "treeViewOracleSchema";
            this.treeViewOracleSchema.Size = new System.Drawing.Size(230, 386);
            this.treeViewOracleSchema.TabIndex = 27;
            this.treeViewOracleSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOracleSchema_AfterSelect);
            // 
            // UCTreeSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewOracleSchema);
            this.Name = "UCTreeSchema";
            this.Size = new System.Drawing.Size(236, 392);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewOracleSchema;

    }
}