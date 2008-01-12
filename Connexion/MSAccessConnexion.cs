using System;
using System.Configuration;
using System.Data.Common;

namespace Connexion
{
    public class MSAccessConnexion
    {
        private DbProviderFactory fact;
        public DbConnection cnn;
        private string dbFile;

        public MSAccessConnexion()
        {
            fact = DbProviderFactories.GetFactory("System.Data.OleDb");
            cnn = fact.CreateConnection();
        }

        public MSAccessConnexion(string dbFile)
        {
            fact = DbProviderFactories.GetFactory("System.Data.OleDb");
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
                    try
                    {
                        DbFile = ConfigurationManager.AppSettings["DbFile"];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }


                }
                cnn.ConnectionString = string.Format("Provider=Microsoft.Jet.OleDB.4.0 ;Data Source={0}", DbFile);
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