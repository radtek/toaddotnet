using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Connexion
{
    class SQLConnexion
    {
        private DbProviderFactory fact;
        public DbConnection cnn;
        private string userId;
        private string password;
        private string dataBase = "MyDatabase";
        private string serveur = "(local)\\SQLEXPRESS";
        private string connectString = null;

        public SQLConnexion()
        {
            fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            cnn = fact.CreateConnection();
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string DataBase
        {
            get { return dataBase; }
            set { dataBase = value; }
        }

        public string Serveur
        {
            get { return serveur; }
            set { serveur = value; }
        }

        public  string ConnectString
        {
            get { return connectString; }
            set { connectString = value; }
        }

        public bool Open(string ConnectionString)
        {
            try
            {
                this.ConnectString = ConnectionString;
                cnn.ConnectionString = ConnectionString;
                cnn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public bool Open()
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectString))
                {
                    cnn.ConnectionString = string.Format("server={0};database={1};Integrated Security=SSPI;max pool size=10;min pool size=5", Serveur, DataBase);    
                } 
                else
                {
                    cnn.ConnectionString = this.ConnectString;
                }
                
                cnn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
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
