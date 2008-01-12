namespace ToadDotNet
{
    partial class FormRegister
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
            this.textBoxNom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelGUID = new System.Windows.Forms.Label();
            this.buttonRegisterApp = new System.Windows.Forms.Button();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelProductVersion = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxNom
            // 
            this.textBoxNom.Location = new System.Drawing.Point(12, 25);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.Size = new System.Drawing.Size(407, 20);
            this.textBoxNom.TabIndex = 0;
            this.textBoxNom.TextChanged += new System.EventHandler(this.TextBoxNomTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Votre nom:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Votre adresse email:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(12, 64);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(407, 20);
            this.textBoxEmail.TabIndex = 2;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.TextBoxNomTextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 43);
            this.label3.TabIndex = 4;
            this.label3.Text = "Attention, votre adresse email doit être valide, car le ou les numéros de licence" +
                " que vous demandez seront envoyés à cette adrese. Cette adresse ne sera en aucun" +
                " cas cédée à des tiers.";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Enabled = false;
            this.buttonSubmit.Location = new System.Drawing.Point(12, 463);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 5;
            this.buttonSubmit.Text = "Envoyer";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(344, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.Key,
            this.Expiration});
            this.dataGridView1.Location = new System.Drawing.Point(12, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(407, 217);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Product
            // 
            this.Product.HeaderText = "Plugin";
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            // 
            // Expiration
            // 
            this.Expiration.HeaderText = "Expiration";
            this.Expiration.Name = "Expiration";
            this.Expiration.ReadOnly = true;
            // 
            // labelGUID
            // 
            this.labelGUID.Location = new System.Drawing.Point(12, 91);
            this.labelGUID.Name = "labelGUID";
            this.labelGUID.Size = new System.Drawing.Size(407, 38);
            this.labelGUID.TabIndex = 9;
            this.labelGUID.Text = "Unregister";
            // 
            // buttonRegisterApp
            // 
            this.buttonRegisterApp.Enabled = false;
            this.buttonRegisterApp.Location = new System.Drawing.Point(344, 132);
            this.buttonRegisterApp.Name = "buttonRegisterApp";
            this.buttonRegisterApp.Size = new System.Drawing.Size(75, 23);
            this.buttonRegisterApp.TabIndex = 10;
            this.buttonRegisterApp.Text = "Register";
            this.buttonRegisterApp.UseVisualStyleBackColor = true;
            this.buttonRegisterApp.Click += new System.EventHandler(this.buttonRegisterApp_Click);
            // 
            // labelProductName
            // 
            this.labelProductName.Location = new System.Drawing.Point(13, 137);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(325, 26);
            this.labelProductName.TabIndex = 11;
            // 
            // labelProductVersion
            // 
            this.labelProductVersion.Location = new System.Drawing.Point(13, 171);
            this.labelProductVersion.Name = "labelProductVersion";
            this.labelProductVersion.Size = new System.Drawing.Size(325, 23);
            this.labelProductVersion.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(263, 463);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Annuler";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FormRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 499);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelProductVersion);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.buttonRegisterApp);
            this.Controls.Add(this.labelGUID);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormRegister";
            this.Text = "Licences";
            this.Load += new System.EventHandler(this.FormRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label labelProductVersion;
        private System.Windows.Forms.Label labelProductName;

        #endregion

        private System.Windows.Forms.TextBox textBoxNom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiration;
        private System.Windows.Forms.Label labelGUID;
        private System.Windows.Forms.Button buttonRegisterApp;
        private System.Windows.Forms.Button button2;
    }
}