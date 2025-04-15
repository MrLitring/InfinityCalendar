using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Calendare.LibClasses.DataBase
{
    public static class SqlController
    {
        private static SQLiteConnection connection = null;
        private static string FullDataBasePath
        { get { return Calendare.Config.Settings.DataBaseFullPath; } }


        public static bool IsConnection
        {
            get
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open) return true;
                return false;
            }
        }

        public static bool IsExistDataBase()
        {
            if (File.Exists(FullDataBasePath)) return true;

            return false;
        }

        public static void OpenOrCreateConnection()
        {
            if (!IsExistDataBase())
                CreateDefaultDataBase();

            try
            {
                if (IsConnection == false || connection == null)
                {
                    connection = new SQLiteConnection($"Data Source={FullDataBasePath};Version=3;");
                    connection.Open();
                }
            }
            catch
            (Exception ex)
            {
                Debug.WriteLine("Error opening connection: " + ex.Message);
            }
        }

        public static void CloseConnection()
        {
            connection?.Close();
            connection.Dispose();
        }

        public static void CommandExecute(SQLiteCommand command)
        {
            if (command == null) return;

            command.Connection = connection;

            try { command.ExecuteNonQuery(); }
            catch { MessageBox.Show("Не смог сохранить: " + command.CommandText); }

        }

        public static SQLiteDataReader ReaderExecute(SQLiteCommand command)
        {
            command.Connection = connection;

            try
            {
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }
        }

        public static void CreateDefaultDataBase()
        {
            SQLiteConnection.CreateFile(Config.Settings.DataBaseFullPath);

            using (SQLiteConnection connect = new SQLiteConnection($"Data Source={FullDataBasePath};Version=3;"))
            {
                connect.Open();
                SQLiteCommand command = new SQLiteCommand(CreateDefaultTableDataBsae());
                command.Connection = connect;

                command.ExecuteNonQuery();

                CreateDefaultTableDataBsae();

                connect.Close();
            }
        }

        private static string CreateDefaultTableDataBsae()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"CREATE TABLE [{Config.Settings.DataBaseEventTableName}] (");
            sb.Append("\"Id\" INTEGER NOT NULL PRIMARY KEY, ");
            sb.Append("\"Name\" TEXT, ");
            sb.Append("\"Description\" TEXT, ");
            sb.Append("\"Day\" INTEGER, ");
            sb.Append("\"Month\" INTEGER, ");
            sb.Append("\"Year\" INTEGER,");
            sb.Append("\"IsAllert\" INTEGER");
            sb.Append(");");

            return sb.ToString();
        }
    }
}