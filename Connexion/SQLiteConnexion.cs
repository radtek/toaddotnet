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