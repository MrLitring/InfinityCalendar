using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses.ViewHelper
{
    public static class LabelExtands
    {
        public static void Hoover(this Label label, Color enter, Color leave)
        {
            label.MouseEnter += (s,e) => label.ForeColor = enter;
            label.MouseLeave += (s, e) => label.ForeColor = leave;
        }
    }
}
