using Calendare.Modules.EventCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.LibClasses.ViewHelper
{
    public static class ContexMenuStripExtends
    {

        public static ContextMenuStrip CMSCheckForDesign(ContextMenuStrip contextMenuStrip, bool isValue = false)
        {
            if (!isValue)
            {
                foreach (var item in contextMenuStrip.Items)
                {
                    if (item is ToolStripMenuItem)
                    {
                        if ((item as ToolStripMenuItem).Name == "btnAddEvent") (item as ToolStripMenuItem).Enabled = true;
                        else (item as ToolStripMenuItem).Enabled = false;
                    }
                }
            }
            else
            {
                foreach (ToolStripMenuItem item in contextMenuStrip.Items)
                {
                    item.Enabled = true;
                }
            }

            return contextMenuStrip;
        }

    }
}
