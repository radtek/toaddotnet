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
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using Schema;
using ULib;

namespace MnuConnection
{
    public class CMnuConnection : IMenuAddOn, IFormAddOn
    {
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        private Form parentForm;

        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        public PlugEvent PlugSender
        {
            get { return plugSender; }
            set { plugSender = value; }
        }

        public Form ParentForm
        {
            get { return parentForm; }
            set { parentForm = value; }
        }

        public void Install(MenuStrip menu)
        {
            PlugUtils.AddMenu(menu, "&Session", "&Connect...", new EventHandler(SessionConnectMenuItem_Click));
            PlugUtils.AddMenu(menu, "&Session", "&Disconnect", new EventHandler(SessionDisconnectMenuItem_Click));
        }

        public void Install(Form form)
        {
            ParentForm = form;
        }

        public void EventPlug(PlugEvent e)
        {
            if (e != null)
            {
                PlugSender = e;
                PlugSender.evtHandler += new PlugEvent._evtHandler(EventProcess);
            }
        }

        private void SessionConnectMenuItem_Click(object sender, EventArgs e)
        {
            FormConnection formConnection = new FormConnection();
            //formConnection.MdiParent = this.ParentForm;
            if (formConnection.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(formConnection.textBoxOracleUserId.Text) 
                    && !string.IsNullOrEmpty(formConnection.textBoxOraclePassword.Text)
                    && !string.IsNullOrEmpty(formConnection.TNSNamesComboBox.Text))
                {
                    DataGridViewRow dgvr = formConnection.dataGridViewConnection.CurrentRow;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Config.Load());
                    string SearchCriteria =
                        string.Format(
                            "//alf-solution/LastConnections/info[@userid='{0}' and @datasource='{1}']",
                            formConnection.textBoxOracleUserId.Text, formConnection.TNSNamesComboBox.Text);
                    XmlNode node = doc.SelectSingleNode(SearchCriteria);
                    if (node == null)

                    //if (dgvr == null || dgvr.Cells["user"].Value.ToString() != formConnection.textBoxOracleUserId.Text || dgvr.Cells["password"].Value == null ||
                    //    dgvr.Cells["password"].Value.ToString() != formConnection.textBoxOraclePassword.Text ||
                    //    dgvr.Cells["datasource"].Value.ToString() != formConnection.TNSNamesComboBox.Text)
                    {
                        //XmlDocument doc = new XmlDocument();
                        doc.LoadXml(Config.Load());
                        
                        node =  doc.SelectSingleNode("//alf-solution/LastConnections");
                        if (node == null)
                        {
                            node = doc.CreateElement("LastConnections");
                            doc.SelectSingleNode("//alf-solution").AppendChild(node);
                        }
                        XmlNode infoElem = doc.CreateElement("info");
                        XmlAttribute attr = doc.CreateAttribute("userid");
                        attr.Value = formConnection.textBoxOracleUserId.Text;
                        infoElem.Attributes.Append(attr);
                        if (formConnection.checkBoxSavePassword.Checked)
                        {
                            attr = doc.CreateAttribute("password");
                            attr.Value = formConnection.textBoxOraclePassword.Text;
                            infoElem.Attributes.Append(attr);                            
                        }
                        attr = doc.CreateAttribute("datasource");
                        attr.Value = formConnection.TNSNamesComboBox.Text;
                        infoElem.Attributes.Append(attr);
                        attr = doc.CreateAttribute("date");
                        attr.Value = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                        infoElem.Attributes.Append(attr);

                        node.AppendChild(infoElem);
                        
                        Config.Save(doc.InnerXml);
                    }
                    MainForm mainForm = new MainForm();
                    plugSender = mainForm.plugEvent;
                    mainForm.MdiParent = ParentForm;
                    mainForm.Show();
                    SendConnectionInfo(formConnection.textBoxOracleUserId.Text, formConnection.textBoxOraclePassword.Text, formConnection.TNSNamesComboBox.Text);
                }
            }
        }

        private void SessionDisconnectMenuItem_Click(object sender, EventArgs e)
        {
            //AboutBox1 aboutBox = new AboutBox1();
            //aboutBox.ShowDialog();
        }

        public void EventProcess(object sender, string data)
        {
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(data);
        }

        private void SendConnectionInfo(string userid, string password, string datasource)
        {            
            //Personnes personne = (Personnes)PersonListBox.SelectedItem;
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode rootNode = doc.CreateElement("ToadDotNet");
            doc.AppendChild(rootNode);

            XmlNode actionNode = doc.CreateElement("action");
            actionNode.InnerText = "connect";
            //XmlAttribute actionAttr = doc.CreateAttribute("connection")
            rootNode.AppendChild(actionNode);

            XmlNode productNode = doc.CreateElement("connection");
            XmlAttribute productAttribute = doc.CreateAttribute("userid");
            productAttribute.Value = userid;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("password");
            productAttribute.Value = password;
            productNode.Attributes.Append(productAttribute);

            productAttribute = doc.CreateAttribute("datasource");
            productAttribute.Value = datasource;
            productNode.Attributes.Append(productAttribute);

            actionNode.AppendChild(productNode);

            if (PlugSender != null)
                PlugSender.Send(doc.OuterXml);
        }
    }
}
