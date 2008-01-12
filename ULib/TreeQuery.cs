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
                    int NumRec = 0;
                    /*
                    try
                    {
                        string SQLCount = "SELECT count(*) " + SQL.Substring(SQL.ToUpper().IndexOf("FROM"));
                        cmd.CommandText = SQLCount; // string.Format("SELECT count(*) FROM {0}", SelectedTable);
                        cmd.Prepare();
                        NumRec = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        NumRec = 0;
                    }
                    */
                    //string SQL = string.Format("SELECT * FROM {0}", SelectedTable);
                    cmd.CommandText = SQL;
                    cmd.Prepare();
                    //int colno = 0;
                    using (DbDataReader rd = cmd.ExecuteReader())
                    {
                        
                        while (rd.Read())
                        {
                            //listBoxOracleTables.Items.Add(rd.GetString(0));
                            string tablename = rd.GetString(0);
                            TreeNode node = new TreeNode(tablename);
                            node.Tag = tn.Tag.ToString().Substring(0, tn.Tag.ToString().Length - 1);
                            //TreeNode FieldsNode = new TreeNode("Fields");
                            //FieldsNode.Tag = "fields";                            
                            if (tn.TreeView.InvokeRequired)
                            {
                                tn.TreeView.Invoke(new addNode(AddNode), new object[] {treeNode, node});
                                //if (node.Tag.ToString() == "table")
                                //{
                                //    tn.TreeView.Invoke(new addNode(AddNode), new object[] {node, FieldsNode});
                                //}
                            } else
                            {
                                treeNode.Nodes.Add(node);
                                //if (node.Tag.ToString() == "table")
                                //{
                                //    node.Nodes.Add(FieldsNode);
                                //}
                            }
                            //if (node.Tag.ToString() == "table")
                            //{
                            //    TreeQuery uLibCol = new TreeQuery(FieldsNode, connexion);
                            //    uLibCol.Start("SELECT cname FROM col where tname = '" + tablename + "'");
                            //    DisplayQueryData(connexion, "SELECT cname FROM col where tname = '" + tablename + "'", node);
                            //}
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
