using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Connexion.utils.Oracle
{
    /// <summary>
    /// TNSNames.ora parser file.
    /// </summary>
    public class TnsParser
    {
        #region instance members

        private TnsEntryCollectionType _tnsEntries = null;

        public TnsEntryCollectionType TnsFileEntries
        {
            get { return _tnsEntries; }
        }

        public TnsParser() : base()
        {
        }

        /// <summary>
        /// Parse default TnsNames.ora file
        /// </summary>
        public void Parse()
        {
            //Read the Oracle Home from registry            
            Microsoft.Win32.RegistryKey oracleHomeKey =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ORACLE");
            if (oracleHomeKey != null)
            {
                string tnsFile =
                string.Format("{0}\\Network\\Admin\\tnsnames.ora", oracleHomeKey.GetValue("ORACLE_HOME").ToString());
                if (!string.IsNullOrEmpty(tnsFile))
                {
                    Parse(tnsFile);
                }
                else
                {
                    throw new ArgumentException("ORACLE_HOME key does not exist does not exist ");
                }
            }
            else
            {
                throw new ArgumentException("ORACLE key does not exist into the registry");
            }
            
        }

        /// <summary>
        /// Parse specified TnsNames.Ora file
        /// </summary>
        /// <param name="file">Tns File Name</param>
        public void Parse(string file)
        {
            string line = string.Empty;
            string entry = string.Empty;
            int countOpenBrackets = 0;
            int countCloseBrackets = 0;

            //validate arguments
            if (!File.Exists(file))
                throw new ArgumentException("{0} File does not exist", "file");

            _tnsEntries = new TnsEntryCollectionType(file);

            using (StreamReader reader = new StreamReader(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    //Ignore comments
                    if (line.StartsWith("#"))
                        continue;
                    entry += line;
                    for (int idx = 0; idx < line.Length; idx++)
                    {
                        switch (line[idx])
                        {
                            case '(':
                                countOpenBrackets++;
                                break;
                            case ')':
                                countCloseBrackets++;
                                break;
                        }
                    }
                    if ((countOpenBrackets == countCloseBrackets) &&
                        (countOpenBrackets > 0) && (countCloseBrackets > 0))
                    {
                        ProcessTnsEntry(entry);
                        countOpenBrackets = 0;
                        countCloseBrackets = 0;
                        entry = string.Empty;
                    }
                }
            }
        }

        private void ProcessTnsEntry(string entry)
        {
            int openBracketIdx = 0;
            int closeBracketIdx = 0;
            int equaltoIdx = 0;
            TnsEntryType tnsEntry = new TnsEntryType();

            openBracketIdx = entry.IndexOf('(', 0);
            tnsEntry.TnsnameEntry = entry.Substring(0, entry.IndexOf('=', 0) - 1).Trim();
            while (openBracketIdx > 0)
            {
                equaltoIdx = entry.IndexOf('=', openBracketIdx);
                //get the token
                string token = entry.Substring((openBracketIdx + 1), (equaltoIdx - openBracketIdx - 1));
                //Trim whitespaces
                token = token.Trim().ToUpper();
                //if the current token is the one we are interested in
                //get the token value 
                closeBracketIdx = entry.IndexOf(')', equaltoIdx);
                string tokenValue = entry.Substring((equaltoIdx + 1), (closeBracketIdx - equaltoIdx - 1));
                //trim whitespaces
                tokenValue = tokenValue.Trim();
                switch (token)
                {
                    case "SERVICE_NAME":
                        tnsEntry.ServiceName = tokenValue;
                        break;
                    case "HOST":
                        tnsEntry.HostName = tokenValue;
                        break;
                    case "SID":
                        tnsEntry.Sid = tokenValue;
                        break;
                    case "PORT":
                        tnsEntry.PortNumber = int.Parse(tokenValue);
                        break;
                }
                //get index of next open bracket
                openBracketIdx = entry.IndexOf('(', openBracketIdx + 1);
            }
            lock (_tnsEntries.SyncRoot)
            {
                _tnsEntries.Add(tnsEntry);
            }
        }

        #endregion
    }
}