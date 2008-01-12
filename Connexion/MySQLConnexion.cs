using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using MySql.Data;

namespace Connexion
{
    class MySQLConnexion
    {
        private DbProviderFactory fact;
        public DbConnection cnn;
        private string userId;
        private string password;
        private string dataSource;
        private string dataBase;

        public MySQLConnexion()
        {
            fact = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
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

        public string DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        public string DataBase
        {
            get { return dataBase; }
            set { dataBase = value; }
        }

        public bool Open()
        {
            try
            {
                
                OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder();
                cnn.ConnectionString = "Server=localhost; User ID=root; Password="; 

                cnn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;                
            }            
        }

        public bool Open(string user, string password, string datasource)
        {
            try
            {
                UserId = user;
                Password = password;
                DataSource = datasource;
                cnn.ConnectionString =
                    String.Format("Server={0}; User ID={1}; Password={2}", DataSource, UserId, Password);
                cnn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;                
            } 
        }

        public bool Open(string user, string password, string datasource, string database)
        {
            try
            {
                UserId = user;
                Password = password;
                DataSource = datasource;
                DataBase = database;
                cnn.ConnectionString =
                    String.Format("Server={0}; User ID={1}; database={2}", DataSource, UserId, DataBase);
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