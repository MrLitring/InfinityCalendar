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
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace Calendare.Modules.Event
{
    public class EventModel
    {
        public CellPoint cellPoint;
        public CustomDateTime dateTime { get; set; }

        public List<EventItem> events { get; private set; }
        public List<EventItem> dayEvents { get; private set; }

        public EventModel()
        {
            events = new List<EventItem>();
            dayEvents = new List<EventItem>();
        }

        public void DataChange(CustomDateTime time)
        {
            dateTime = time;
            events = SQLEventItem.EventItemList($"month = {time.Month} and year = {time.Year}");
            dayEvents = SQLEventItem.EventItemList($"day = {time.Day} and month = {time.Month} and year = {time.Year}");
        }
        public DataTable GetEventTable(DataTable dataTable)
        {
            List<EventItem> listEvents = SQLEventItem.EventItemList($"month = {dateTime.Month} and year = {dateTime.Year}");
            DataTable eventTable = dataTable.Copy();

            for (int row = 0; row < eventTable.Rows.Count; row++)
            {
                for (int col = 0; col < eventTable.Columns.Count; col++)
                {
                    if (dataTable.Rows[row][col].ToString() != "" && int.TryParse(dataTable.Rows[row][col].ToString(), out _) == true)
                        eventTable.Rows[row][col] = isFirstDate(listEvents, (int)dataTable.Rows[row][col]) ? 1 : 0;
                    else
                        eventTable.Rows[row][col] = 0;
                }
            }

            return eventTable;
        }

        public DataTable GetDayDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("наименование", typeof(object));
            dt.Columns.Add("Описание", typeof(object));

            foreach (EventItem item in dayEvents)
            {
                dt.Rows.Add(new string[] { item.Name, item.Description });
            }

            return dt;
        }

        private bool isFirstDate(List<EventItem> events, int day)
        {
            foreach (EventItem item in events)
            {
                if (item.dateTime.Day == day) return true;
            }
            return false;
        }

    }
}
