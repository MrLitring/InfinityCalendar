using System;
using System.Drawing;
using System.IO;
using System.Text;
using Calendare.LibClasses.CustomDateTime;

namespace Calendare.Config
{
    public static partial class Settings
    {
        public const string DataBaseEventTableName = "TableOfEvents";


        private static string DirectoryPath;

        private const string DirectoryName = "InfinityCalendar";
        private const string DataBaseName = "db";

        private const string DataBaseImport = "InfinityCalendareEvents";
               

        
        public static void Initilization()
        {
            DirectoryPath = CreateDirectoryPath();
            NullOrCreateDirectory();

        }


        public static string DataBaseFullPath
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(DirectoryPath);
                sb.Append("\\" + DataBaseName + ".db");

                return sb.ToString();
            }
        }

        public static string GetImportPathOfXmlDocument
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(DirectoryPath);
                sb.Append("\\" + DataBaseImport + "_" + CustomDateTime.Now("d.m.y", '_') + ".xml" );

                return sb.ToString();
            }
        }

        
        private static string CreateDirectoryPath()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DirectoryName);

            return sb.ToString();
        }

        private static void NullOrCreateDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
        }

    }
}
