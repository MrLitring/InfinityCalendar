using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calendare.LibClasses;

namespace Calendare.LibClasses.Interfaces
{
    public interface IFormEvent : IDGV
    {
        void CMSShow(ContextMenuStrip cms);

        ContextMenuStrip GetCMS();
    }
}
