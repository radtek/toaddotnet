namespace MnuConnection
{
    partial class UCPersonne
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
            this.SuspendLayout();
            // 
            // PersonneGroupBox
            // 
            this.PersonneGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonneGroupBox.Location = new System.Drawing.Point(4, 4);
            this.PersonneGroupBox.Name = "PersonneGroupBox";
            this.PersonneGroupBox.Size = new System.Drawing.Size(402, 281);
            this.PersonneGroupBox.TabIndex = 0;
            this.PersonneGroupBox.TabStop = false;
            this.PersonneGroupBox.Text = "Personne";
            // 
            // UCPersonne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PersonneGroupBox);
            this.Name = "UCPersonne";
            this.Size = new System.Drawing.Size(420, 288);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PersonneGroupBox;
    }
}
