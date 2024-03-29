/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte � Tous droits r�serv�s.

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
  modifier suivant les termes de la �GNU General Public License� telle que
  publi�e par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ult�rieure.
  
  Ce programme est distribu� dans l�espoir qu�il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans m�me la garantie implicite de COMMERCIALISABILIT�
  ni d�AD�QUATION � UN OBJECTIF PARTICULIER. Consultez la Licence G�n�rale
  Publique GNU pour plus de d�tails.
  
  Vous devriez avoir re�u une copie de la Licence G�n�rale Publique GNU avec
  ce programme ; si ce n�est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
using System;
using System.Windows.Forms;
using System.Xml;
using ULib;

namespace Register
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
                if (!string.IsNullOrEmpty(labelGUID.Text))
                    buttonOk.Enabled = true;
                XmlNodeList elements = Config.GetValue(xml, "//alf-solution/plugins");
                foreach (XmlElement element in elements)
                {
                    foreach (XmlElement pluginElem in element)
                    {
                        string plugname = pluginElem.Name;
                        string key = Config.GetInnerTextValue(xml, string.Format("//alf-solution/plugins/{0}/key", plugname));
                        dataGridView1.Rows.Add(
                            new string[] { plugname, key, "Never" });
                    }    
                    dataGridView1.AutoResizeColumns();
                }                
            }
            
            this.labelProductName.Text = Application.ProductName;
            this.labelProductVersion.Text = Application.ProductVersion;            
        }

        private void buttonRegisterApp_Click(object sender, EventArgs e)
        {
            labelGUID.Text = Guid.NewGuid().ToString();            
            string xmlResponse = Utils.RegisterApp(textBoxNom.Text, textBoxEmail.Text, labelGUID.Text);

            // Get the application configuration file.
            XmlNodeList elements = Config.GetValue(xmlResponse, "//alf-solution/RegisterApp/key");
            foreach (XmlElement element in elements)
            {
                labelGUID.Text = element.InnerText;
                string xml = Config.Load();
                XmlNodeList elems = Config.GetValue(xml, "//alf-solution/RegisterApp/client");
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
                buttonOk.Enabled = true;
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
            XmlNodeList elements = Config.GetValue(xmlResponse, "//alf-solution/Product/key");
            foreach (XmlElement element in elements)
            {
                dataGridView1.CurrentCell.Value = element.InnerText;
                string xml = Config.Load();
                XmlNodeList elems = Config.GetValue(xml, "//alf-solution/LicenceKey/plug[@name='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "']");
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