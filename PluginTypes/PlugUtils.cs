using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PluginTypes
{
    public static class PlugUtils
    {
        public static void AddMenu(MenuStrip menu, string menuItem, string subMenuItem, EventHandler Target)
        {
            // check if the Menu Tools exists if not create it
            #region Variables
            ToolStripMenuItem toolsToolStripMenuItem = null;
            ToolStripMenuItem toolsPrefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ToolStripMenuItem webSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            #endregion

            #region Designer
            // 
            // toolsToolStripMenuItem
            // Search for the entry menu tools
            // 
            foreach (ToolStripItem item in menu.Items)
            {
                // do something with this item
                Console.WriteLine(item.ToString());
                if (item.Text == menuItem)
                    toolsToolStripMenuItem = item as ToolStripMenuItem;
                // enumerate sub-items (if could have them)
                //ToolStripDropDownItem dropItem = item as ToolStripDropDownItem;
                //if (dropItem != null)
                //{
                //WalkItems(dropItem.DropDownItems);
                //}
            }
            // The entry menu Tools has not been found, then create id
            if (toolsToolStripMenuItem == null)
            {
                toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                toolsToolStripMenuItem.Name = menuItem;
                toolsToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
                toolsToolStripMenuItem.Text = menuItem;
                menu.Items.Add(toolsToolStripMenuItem);
            }
            // Add the sub-menu to the Tools menu            
            toolsToolStripMenuItem.DropDownItems.Add(toolsPrefToolStripMenuItem);
            //toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {toolsPrefToolStripMenuItem});
            // 
            // configToolStripMenuItem
            // 
            toolsPrefToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            toolsPrefToolStripMenuItem.Name = subMenuItem;
            toolsPrefToolStripMenuItem.Text = subMenuItem;
            
            // Add event to execute when menu item selected
            toolsPrefToolStripMenuItem.Click += new System.EventHandler(Target);
            #endregion
        }

     
    }
}
