using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Membs;
using PlugIn;
using PluginTypes;

namespace ToadDotNet
{
    public partial class MainForm : Form
    {
        // Needed to send event to the libraries
        public PlugEvent plugEvent = new PlugEvent();

        public Connexion.Connexion connexion = null;

        /// <summary>
        /// Define the language to be use 
        /// </summary>
        public CultureInfo CurrentCulture = null;

        public MainForm()
        {
            // Définition de la culture par défaut
            //System.Threading.Thread.CurrentThread.CurrentUICulture = FrenchCulture;
            CurrentCulture = new CultureInfo(Config.GetInnerTextValue(Config.Load(), "/membs/AppConfig/lang"));
            System.Threading.Thread.CurrentThread.CurrentUICulture = CurrentCulture;
            InitializeComponent();
            
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MembsForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.Nom()))
            {
                FormRegister formRegister = new FormRegister();
                if (formRegister.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            } 
            else
            {
                AssemblyLoader asmLoader = new AssemblyLoader(this, this.menuStrip1, this.rightTabControl, this.leftTabControl, plugEvent);
                string PluginsPath = Config.GetElement(Config.Load(), "/membs/AppConfig/plugin").GetAttribute("path"); //@".\plugins";//
                if (Directory.Exists(PluginsPath))
                {
                    DirectoryInfo di = new DirectoryInfo(PluginsPath);
                    string[] files = Directory.GetFiles(di.FullName, "*.dll");
                    foreach (string file in files)
                    {
                        if (asmLoader.Load(file))
                        {

                        }
                        else if (asmLoader.GetErrorMessage() != null)
                        {
                            MessageBox.Show(asmLoader.GetErrorMessage(), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }    
            }
            
        }

        private void enregistrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }
    }
}