using Calendare.LibClasses;
using Calendare.LibClasses.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.Modules.EventCreate
{
    internal class EventCreateModel
    {
        public enum Mode
        {
            Create,
            Edit,
            Delete
        }

        Mode mode;

        public EventItem item;

        public EventCreateModel(Mode mode)
        {
            this.mode = mode;
        }

        public void Execute()
        {
            switch (mode)
            {
                case Mode.Create: Create(); break;
                case Mode.Edit: Update(); break;
            }
        }


        public void Create()
        {
            SQLEventItem sql = new SQLEventItem(item, SQLEventItem.DataOperation.Insert);
            sql.Execute();
        }
        public void Update()
        {
            SQLEventItem sql = new SQLEventItem(item, SQLEventItem.DataOperation.Update);
            sql.Execute();
        }


    }
}
