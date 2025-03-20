using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendare.LibClasses.DataBase;
using Calendare.LibClasses.CustomDateTime;
using System.Windows.Forms;
using Calendare.LibClasses;
using Calendare.LibClasses.DataBase;
using System.Data.SQLite;


namespace Calendare.Modules.Event
{
    public class EventModel
    {
        public CustomDateTime dateTime { get; set; }


        public EventModel()
        {
            


        }

        public void Main()
        {
            int count = 0;
            string where = "";
            where = $"where Day = {dateTime.Day} AND Month = {dateTime.Month} AND YEAR = {dateTime.Year}";
            List<EventItem> s = SQLEventItem.EventItemList(where);
            MessageBox.Show(s.Count().ToString());
        }



        //public EventModel()
        //{
        //    SQLiteCommand command = new SQLiteCommand();
        //    command.CommandText = "Select MAX(id) from " + Config.Settings.DataBaseEventTableName;

        //    using (SQLiteDataReader reader = SqlController.ReaderExecute(command))
        //    {
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                int max = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
        //                MessageBox.Show(max.ToString());
        //            }
        //        }
        //    }
        //    //CustomDateTime customDateTime = new CustomDateTime(CustomDateTime.Now(), '.');
            //EventItem item = new EventItem(3, "name", customDateTime, string.Empty, true);
            //SQLEventItem eventInsert = new SQLEventItem(item, SQLEventItem.DataOperation.Insert);
            //eventInsert.Execute();
        //}


        public DataTable GetDataTable(DataTable dataTable)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(object));
            dt.Columns.Add("name", typeof(object));
            dt.Columns.Add("description", typeof(object));

            return dt;
        }


    }
}
