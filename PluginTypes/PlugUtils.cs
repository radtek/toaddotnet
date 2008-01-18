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
                //Console.WriteLine(item.ToString());
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
                //menu.Items.Add(toolsToolStripMenuItem);
                menu.Items.Insert(2, toolsToolStripMenuItem);
            }
            // Add the sub-menu to the Tools menu
            if (toolsToolStripMenuItem.DropDownItems.Find(subMenuItem, true).Length == 0)
            {

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
            }    
            
            #endregion
        }

     
    }
}
