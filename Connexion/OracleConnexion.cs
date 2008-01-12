using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OracleClient;

namespace Connexion
{
    public class OracleConnexion
    {

        private DbProviderFactory fact;
        public DbConnection cnn;
        private string userId;
        private string password;
        private string dataSource;

        public OracleConnexion()
        {
            fact = DbProviderFactories.GetFactory("System.Data.OracleClient");
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

        public bool Open()
        {
            try
            {
                OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder();
                builder.UserID = UserId;
                builder.Password = Password;
                builder.DataSource = DataSource;
                cnn.ConnectionString = builder.ConnectionString; //"Password=S3I;User ID=S3I;Data Source=EA.NETIKA.DEV;Persist Security Info=True";
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
                OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder();
                builder.UserID = user;
                builder.Password = password;
                builder.DataSource = datasource;
                cnn.ConnectionString = builder.ConnectionString; //"Password=S3I;User ID=S3I;Data Source=EA.NETIKA.DEV;Persist Security Info=True";
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