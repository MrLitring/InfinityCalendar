using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.LibClasses.Interfaces
{
    public interface IFormCalendare : IDGV
    {
        void LabelTextUpdate(int index, string str);

        void CellFocus(CellPoint cellPoint);

        ContextMenuStrip GetCMS();

        void CMSShow(ContextMenuStrip cms);

        void CellPaint(CellPoint cellPoint, Color backColor, Color foreColor);
    }
}
