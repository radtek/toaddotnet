using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Connexion.utils.Oracle;

namespace MnuConnection
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            TnsParser parser = new TnsParser();
            parser.Parse();
            TnsEntryCollectionType SourceEntries = parser.TnsFileEntries;
            foreach (TnsEntryType tnsEntry in SourceEntries)
            {
                TNSNamesComboBox.Items.Add(tnsEntry.TnsnameEntry);                
            }
        }
    }
}