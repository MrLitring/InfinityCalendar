using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendare.LibClasses;

namespace Calendare.LibClasses.Interfaces
{
    public interface IFormEvent
    {
        void DataGridViewUpdate(CustomDataTable dataTable);
    }
}
