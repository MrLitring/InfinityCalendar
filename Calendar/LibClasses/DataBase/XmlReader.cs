using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using Calendare.LibClasses.CustomDateTime;

namespace Calendare.LibClasses.DataBase
{
    /// <summary>
    /// Класс XmlWriter предназначен для для чтения XML-документа.
    /// </summary>
    internal class XmlReader
    {
        private string FilePath;
        System.Xml.XmlDocument xmlDoc;

        public XmlReader(string FilePath) 
        {
            xmlDoc = new System.Xml.XmlDocument();
            StringBuilder sb = new StringBuilder();

            sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            sb.Append("\\" + FilePath);

            if (!FilePath.EndsWith(".xml"))
                sb.Append(".xml");

            this.FilePath = sb.ToString();
        }

        public List<EventItem> GetEventCollections()
        {
            List<EventItem> events = new List<EventItem>();

            if(!ExistXmlDoc(FilePath)) return events;
            
            xmlDoc.Load(FilePath);
            XmlNodeList eventNodes = xmlDoc.SelectNodes("/EventItems/EventItem");

            foreach (XmlNode eventNode in eventNodes)
            {
                int id = Convert.ToInt32(eventNode["id"].InnerText);
                string name = eventNode["name"].InnerText;
                string description = eventNode["description"].InnerText;
                string date = eventNode["date"].InnerText;
                bool isAllert = (int.Parse(eventNode["isAllert"].InnerText) == 1) ? true : false;

                EventItem item = new EventItem(id, name, new CustomDateTime.CustomDateTime(date, '.'), description, isAllert );

                events.Add(item);
            }

            return events;
        }

        private bool ExistXmlDoc(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}
