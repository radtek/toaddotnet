/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte — Tous droits réservés.

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
  ----------------------------------------------------------------------------- 
 
  Ce programme est un logiciel libre ; vous pouvez le redistribuer ou le
  modifier suivant les termes de la “GNU General Public License” telle que
  publiée par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ultérieure.
  
  Ce programme est distribué dans l’espoir qu’il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans même la garantie implicite de COMMERCIALISABILITÉ
  ni d’ADÉQUATION À UN OBJECTIF PARTICULIER. Consultez la Licence Générale
  Publique GNU pour plus de détails.
  
  Vous devriez avoir reçu une copie de la Licence Générale Publique GNU avec
  ce programme ; si ce n’est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
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
            CurrentCulture = new CultureInfo(Config.GetInnerTextValue(Config.Load(), "/alf-solution/AppConfig/lang"));
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
            
            AssemblyLoader asmLoader = new AssemblyLoader(this, this.menuStrip1, this.rightTabControl, this.leftTabControl, plugEvent);
            string PluginsPath = Config.GetElement(Config.Load(), "/alf-solution/AppConfig/plugin").GetAttribute("path"); //@".\plugins";//
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