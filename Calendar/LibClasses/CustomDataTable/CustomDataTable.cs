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

        public DataTable DateTable { get;  set; }

        public DataTable EventTable { get;  set; } 

        public DataTable ListEventTable { get; set; }

        public bool isVisibleHeader { get; set; }


        

    }
}
