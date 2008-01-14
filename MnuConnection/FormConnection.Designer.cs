namespace MnuConnection
{
    partial class FormConnection
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
            this.ConnectionGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOraclePassword = new System.Windows.Forms.TextBox();
            this.textBoxOracleUserId = new System.Windows.Forms.TextBox();
            this.buttonGetOracleTables = new System.Windows.Forms.Button();
            this.dataGridViewConnection = new System.Windows.Forms.DataGridView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.TNSNamesComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectionGroupBox
            // 
            this.ConnectionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectionGroupBox.Controls.Add(this.TNSNamesComboBox);
            this.ConnectionGroupBox.Controls.Add(this.label3);
            this.ConnectionGroupBox.Controls.Add(this.label2);
            this.ConnectionGroupBox.Controls.Add(this.label1);
            this.ConnectionGroupBox.Controls.Add(this.textBoxOraclePassword);
            this.ConnectionGroupBox.Controls.Add(this.textBoxOracleUserId);
            this.ConnectionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ConnectionGroupBox.Name = "ConnectionGroupBox";
            this.ConnectionGroupBox.Size = new System.Drawing.Size(232, 104);
            this.ConnectionGroupBox.TabIndex = 29;
            this.ConnectionGroupBox.TabStop = false;
            this.ConnectionGroupBox.Text = "Connection";
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
            // buttonGetOracleTables
            // 
            this.buttonGetOracleTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetOracleTables.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonGetOracleTables.Location = new System.Drawing.Point(468, 363);
            this.buttonGetOracleTables.Name = "buttonGetOracleTables";
            this.buttonGetOracleTables.Size = new System.Drawing.Size(75, 23);
            this.buttonGetOracleTables.TabIndex = 33;
            this.buttonGetOracleTables.Text = "Connect";
            this.buttonGetOracleTables.UseVisualStyleBackColor = true;
            // 
            // dataGridViewConnection
            // 
            this.dataGridViewConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConnection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConnection.Location = new System.Drawing.Point(12, 122);
            this.dataGridViewConnection.Name = "dataGridViewConnection";
            this.dataGridViewConnection.Size = new System.Drawing.Size(531, 153);
            this.dataGridViewConnection.TabIndex = 30;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(387, 363);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 34;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // TNSNamesComboBox
            // 
            this.TNSNamesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TNSNamesComboBox.FormattingEnabled = true;
            this.TNSNamesComboBox.Location = new System.Drawing.Point(65, 73);
            this.TNSNamesComboBox.Name = "TNSNamesComboBox";
            this.TNSNamesComboBox.Size = new System.Drawing.Size(161, 21);
            this.TNSNamesComboBox.TabIndex = 33;
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 398);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonGetOracleTables);
            this.Controls.Add(this.dataGridViewConnection);
            this.Controls.Add(this.ConnectionGroupBox);
            this.Name = "FormConnection";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.ConnectionGroupBox.ResumeLayout(false);
            this.ConnectionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ConnectionGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGetOracleTables;
        private System.Windows.Forms.DataGridView dataGridViewConnection;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxOraclePassword;
        public System.Windows.Forms.TextBox textBoxOracleUserId;
        public System.Windows.Forms.ComboBox TNSNamesComboBox;
    }
}