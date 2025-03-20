using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Calendare.LibClasses;
using Calendare.LibClasses.CustomDateTime;
using System.Windows.Forms;

namespace Calendare.LibClasses.DataBase
{
    public class SQLEventItem
    {
        public enum DataOperation
        {
            Insert,
            Update,
            Delete
        }

        private EventItem eventItem;
        private DataOperation operationMode;
        private SQLiteCommand command;


        public SQLEventItem(EventItem eventItem, DataOperation operation)
        {
            this.eventItem = eventItem;
            this.operationMode = operation;

        }

        public void Execute()
        {
            command = new SQLiteCommand();
            command.CommandText = GetQueryOperation(operationMode);
            ParametersInCommandSet();
            SqlController.CommandExecute(command);
        }


        private string GetQueryOperation(DataOperation operation)
        {
            switch (operation)
            {
                case DataOperation.Insert:
                    {
                        return $"INSERT INTO {Config.Settings.DataBaseEventTableName} ({GetParameterNames()}) VALUES ({GetParameterNames('@')});";
                    }
                case DataOperation.Update:
                    {
                        return $"UPDATE {Config.Settings.DataBaseEventTableName} SET {GetUpdateSetClause()} WHERE \"Id\" = @Id;";
                    }
                case DataOperation.Delete:
                    {
                        return $"DELETE FROM {Config.Settings.DataBaseEventTableName} WHERE \"Id\" = @Id;";
                    }
            }

            return string.Empty;
        }

        private void ParametersInCommandSet()
        {
            object[,] objects = eventItem.ToArray(EventItem.ArrayToElement.sql);
            int len = objects.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                command.Parameters.AddWithValue($"@{objects[i, 0]}", objects[i, 1]);
            }
        }


        private string GetParameterNames(char add = ' ')
        {
            StringBuilder sb = new StringBuilder();
            object[,] objects = eventItem.ToArray(EventItem.ArrayToElement.sql);
            int len = objects.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                sb.Append(add.ToString() + objects[i, 0]);
                if (i < len - 1) sb.Append(", ");
            }

            return sb.ToString();
        }

        private string GetUpdateSetClause()
        {
            StringBuilder sb = new StringBuilder();
            object[,] objects = eventItem.ToArray(EventItem.ArrayToElement.sql);
            int len = objects.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                sb.Append($"\"{objects[i, 0]}\" = @{objects[i, 0]}");
                if (i < len - 1) sb.Append(", ");
            }

            return sb.ToString();
        }


        public static int Max()
        {
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = "Select MAX(id) from " + Config.Settings.DataBaseEventTableName + " limit 1;";

            using (SQLiteDataReader reader = SqlController.ReaderExecute(command))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    }
                }
            }


            return 0;
        }


        public static List<EventItem> EventItemList(string where = "")
        {
            List<EventItem> eventItems = new List<EventItem>();

            string query = $"SELECT * FROM {Config.Settings.DataBaseEventTableName}";
            query += string.IsNullOrEmpty(where) == true ? " ;" : " " + where + ";";
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = query;

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
                        Calendare.LibClasses.CustomDateTime.CustomDateTime dateTime = new CustomDateTime.CustomDateTime(date, '.');
                        bool isAllert = reader.GetInt32(6) == 1 ? true : false;

                        EventItem item = new EventItem(id, name, dateTime, description, isAllert);
                        eventItems.Add(item);
                    }
                }


                return eventItems;
            }

        }
    }
}
