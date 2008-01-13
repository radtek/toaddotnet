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