/*
 * Created by SharpDevelop.
 * User: pdelporte
 * Date: 6/06/2007
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Configuration;
using System.Data.Common;

namespace Connexion
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class SQLiteConnexion
    {
        private DbProviderFactory fact;
        public DbConnection cnn;
        private string dbFile;

        public SQLiteConnexion()
        {
            fact = DbProviderFactories.GetFactory("System.Data.SQLite");
            cnn = fact.CreateConnection();
        }

        public SQLiteConnexion(string dbFile)
        {
            fact = DbProviderFactories.GetFactory("System.Data.SQLite");
            cnn = fact.CreateConnection();
            this.DbFile = dbFile;
        }

        public string DbFile
        {
            get { return dbFile; }
            set { dbFile = value; }
        }

        public bool Open()
        {
            try
            {
                if (DbFile == null)
                {
                    DbFile = ConfigurationManager.AppSettings["DbFile"];
                }

                cnn.ConnectionString = "Data Source=" + DbFile;
                cnn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public bool Open(string dbFile)
        {
            DbFile = dbFile;
            return Open();
        }

        public bool Close()
        {
            if (cnn != null)
                cnn.Close();
            else
                return false;
            return true;
        }
    }
}