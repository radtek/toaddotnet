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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetOracleTables = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOracleDataSource = new System.Windows.Forms.TextBox();
            this.textBoxOraclePassword = new System.Windows.Forms.TextBox();
            this.textBoxOracleUserId = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewOracleSchema
            // 
            this.treeViewOracleSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewOracleSchema.FullRowSelect = true;
            this.treeViewOracleSchema.HideSelection = false;
            this.treeViewOracleSchema.Location = new System.Drawing.Point(3, 139);
            this.treeViewOracleSchema.Name = "treeViewOracleSchema";
            this.treeViewOracleSchema.Size = new System.Drawing.Size(230, 250);
            this.treeViewOracleSchema.TabIndex = 27;
            this.treeViewOracleSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOracleSchema_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonGetOracleTables);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxOracleDataSource);
            this.groupBox1.Controls.Add(this.textBoxOraclePassword);
            this.groupBox1.Controls.Add(this.textBoxOracleUserId);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 130);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonGetOracleTables
            // 
            this.buttonGetOracleTables.Location = new System.Drawing.Point(4, 99);
            this.buttonGetOracleTables.Name = "buttonGetOracleTables";
            this.buttonGetOracleTables.Size = new System.Drawing.Size(75, 23);
            this.buttonGetOracleTables.TabIndex = 33;
            this.buttonGetOracleTables.Text = "Connect";
            this.buttonGetOracleTables.UseVisualStyleBackColor = true;
            this.buttonGetOracleTables.Click += new System.EventHandler(this.buttonGetOracleTables_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-2, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "DataSource";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "UserId";
            // 
            // textBoxOracleDataSource
            // 
            this.textBoxOracleDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOracleDataSource.Location = new System.Drawing.Point(65, 73);
            this.textBoxOracleDataSource.Name = "textBoxOracleDataSource";
            this.textBoxOracleDataSource.Size = new System.Drawing.Size(158, 20);
            this.textBoxOracleDataSource.TabIndex = 29;
            // 
            // textBoxOraclePassword
            // 
            this.textBoxOraclePassword.Location = new System.Drawing.Point(65, 46);
            this.textBoxOraclePassword.Name = "textBoxOraclePassword";
            this.textBoxOraclePassword.PasswordChar = '*';
            this.textBoxOraclePassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxOraclePassword.TabIndex = 28;
            // 
            // textBoxOracleUserId
            // 
            this.textBoxOracleUserId.Location = new System.Drawing.Point(65, 19);
            this.textBoxOracleUserId.Name = "textBoxOracleUserId";
            this.textBoxOracleUserId.Size = new System.Drawing.Size(100, 20);
            this.textBoxOracleUserId.TabIndex = 27;
            // 
            // UCTreeSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeViewOracleSchema);
            this.Name = "UCTreeSchema";
            this.Size = new System.Drawing.Size(236, 392);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewOracleSchema;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGetOracleTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOracleDataSource;
        private System.Windows.Forms.TextBox textBoxOraclePassword;
        private System.Windows.Forms.TextBox textBoxOracleUserId;

    }
}