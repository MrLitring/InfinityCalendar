using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.LibClasses.Interfaces
{
    public interface IFormCalendare
    {
        void LabelTextUpdate(int index, string str);

        void DataGridViewUpdate(Calendare.LibClasses.CustomDataTable dataTable);

        void CellFocus(CellPoint cellPoint);

        ContextMenuStrip GetCMS();

        void CMSShow(ContextMenuStrip cms);
    }
}
