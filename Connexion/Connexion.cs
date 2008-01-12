/*
 * Created by: Pierre DELPORTE
 * Created: vendredi 20 juillet 2007
 */

using System;
using System.Data.Common;
using System.Windows.Forms;

namespace Connexion
{
    public class Connexion
    {
        //private DbProviderFactory fact;
        private DbConnection cnn;
        private string type = null;
        private OracleConnexion oracleConnexion;
        private SQLiteConnexion sqliteConnexion;
        private MSAccessConnexion msaccessConnexion;
        private MySQLConnexion mysqlConnexion;
        private SQLConnexion sqlConnexion;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public DbConnection Cnn
        {
            get
            {                
                return cnn;
            }
            internal set { cnn = value; }
        }

        public Connexion(string type)
        {
            Type = type;
            switch(Type)
            {
                case "Oracle":
                    oracleConnexion = new OracleConnexion();                    
                    break;
                case "MySQL":
                    mysqlConnexion = new MySQLConnexion();
                    break;
                case "MS-Access":
                    msaccessConnexion = new MSAccessConnexion();
                    break;
                case "SQLite":
                    sqliteConnexion = new SQLiteConnexion();                    
                    break;
                case "SQLServer":
                    sqlConnexion = new SQLConnexion();
                    break;
                default:
                    break;
            }
        }

        public bool Open()
        {
            bool rBool = false;
            switch (Type)
            {
                case "Oracle":
                    rBool = oracleConnexion.Open();
                    Cnn = oracleConnexion.cnn;                    
                    break;
                case "MySQL":
                    rBool = mysqlConnexion.Open();
                    Cnn = mysqlConnexion.cnn;
                    break;
                case "SQLite":
                    rBool = sqliteConnexion.Open();
                    Cnn = sqliteConnexion.cnn;
                    break;
                case "MS-Access":
                    rBool = msaccessConnexion.Open();
                    Cnn = msaccessConnexion.cnn;
                    break;
                case "SQLServer":
                    rBool = sqlConnexion.Open();
                    Cnn = sqlConnexion.cnn;
                    break;
                default:
                    break;
            }
            return rBool;
        }

        public bool Open(string param)
        {
            switch(Type)
            {
                case "SQLite":
                    sqliteConnexion.DbFile = param;
                    return Open();
                case "MS-Access":
                    msaccessConnexion.DbFile = param;
                    return Open();
                case "SQLServer":
                    sqlConnexion.Open(param);
                    return Open();
                default:
                    return false;
            }
        }

        public bool Open(string user, string password, string datasource)
        {
            switch (Type)
            {
                case "Oracle":
                    oracleConnexion.UserId = user;
                    oracleConnexion.Password = password;
                    oracleConnexion.DataSource = datasource;
                    return Open();
                    // break;
                case "MySQL":
                    mysqlConnexion.UserId = user;
                    mysqlConnexion.Password = password;
                    mysqlConnexion.DataSource = datasource;
                    return Open();
                    //break;
                case "MS-Access":
                    return Open();
                case "SQLite":
                    return Open();
                    // break;
                default:
                    break;
            }
            return false;
        }

        public bool Open(string user, string password, string datasource, string database)
        {
            switch (Type)
            {
                case "Oracle":
                    oracleConnexion.UserId = user;
                    oracleConnexion.Password = password;
                    oracleConnexion.DataSource = datasource;
                    return Open();
                    // break;
                case "MySQL":
                    mysqlConnexion.UserId = user;
                    mysqlConnexion.Password = password;
                    mysqlConnexion.DataSource = datasource;
                    mysqlConnexion.DataBase = database;
                    bool rbool = mysqlConnexion.Open(user, password, datasource, database);
                    if (rbool)
                        Cnn = mysqlConnexion.cnn;
                    return rbool;
                    //break;
                case "MS-Access":
                    return Open();
                case "SQLite":
                    return Open();
                    // break;
                default:
                    break;
            }
            return false;
        }

        public bool Close()
        {
            switch (Type)
            {
                case "Oracle":
                    return oracleConnexion.Close();
                    // break;
                case "MySQL":
                    return mysqlConnexion.Close();
                    //break;
                case "MS-Access":
                    return msaccessConnexion.Close();
                case "SQLite":
                    return sqliteConnexion.Close();
                    // break;
                default:
                    break;
            }
            return false;

        }

        // Execute a Command (Insert, Update, Delete)
        // and return true if success
        public bool DoCmd(string sql)
        {
            bool result = true;
            if (Cnn != null)
            {
                using (DbCommand cmd = Cnn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        string msg = "";
                        while (ex != null)
                        {
                            msg += ex.Message + "";
                            ex = ex.InnerException;
                        }
                        MessageBox.Show(msg + "\n"+sql, "Erreur", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }

                }                
            }
            return result;
        }
    }
}