namespace TBSource
{
    partial class UCSource
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.sqlEditor1 = new ULib.SQLEditor();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // sqlEditor1
            // 
            this.sqlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlEditor1.Location = new System.Drawing.Point(0, 0);
            this.sqlEditor1.Name = "sqlEditor1";
            this.sqlEditor1.Size = new System.Drawing.Size(774, 539);
            this.sqlEditor1.TabIndex = 5;
            // 
            // UCSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sqlEditor1);
            this.Name = "UCSource";
            this.Size = new System.Drawing.Size(774, 539);
            this.Load += new System.EventHandler(this.UCSource_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ULib.SQLEditor sqlEditor1;

    }
}
