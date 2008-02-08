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
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Util;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using Intellisense;
using PluginTypes;
using ULib;


namespace TBQuery
{
    public partial class UCQuery : UserControl, ITabPageAddOn
    {
        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        private Connexion.Connexion connexion = new Connexion.Connexion("Oracle");
        
        private TabControl tc;
        private static readonly int DEFAULT_TABPOSITION = 10;
        private int tabPosition = DEFAULT_TABPOSITION;

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCQuery()
        {
            InitializeComponent();
        }

        #region PluginInstall
        /// <summary> 
        /// Required implementation of the interface.
        /// </summary>
        public void Install(TabControl tabControl)
        {
            // Create a new tab page as we implement a ITabPageAddOn
            TabPage tp = new TabPage("Query");
            tc = tabControl;
            string TabPosition = Config.GetText("//alf-solution/plugins/TBQuery/tab/position");
            if (string.IsNullOrEmpty(TabPosition))
            {
                Config.SaveText("/alf-solution/plugins/TBQuery/tab/position", DEFAULT_TABPOSITION.ToString());
                tabPosition = DEFAULT_TABPOSITION;
            }
            else
            {
                tabPosition = Convert.ToInt32(TabPosition);
            }
            // Insert the new tab page to the TabControl of the main window's application           
            if (tabPosition > tc.TabPages.Count)
                tc.TabPages.Add(tp);
            else 
                tc.TabPages.Insert(tabPosition, tp); 
            
            // Set automatic resizing of the UserControl
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            this.Height = tp.Height - 10;
            this.Width = tp.Width - 10;
            this.Top = 5;
            this.Left = 5;
            // Add the UserControl to the tab page
            tp.Controls.Add(this);
        }
        #endregion

        #region EventProcess
        /// <summary> 
        /// Required implementation of the event interface.
        /// </summary>
        public void EventPlug(PlugEvent e)
        {
            plugSender = e;
            plugSender.evtHandler += new PlugEvent._evtHandler(EventProcess);
        }

        /// <summary> 
        /// Method to execute when event is fired
        /// </summary>
        public void EventProcess(object sender, string data)
        {
            //MessageBox.Show("test " + data);
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(data);
            foreach (XmlNode xmlNodeAction in xmlData.GetElementsByTagName("action"))
            {
                switch (xmlNodeAction.InnerText)
                {
                    case "connect":
                        // Get Info for the oracle connection
                        XmlNode xmlNode = xmlData.SelectSingleNode("//ToadDotNet/action/connection");
                        if (xmlNode != null)
                        {
                            connexion.OracleConnexion.UserId = xmlNode.Attributes.GetNamedItem("userid").Value;
                            connexion.OracleConnexion.Password = xmlNode.Attributes.GetNamedItem("password").Value;
                            connexion.OracleConnexion.DataSource = xmlNode.Attributes.GetNamedItem("datasource").Value;
                            if (connexion.IsOpen)
                            {
                                sqlEditor1.SetConnexion = connexion;
                                connexion.Close();
                            }
                            else
                            {
                                connexion.Open();
                                sqlEditor1.SetConnexion = connexion;
                            }                            
                        }
                        break;                    
                    default:
                        break;
                }
            }
        }
        #endregion

        private void UCQuery_Load(object sender, EventArgs e)
        {            
            
        }  
    }
}
