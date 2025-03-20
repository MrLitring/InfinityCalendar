using System;
using System.Linq;
using System.Windows.Forms;

namespace Calendare.LibClasses.ViewHelper
{
    public static class DataGridViewExtands
    {
        public static void RowResize(this DataGridView dgv, bool isHeaderView)
        {
            int rows = dgv.Rows.Count;
            int height = dgv.ClientSize.Height;

            if (rows <= 0) return;

            if (isHeaderView) rows++;

            dgv.ColumnHeadersVisible = isHeaderView;
            height = height / rows;
            height = height < 0 ? 0 : height; 

            foreach (DataGridViewRow row in dgv.Rows)
                row.Height = height;
            dgv.ColumnHeadersHeight = height;

        }

        public static void Reset(this DataGridView dataGridView)
        {
            if (dataGridView.DataSource != null)
                dataGridView.DataSource = null;

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }

        public static void CellFocus(this DataGridView dgv, CellPoint cellPoint)
        { 
            if(cellPoint.IsNotOutBorder(cellPoint))
                dgv.CurrentCell = (cellPoint != null) ? dgv.Rows[cellPoint.row].Cells[cellPoint.column] : dgv.Rows[0].Cells[0]; 
        }
    }
}
