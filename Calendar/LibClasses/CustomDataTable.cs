using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses
{
    public class CustomDataTable
    {
        public CellPoint DateToDayPosition { get; set; }


        public DataTable DateTable { get; private set; }
        public DataTable DateTableEvent { get; private set; } // -1, 1 и 0

        public DataTable DateEventTable { get; private set; } 

        private DataTable TextTable { get; set; }
        public DataTable CopyTextTable{ get; private set; }

        public bool isVisibleHeader { get; set; }

        public CustomDataTable(DataTable DateTable, DataTable EventTable = null) 
        { 
            this.DateTable = new DataTable();
            this.DateTableEvent = new DataTable();
            this.TextTable = new DataTable();
            this.CopyTextTable = new DataTable();
            this.isVisibleHeader = true;
        }
        public CustomDataTable(DataTable DateTable, bool IsVisibleHeader)
        {
            this.DateTable = DateTable;
            this.isVisibleHeader = IsVisibleHeader;
        }
        public CustomDataTable(DataTable DateTable, bool IsVisibleHeader, DataTable EvenTable)
        {
            this.DateTable = DateTable;
            this.isVisibleHeader = IsVisibleHeader;
            this.CopyTextTable = EvenTable;
        }


        public DataTable GetSearchDataInDateTable(DataTable dataTable,string query)
        {
            CopyTextTable.Clear();




            return CopyTextTable;
        }


        public CellPoint SearchValueOfTable(object value)
        {
            for(int row = 0; row  < this.DateTable.Rows.Count; row++)
            {
                for(int col = 0; col < this.DateTable.Columns.Count; col++)
                {
                    if (this.DateTable.Rows[row][col].ToString() == value.ToString())
                        return new CellPoint(row, col);
                }
            }


            return null;
        }

    }
}
