using System;
using System.Data;
using System.Drawing;
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

        public static void DataTableSource(this DataGridView dgv, DataTable dataTable, int offset = 0)
        {
            dgv.Reset();
            int lasCol = dgv.Columns.Count + offset;
            for (int col = 0; col < offset; col++)
                dgv.Columns.Add("adaptive_" + offset, string.Empty);

            for (int col = 0; col < dataTable.Columns.Count; col++)
            {
                string text = dataTable.Columns[col].ColumnName;
                dgv.Columns.Add(text, text);
            }

            if (dataTable.Rows.Count == 0) return;

            int row = 0;
            do
            {
                dgv.Rows.Add();

                for (int col = lasCol; col < dataTable.Columns.Count + lasCol; col++)
                {
                    dgv.Rows[row].Cells[col].Value = dataTable.Rows[row][col - lasCol];
                }
                row++;
            } while (row < dataTable.Rows.Count);

            

        }

        public static void GenerallDesign(this DataGridView dgv, Color backColor, Color foreColor, bool isUserAction = false)
        {

            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if(!isUserAction) column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach(DataGridViewRow row in dgv.Rows)
                foreach(DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = backColor;
                    cell.Style.SelectionBackColor = Config.Settings.BackGroundColorHoover;
                    cell.Style.ForeColor = foreColor;

                    //cell.Style.Font = new Font(cell.Style.Font.OriginalFontName, cell.Style.Font.Size + 4,FontStyle.Bold);
                }


            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;


            if (!isUserAction) return;

            dgv.AllowUserToResizeRows = true;
            dgv.AllowUserToResizeColumns = true;
        }
    }
}
