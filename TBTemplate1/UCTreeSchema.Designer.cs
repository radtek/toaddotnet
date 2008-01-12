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
            this.buttonGetOracleTables = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOracleDataSource = new System.Windows.Forms.TextBox();
            this.textBoxOraclePassword = new System.Windows.Forms.TextBox();
            this.textBoxOracleUserId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeViewOracleSchema
            // 
            this.treeViewOracleSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewOracleSchema.FullRowSelect = true;
            this.treeViewOracleSchema.HideSelection = false;
            this.treeViewOracleSchema.Location = new System.Drawing.Point(3, 113);
            this.treeViewOracleSchema.Name = "treeViewOracleSchema";
            this.treeViewOracleSchema.Size = new System.Drawing.Size(255, 307);
            this.treeViewOracleSchema.TabIndex = 27;
            this.treeViewOracleSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOracleSchema_AfterSelect);
            // 
            // buttonGetOracleTables
            // 
            this.buttonGetOracleTables.Location = new System.Drawing.Point(6, 84);
            this.buttonGetOracleTables.Name = "buttonGetOracleTables";
            this.buttonGetOracleTables.Size = new System.Drawing.Size(75, 23);
            this.buttonGetOracleTables.TabIndex = 26;
            this.buttonGetOracleTables.Text = "Connect";
            this.buttonGetOracleTables.UseVisualStyleBackColor = true;
            this.buttonGetOracleTables.Click += new System.EventHandler(this.buttonGetOracleTables_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "DataSource";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "UserId";
            // 
            // textBoxOracleDataSource
            // 
            this.textBoxOracleDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOracleDataSource.Location = new System.Drawing.Point(67, 58);
            this.textBoxOracleDataSource.Name = "textBoxOracleDataSource";
            this.textBoxOracleDataSource.Size = new System.Drawing.Size(191, 20);
            this.textBoxOracleDataSource.TabIndex = 22;
            this.textBoxOracleDataSource.Text = "FT.LOCALPDL-ASOCS";
            // 
            // textBoxOraclePassword
            // 
            this.textBoxOraclePassword.Location = new System.Drawing.Point(67, 31);
            this.textBoxOraclePassword.Name = "textBoxOraclePassword";
            this.textBoxOraclePassword.PasswordChar = '*';
            this.textBoxOraclePassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxOraclePassword.TabIndex = 21;
            this.textBoxOraclePassword.Text = "PROPERTY";
            // 
            // textBoxOracleUserId
            // 
            this.textBoxOracleUserId.Location = new System.Drawing.Point(67, 4);
            this.textBoxOracleUserId.Name = "textBoxOracleUserId";
            this.textBoxOracleUserId.Size = new System.Drawing.Size(100, 20);
            this.textBoxOracleUserId.TabIndex = 20;
            this.textBoxOracleUserId.Text = "ABSIS";
            // 
            // UCTreeSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewOracleSchema);
            this.Controls.Add(this.buttonGetOracleTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOracleDataSource);
            this.Controls.Add(this.textBoxOraclePassword);
            this.Controls.Add(this.textBoxOracleUserId);
            this.Name = "UCTreeSchema";
            this.Size = new System.Drawing.Size(261, 423);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewOracleSchema;
        private System.Windows.Forms.Button buttonGetOracleTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOracleDataSource;
        private System.Windows.Forms.TextBox textBoxOraclePassword;
        private System.Windows.Forms.TextBox textBoxOracleUserId;

    }
}