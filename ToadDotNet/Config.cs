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
            return GetInnerTextValue(Load(), "//alf-solution/RegisterApp/client/nom");
        }

        public static string PublicKey()
        {
            return GetInnerTextValue(Load(), "//alf-solution/RegisterApp/client/PublicKey");          
        }

        public static string Email()
        {
            return GetInnerTextValue(Load(), "//alf-solution/RegisterApp/client/email");      
        }

        public static string TnsPath()
        {
            return GetInnerTextValue(Load(), "//alf-solution/db/tnsanmes.ora");
        }

    }
}