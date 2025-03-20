using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.LibClasses.ViewHelper
{
    public static class FormViewHelper
    {
        public static void ShowFormInPanel(Control control, Form form)
        {
            if (control == null || form == null) return;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;


            control.Controls.Add(form);

            form.Dock = DockStyle.Fill;
            form.Show();
        }

    }
}
