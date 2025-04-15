using Calendare.LibClasses.CustomDateTime;
using Calendare.LibClasses;
using Calendare.LibClasses.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendare.Modules.Main
{
    public class MainFormModel
    {
        


        public void ExportDataBase()
        {

            string query = "Select * from " + Config.Settings.DataBaseEventTableName + ";";
            SQLiteCommand command = new SQLiteCommand(query);
            XmlWriter xmlFile = new XmlWriter();

            using (SQLiteDataReader reader = SqlController.ReaderExecute(command))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        string date = $"{reader.GetInt32(3)}.{reader.GetInt32(4)}.{reader.GetInt32(5)}";
                        Calendare.LibClasses.CustomDateTime.CustomDateTime dateTime = new Calendare.LibClasses.CustomDateTime.CustomDateTime(date, '.');
                        bool isAllert = reader.GetInt32(6) == 1 ? true : false;

                        EventItem item = new EventItem(id, name, dateTime, description, isAllert);

                        xmlFile.AddNewStruct(item);


                    }
                }
            }

            xmlFile.Save();
            xmlFile.ShowExplorer();
        }

        public void ImportDataBase(string Path) 
        {
            XmlReader xmlReader = new XmlReader(Path);

            List<EventItem> items = xmlReader.GetEventCollections();

            foreach (EventItem item in items)
            {
                item.NewID();
                SQLEventItem sqlItem = new SQLEventItem(item, SQLEventItem.DataOperation.Insert);
                sqlItem.Execute();
            }
        }


        public void CloseApp()
        {
            Application.Exit();
        }
    }
}
