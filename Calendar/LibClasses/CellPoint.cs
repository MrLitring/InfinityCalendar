using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses
{
    public class CellPoint
    {
        public int row;
        public int column;

        public CellPoint(int row, int col)
        {
            this.row = row;
            this.column = col;
        }


        public bool IsNotOutBorder(CellPoint cellPoint,int row = - 1, int col = -1)
        {
            return (cellPoint.row > row && cellPoint.column > col) ? true : false;
        }

        public static CellPoint SearchValueOfTable(object value, DataTable dataTable)
        {
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    if (dataTable.Rows[row][col].ToString() == value.ToString())
                        return new CellPoint(row, col);
                }
            }

            return null;
        }
    }
}
