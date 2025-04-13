using Calendare.LibClasses;
using Calendare.LibClasses.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.Modules.EventCreate
{
    internal class EventCreateController
    {
        EventCreateModel model;
        EventCreateForm form;
        EventItem item;

        public EventCreateController(EventCreateModel.Mode mode = EventCreateModel.Mode.Create, EventItem item = null)
        {
            model = new EventCreateModel(mode);
            form = new EventCreateForm();
            this.item = item;

            form.ShowData(item);
            Event();


            form.ShowDialog();
        }

        private void Event()
        {
            form.button2.Click += (s, e) => Execute();
            form.button1.Click += (s, e) => Cancel();
        }



        public void ReadData()
        {
            EventItem newItem = new EventItem(
                item.Id,
                form.textBox2.Text,
                item.dateTime,
                form.textBox3.Text,
                form.checkBox1.Checked
                );
            item = newItem;
        }

        public void Execute()
        {
            ReadData();
            model.item = item;
            model.Execute();

            this.form.Close();
        }


        private void Cancel()
        {
            this.form.Close();
        }

    }
}
