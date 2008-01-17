using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Connexion.utils.Oracle;
using ULib;

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
            // Load all entries from the tnsnames.ora
            TnsParser parser = new TnsParser();
            parser.Parse();
            TnsEntryCollectionType SourceEntries = parser.TnsFileEntries;
            foreach (TnsEntryType tnsEntry in SourceEntries)
            {
                TNSNamesComboBox.Items.Add(tnsEntry.TnsnameEntry);                
            }

            // retrieve last connections used
            int numcol = dataGridViewConnection.Columns.Add("user", "Userid");
            numcol = dataGridViewConnection.Columns.Add("password", "Password");
            dataGridViewConnection.Columns[numcol].Visible = false;
            numcol = dataGridViewConnection.Columns.Add("datasource", "Datasource");
            numcol = dataGridViewConnection.Columns.Add("lastconnect", "Last connection");

            XmlNodeList LastConnectionList = Config.GetValue(Config.Load(), "//alf-solution/LastConnections");

            foreach (XmlElement LastConnectionInfos in LastConnectionList)
            {
                foreach (XmlElement LastConnectionInfo in LastConnectionInfos)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    for (int i = 0; i < dataGridViewConnection.Columns.Count; i++)
                    {
                        dgvr.Cells.Add(new DataGridViewTextBoxCell());
                    }
                    dgvr.Cells[0].Value = LastConnectionInfo.Attributes.GetNamedItem("userid").Value;
                    dgvr.Cells[1].Value = LastConnectionInfo.Attributes.GetNamedItem("password").Value;
                    dgvr.Cells[2].Value = LastConnectionInfo.Attributes.GetNamedItem("datasource").Value;
                    dgvr.Cells[3].Value = LastConnectionInfo.Attributes.GetNamedItem("date").Value;
                    dataGridViewConnection.Rows.Add(dgvr);
                }                                    
            }
            dataGridViewConnection.AutoResizeColumns();            
        }

        private void dataGridViewConnection_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridViewConnection.Rows[e.RowIndex];
            textBoxOracleUserId.Text = dgvr.Cells["user"].Value.ToString();
            textBoxOraclePassword.Text = dgvr.Cells["password"].Value.ToString();
            TNSNamesComboBox.Text = dgvr.Cells["datasource"].Value.ToString();
        }
    }
}