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
