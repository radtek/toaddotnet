using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PluginTypes;

namespace DBDiff
{
    public class Class1: IMenuAddOn, IFormAddOn
    {
        private Form parentForm = null;
        #region IMenuAddOn Members

        public void Install(MenuStrip menu)
        {
            PlugUtils.AddMenu(menu, "&Schema", "Compare tables", new EventHandler(SchemaCompareTablesMenuItem_Click));
        }        

        public void EventPlug(PlugEvent e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region event
        
        #endregion

        #region IFormAddOn Members

        public void Install(Form form)
        {
            parentForm = form;
        }

        #endregion
        
        private void SchemaCompareTablesMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.MdiParent = parentForm;            
            form.Show();
        }
    }
}
