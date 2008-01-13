using System.IO;
using System.Xml;

namespace Membs
{
    public static class Config
    {
        private static string filename = "ToadDotNet.xml";
        public static XmlNodeList GetValue(string xmlData, string section)
        {
            XmlDocument xml = new XmlDocument();
            xmlData = xmlData.Replace("&", "&amp;");
            xml.LoadXml(xmlData);

            XmlNodeList elements = xml.SelectNodes(section);
            return elements;
        }

        public static string GetInnerTextValue(string xmlData, string path)
        {
            XmlNode node = GetNode(xmlData, path);
            if (node != null)
                return node.InnerText;
            else
                return null;
        }

        public static XmlNode GetNode(string xmlData, string section)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);

            XmlNode node = xml.SelectSingleNode(section); // .SelectNodes(section);
            return node;
        }

        public static XmlElement GetElement(string xmlData, string section)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);

            XmlNodeList elements = xml.SelectNodes(section); // .SelectNodes(section);
            foreach (XmlElement element in elements)
            {
                return element;
            }
            return null;
        }

        public static string Load()
        {
            string xml = null;
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                xml = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            return xml;
        }

        public static void Save(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(filename);
        }

        public static string SetValue(string xmlData, string section, string tag, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);

            XmlNodeList elemList = FindNode(doc.DocumentElement, section);
            XmlNode node = null;
            if (elemList.Count > 0)
            {
                node = elemList[0];
            }
            else
            {
                char[] splitter  = {'/'};
                XmlNode nodeParent = doc.DocumentElement;
                foreach (string sec in section.Split(splitter))
                {
                    node =  FindNode(nodeParent, sec);
                    if (node == null)
                    {
                        node = doc.CreateElement(sec);
                        nodeParent.AppendChild(node);
                    }
                    nodeParent = node;
                }                
            }
            XmlNode nodetag = node.SelectSingleNode(tag);
            if (nodetag != null)
            {
                nodetag.InnerText = value;
            }
            else
            {
                XmlElement elem = doc.CreateElement(tag);
                elem.InnerText = value;
                node.AppendChild(elem);
            }
            return doc.InnerXml;
        }

        public static XmlNodeList GetSection(string xmlData, string section)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);
            return FindNode(doc.DocumentElement, section);
        }

        private static XmlNodeList FindNode(XmlElement doc, string node)
        {
            return doc.GetElementsByTagName(node);
        }

        private static XmlNode FindNode(XmlNode xmlNode, string node)
        {
            foreach (XmlNode child in xmlNode)
            {
                if (child.Name == node)
                    return child;
                
            }
            return null;
        }

        public static string Nom()
        {
            return GetInnerTextValue(Load(), "//membs/RegisterApp/client/nom");
        }

        public static string PublicKey()
        {
            return GetInnerTextValue(Load(), "//membs/RegisterApp/client/PublicKey");          
        }

        public static string Email()
        {
            return GetInnerTextValue(Load(), "//membs/RegisterApp/client/email");      
        }

    }
}