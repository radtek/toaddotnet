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

        private bool isOpen = false;

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

        public OracleConnexion OracleConnexion
        {
            get { return oracleConnexion; }
            set { oracleConnexion = value; }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        public Connexion(string type)
        {
            Type = type;
            switch(Type)
            {
                case "Oracle":
                    OracleConnexion = new OracleConnexion();                    
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
            //bool rBool = false;
            switch (Type)
            {
                case "Oracle":
                    isOpen = OracleConnexion.Open();
                    Cnn = OracleConnexion.cnn;                    
                    break;
                case "MySQL":
                    isOpen = mysqlConnexion.Open();
                    Cnn = mysqlConnexion.cnn;
                    break;
                case "SQLite":
                    isOpen = sqliteConnexion.Open();
                    Cnn = sqliteConnexion.cnn;
                    break;
                case "MS-Access":
                    isOpen = msaccessConnexion.Open();
                    Cnn = msaccessConnexion.cnn;
                    break;
                case "SQLServer":
                    isOpen = sqlConnexion.Open();
                    Cnn = sqlConnexion.cnn;
                    break;
                default:
                    break;
            }
            return isOpen;
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
                    OracleConnexion.UserId = user;
                    OracleConnexion.Password = password;
                    OracleConnexion.DataSource = datasource;
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
                    OracleConnexion.UserId = user;
                    OracleConnexion.Password = password;
                    OracleConnexion.DataSource = datasource;
                    return Open();
                    // break;
                case "MySQL":
                    mysqlConnexion.UserId = user;
                    mysqlConnexion.Password = password;
                    mysqlConnexion.DataSource = datasource;
                    mysqlConnexion.DataBase = database;
                    isOpen = mysqlConnexion.Open(user, password, datasource, database);
                    if (isOpen)
                        Cnn = mysqlConnexion.cnn;
                    return isOpen;
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
                    isOpen = OracleConnexion.Close();
                    break;
                case "MySQL":
                    isOpen = mysqlConnexion.Close();
                    break;
                case "MS-Access":
                    isOpen = msaccessConnexion.Close();
                    break;
                case "SQLite":
                    isOpen = sqliteConnexion.Close();
                    break;
                default:
                    break;
            }
            return isOpen;

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