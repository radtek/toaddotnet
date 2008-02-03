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
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using ULib;

namespace ULib
{
    public static class Utils
    {
        
        public static string GetLicenceKey(string plugname, string name, string email, string guid)
        {
            string url = "http://www.asbl10bw.be/key/index.php5?cmd=LicenceKey&Option=" + plugname + "&EMail=" + email + "&Guid=" + guid + "&Name=" + name + "&Computer=" +
                         System.Environment.MachineName.ToString().ToLower();
            return DownloadPage(url);            
        }

        public static string RegisterApp(string name, string email, string guid)
        {
            string url = "http://www.asbl10bw.be/key/index.php5?cmd=RegisterApp&EMail=" + email + "&Guid=" + guid + "&Name=" + name + "Computer=" +
                         System.Environment.MachineName.ToString().ToLower();
            return DownloadPage(url);            
        }

        private static string DownloadPage(String url)
        {
            string CodeSource = "";
            try
            {
                // Create a new HttpWebRequest Object to the mentioned URL.            
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                myHttpWebRequest.MaximumAutomaticRedirections = 10;
                //Evite d'être renvoyé sur un autre site avec par exemple les balises :

                myHttpWebRequest.AllowAutoRedirect = true;

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //Console.WriteLine("Content length is {0}", myHttpWebResponse.ContentLength);
                //Console.WriteLine("Content type is {0}", myHttpWebResponse.ContentType);

                // Get the stream associated with the response.
                Stream receiveStream = myHttpWebResponse.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Console.WriteLine("Response stream received.");
                CodeSource = readStream.ReadToEnd();

                myHttpWebResponse.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                CodeSource = ex.Message;
            }            
            return CodeSource;
        }

        public static string GetKey(string PluginName, string labelGUID, string nom, string email)
        {
            //string PluginName = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (PluginName.Length < 25)
            {
                PluginName += labelGUID.Substring(labelGUID.Length - (25 - PluginName.Length));
            }
            string xmlResponse = GetLicenceKey(PluginName, nom, email, labelGUID);

            // Get the application configuration file.
            XmlNodeList elements = Config.GetValue(xmlResponse, "//alf-solution/Product/key");
            foreach (XmlElement element in elements)
            {
                return element.InnerText;                 
            }
            return null;
        }
        
        public static void OpenFolder(string docPath)
        {
            ProcessStartInfo exploreTest = new ProcessStartInfo();
            exploreTest.FileName = "explorer.exe";
            exploreTest.Arguments = docPath;
            Process.Start(exploreTest);
        }
        
        public static void OpenFile(string filePath)
        {
            try
            {
                ProcessStartInfo exploreTest = new ProcessStartInfo();
                exploreTest.FileName = filePath;
                Process.Start(exploreTest);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Unable to open the file {0}.\n{1}", filePath, e.Message),"File error");
            }
        	
        }
        
        public static bool CheckEmail(string email)
        {
            string pattern=@"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" + 
                           @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" + 
                           @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match = 
                Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            return match.Success;			
        }
        
    }
}