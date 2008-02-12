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
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ULib
{
    
    public class TreeQuery
    {
        private System.Windows.Forms.TreeNode tn;
        private Connexion.Connexion connexion;
        public Thread mythread;

        #region constructeur
        public TreeQuery()
        {
            
        }

        public TreeQuery(TreeNode tn)
        {
            this.tn = tn;
        }

        public TreeQuery(TreeNode tn, Connexion.Connexion connexion)
        {
            this.tn = tn;
            this.connexion = connexion;
        }

        #endregion

        #region Delegate

        private delegate void addNode(TreeNode nolde, TreeNode newNode);
        private void AddNode(TreeNode node, TreeNode newNode)
        {
            node.Nodes.Add(newNode);
        }
        #endregion

        #region publicMethode
        public void Start(object obj)
        {
            try
            {
                if (mythread != null && mythread.IsAlive)
                    mythread.Abort();
                mythread = new Thread((Display));
                mythread.Start(Convert.ToString(obj));                
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Stop()
        {
            try
            {
                if (mythread != null && mythread.IsAlive)
                    mythread.Abort();
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Display(object obj)
        {
            //Thread.Sleep(100);
            DateTime startTime = DateTime.Now;
            bool bConnexion = true;
            if (this.connexion == null || this.connexion.Cnn == null || this.connexion.Cnn.State.ToString() == "Closed")
            {
                connexion = new Connexion.Connexion("Oracle");
                bConnexion = connexion.Open();
            }
            if (bConnexion)
            {
                DisplayQueryData(connexion, Convert.ToString(obj), tn);
                //connexion.Close();
            }
            TimeSpan elapsed = DateTime.Now - startTime;           
        }
        #endregion

        #region display
        private void DisplayQueryData(Connexion.Connexion connexion, string SQL, TreeNode treeNode)
        {
            try
            {
                //string SelectedTable = treeViewOracleSchema.SelectedNode.Text;
                using (DbCommand cmd = connexion.Cnn.CreateCommand())
                {
                    //int NumRec = 0;                    
                    cmd.CommandText = SQL;
                    cmd.Prepare();
                    //int colno = 0;
                    using (DbDataReader rd = cmd.ExecuteReader())
                    {
                        
                        while (rd.Read())
                        {
                            //listBoxOracleTables.Items.Add(rd.GetString(0));
                            DbObjectItem tnDbOI = ((DbObjectItem) tn.Tag);
                            DbObjectItem DbOI = new DbObjectItem(rd.GetString(0), tnDbOI.Type.Substring(0, tnDbOI.Type.Length - 1));
                            //string tablename = rd.GetString(0);
                            TreeNode node = new TreeNode(DbOI.Name);
                            //DbOI.Type = tnDbOI.Type.Substring(0, tnDbOI.Type.Length - 1);
                            node.Tag = DbOI; // tnDbOI.Type.Substring(0, tnDbOI.Type.Length - 1);
                            switch (DbOI.Type.ToLower())
                            {
                                case "table":
                                    node.SelectedImageIndex = 2;
                                    node.ImageIndex = 2;
                                    break;
                                case "view":
                                    node.SelectedImageIndex = 2;
                                    node.ImageIndex = 2;
                                    break;
                                case "function":
                                    node.SelectedImageIndex = 3;
                                    node.ImageIndex = 3;
                                    break;
                                case "procedure":
                                    node.SelectedImageIndex = 4;
                                    node.ImageIndex = 4;
                                    break;
                                case "package spec":
                                case "package body":
                                case "package":
                                    node.SelectedImageIndex = 5;
                                    node.ImageIndex = 5;
                                    break;
                                case "trigger":
                                    node.SelectedImageIndex = 6;
                                    node.ImageIndex = 6;
                                    break;
                                case "sequence":
                                    node.SelectedImageIndex = 7;
                                    node.ImageIndex = 7;
                                    break;
                                case "field":
                                    node.SelectedImageIndex = 8;
                                    node.ImageIndex = 8;
                                    break;
                                case "fk":
                                    node.SelectedImageIndex = 9;
                                    node.ImageIndex = 9;
                                    break;
                            }
                            if (rd.GetString(1) != "VALID")
                            {
                                node.SelectedImageIndex = 12;
                                node.ImageIndex = 12;                                    
                            }
                            if (tn.TreeView != null && tn.TreeView.InvokeRequired)
                            {
                                tn.TreeView.Invoke(new addNode(AddNode), new object[] {treeNode, node});                                
                            } else
                            {
                                treeNode.Nodes.Add(node);                                
                            }                            
                        }
                        rd.Close();
                    }                                 
                }
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    errorMessage += "\n" + e.Message;
                }
                MessageBox.Show(errorMessage, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
