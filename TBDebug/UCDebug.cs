using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using PluginTypes;
using ULib;

namespace TBDebug
{
    public partial class UCDebug : UserControl, ITabPageAddOn
    {
        private TabPage tp = null;
        private TabControl tc = null;

        private static readonly int DEFAULT_TABPOSITION = 99;
        private int tabPosition = DEFAULT_TABPOSITION; // default position for the tab;

        /// <summary> 
        /// Private attribute for the event.
        /// </summary>
        private PlugEvent plugSender;

        /// <summary> 
        /// Default Constructor.
        /// </summary>
        public UCDebug()
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
            TabPage tp = new TabPage("Debug");
            tc = tabControl;
            //tabControl.TabPages.Add(tp);
            string TabPosition = Config.GetText("//alf-solution/plugins/TBColumns/tab/position");
            if (string.IsNullOrEmpty(TabPosition))
            {
                Config.SaveText("/alf-solution/plugins/TBColumns/tab/position", DEFAULT_TABPOSITION.ToString());
                tabPosition = DEFAULT_TABPOSITION;
            }
            else
            {
                tabPosition = Convert.ToInt32(TabPosition);
            }
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
            textBoxDebug.Text += data + Environment.NewLine;            
            int index = treeViewDebug.Nodes.Add(GetTreeNodeFromXML(data));
            if (index > 0)
                treeViewDebug.Nodes[index-1].Collapse(false);
            treeViewDebug.Nodes[index].ExpandAll();
        }

        #endregion

        private static TreeNode DoElement(XmlNode xn)
        {
            TreeNode tn = new TreeNode(xn.Name);
            // créé l'objet TreeNode de base qui va recevoir les données XML
            // il portera le nom de la balise XML

            // il y a des attributs
            if (xn.Attributes != null && xn.Attributes.Count > 0)
            {
                TreeNode attr_node = tn.Nodes.Add("Attibutes");
                // on creer un sous arbre
                // qui contiendra les attributs

                foreach (XmlAttribute attr in xn.Attributes) // parcours des attributs
                    attr_node.Nodes.Add(attr.Name + "=" + attr.Value);
                // ajout des attributs,
                // nom de l'attribut puis sa valeur
            }


            // le sous-element XML n'est pas une balise mais une valeur textuelle
            if (xn.Value != null && xn.Value != "")
                tn.Text = xn.Value;
            else
            {
                // parcours des sous-elements XML
                foreach (XmlNode subxn in xn.ChildNodes)
                    tn.Nodes.Add(DoElement(subxn));
                // on ajoute le sous-element ainsi que ses propres
                // sous-element recursivement
            }
            return (tn);
            // une fois la recursion terminée, on renvoie le TreeNode, qui contient ses sous TreeNode
        }

        public static TreeNode GetTreeNodeFromXML(string xml_file)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml_file);
            // charge un fichier xml, cette fonction se charge toute seule de gerer le fichier
            // pas besoin de Close ou autre

            XmlNode root = doc.DocumentElement;
            // root devient le premier element XML
            // (il n'y en a jamais plus d'un en XML, il est toujours unique)

            return (DoElement(root));
            // appel a DoElement
        }
    }
}