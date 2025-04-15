using Calendare.LibClasses.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibCsTime = Calendare.LibClasses.CustomDateTime;

namespace Calendare.LibClasses
{
    public class EventItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public LibCsTime.CustomDateTime dateTime { get; private set; }
        public string Description { get; private set; }
        public bool IsAllert { get; private set; }


        public EventItem(int id, string name, LibCsTime.CustomDateTime dateTime, string description = null, bool isAlert = false)
        {
            this.Id = id;
            this.Name = name;
            this.dateTime = dateTime;
            this.Description = description;
            this.IsAllert = isAlert;
        }

        public EventItem(EventItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.dateTime = item.dateTime;
            this.Description = item.Description;
            this.IsAllert = item.IsAllert;
        }

        
        public void NewID()
        {
            this.Id = SQLEventItem.Max() + 1;
        }

        public enum ArrayToElement
        {
            xml,
            sql
        }
        public object[,] ToArray(ArrayToElement arrayToItem)
        {
            switch (arrayToItem)
            {
                case ArrayToElement.xml: return ToArrayXml();
                case ArrayToElement.sql: return ToArraySQL();
                default:
                    return null;
            }
        }

        private object[,] ToArraySQL()
        {
            return new object[,]
            {
                    { "Id", Id},
                    { "Name", Name},
                    { "Description",Description},
                    { "Day", dateTime.Day },
                    { "Month", dateTime.Month },
                    { "Year", dateTime.Year },
                    {"IsAllert", IsAllert == true ? 1 : 0}
                };
        }

        private object[,] ToArrayXml()
        {
            return new object[,]
            {
                    { "Id", Id},
                    { "Name", Name},
                    { "Description",Description},
                    { "DateTime", dateTime.ToString() },
                    {"IsAllert", IsAllert == true ? 1 : 0}
                };

        }

        public static EventItem NewEmpty(int id, CustomDateTime.CustomDateTime dateTime)
        {
            return new EventItem(id, "", dateTime, "", false);
        }



    }
}
