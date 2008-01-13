/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte � Tous droits r�serv�s.

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
  modifier suivant les termes de la �GNU General Public License� telle que
  publi�e par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ult�rieure.
  
  Ce programme est distribu� dans l�espoir qu�il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans m�me la garantie implicite de COMMERCIALISABILIT�
  ni d�AD�QUATION � UN OBJECTIF PARTICULIER. Consultez la Licence G�n�rale
  Publique GNU pour plus de d�tails.
  
  Vous devriez avoir re�u une copie de la Licence G�n�rale Publique GNU avec
  ce programme ; si ce n�est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
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