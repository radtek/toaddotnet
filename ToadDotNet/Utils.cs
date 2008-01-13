using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using Membs;

namespace ToadDotNet
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
            XmlNodeList elements = Config.GetValue(xmlResponse, "//GestMembre/Product/key");
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