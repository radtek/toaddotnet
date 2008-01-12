using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PluginTypes;

namespace ToadDotNet
{
    public partial class ModuleInfoForm : Form
    {
        public ModuleInfoForm()
        {
            InitializeComponent();
        }
        public void setInfo(ModuleInfoAttribute moduleInfo)
        {
            nameTextBox.Text = moduleInfo.Name;
            authorTextBox.Text = moduleInfo.Author;
            languageTextBox.Text = moduleInfo.Language;
            descriptionTextBox.Text = moduleInfo.Description;
        }

        private void ModuleInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult != DialogResult.Yes) this.DialogResult = DialogResult.No;
        }
    }
}