using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using Membs;

namespace ToadDotNet
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            string xml = Config.Load();
            if (xml != null)
            {
                textBoxNom.Text = Config.Nom();
                labelGUID.Text = Config.PublicKey();
                textBoxEmail.Text = Config.Email();
                //XmlNodeList elements = Config.GetValue(xml, "//Membs/RegisterApp/client");
                //foreach (XmlElement element in elements)
                //{
                //    textBoxNom.Text = element.GetAttribute("nom");
                //    labelGUID.Text = element.GetAttribute("PublicKey");
                //    textBoxEmail.Text = element.GetAttribute("email");
                //}


                XmlNodeList elements = Config.GetValue(xml, "//Membs/LicenceKey/plug");
                foreach (XmlElement element in elements)
                {
                    dataGridView1.Rows.Add(
                        new string[] { element.GetAttribute("name", ""), element.GetAttribute("key", ""), "Never" });
                }                
            }
            
            this.labelProductName.Text = Application.ProductName;
            this.labelProductVersion.Text = Application.ProductVersion;
            //if (labelGUID.Text != "Unregister" )
            //{
            //    buttonRegisterApp.Enabled = false;
            //}
            //else
            //{
            //    buttonRegisterApp.Enabled = false;
            //}
        }

        private void buttonRegisterApp_Click(object sender, EventArgs e)
        {
            labelGUID.Text = Guid.NewGuid().ToString();            
            string xmlResponse = Utils.RegisterApp(textBoxNom.Text, textBoxEmail.Text, labelGUID.Text);

            // Get the application configuration file.
            XmlNodeList elements = Config.GetValue(xmlResponse, "//GestMembre/RegisterApp/key");
            foreach (XmlElement element in elements)
            {
                labelGUID.Text = element.InnerText;
                string xml = Config.Load();
                XmlNodeList elems = Config.GetValue(xml, "//membs/RegisterApp/client");
                for (int i = 0; i < elems.Count; i++ )
                {
                    XmlElement elem = elems[i] as XmlElement;
                    xml = Config.SetValue(xml, "RegisterApp/client", "nom", textBoxNom.Text);
                    xml = Config.SetValue(xml, "RegisterApp/client", "email", textBoxEmail.Text);
                    xml = Config.SetValue(xml, "RegisterApp/client", "PublicKey", element.InnerText);
                    Config.Save(xml);
                }
                if (elems.Count == 0)
                {
                    xml = Config.SetValue(xml, "RegisterApp/client", "nom", textBoxNom.Text);
                    xml = Config.SetValue(xml, "RegisterApp/client", "email", textBoxEmail.Text);
                    xml = Config.SetValue(xml, "RegisterApp/client", "PublicKey", element.InnerText);
                    Config.Save(xml);
                }
                
            }                        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value == null || dataGridView1.CurrentCell.Value.ToString() == "")
            {
                buttonSubmit.Enabled = true;
            }
            else
            {
                buttonSubmit.Enabled = false;
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            string PluginName = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (PluginName.Length < 25)
            {
                PluginName += labelGUID.Text.Substring(labelGUID.Text.Length - (25 - PluginName.Length));
            }
            string xmlResponse = Utils.GetLicenceKey(PluginName,textBoxNom.Text, textBoxEmail.Text, labelGUID.Text);

            // Get the application configuration file.
            XmlNodeList elements = Config.GetValue(xmlResponse, "//GestMembre/Product/key");
            foreach (XmlElement element in elements)
            {
                dataGridView1.CurrentCell.Value = element.InnerText;
                string xml = Config.Load();
                XmlNodeList elems = Config.GetValue(xml, "//Concert/LicenceKey/plug[@name='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "']");
                foreach (XmlElement elem in elems)
                {
                    elem.SetAttribute("key", element.InnerText);
                    Config.Save(elem.OwnerDocument.InnerXml);
                }

            }
        }        
        
        void TextBoxNomTextChanged(object sender, EventArgs e)
        {
            if (this.textBoxNom.Text.Length > 5 && Utils.CheckEmail(this.textBoxEmail.Text))
            {
                this.buttonRegisterApp.Enabled = true;
            }
            else 
            {
                this.buttonRegisterApp.Enabled = false;
            }
        }                
    }
}