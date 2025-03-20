using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Calendare.LibClasses;
using System.IO;
using System.Diagnostics;

namespace Calendare.LibClasses.DataBase
{
    /// <summary>
    /// Класс XmlWriter предназначен для для записи XML-документа.
    /// </summary>
    public class XmlWriter
    {
        private string FilePath;
        System.Xml.XmlDocument xmlDoc;

        public XmlWriter()
        {
            xmlDoc = new System.Xml.XmlDocument();
            FilePath = Config.Settings.GetImportPathOfXmlDocument;

            CreateDocument();

        }

        public void ShowExplorer()
        {
            string directoryPath = Path.GetDirectoryName(this.FilePath);
            Process.Start("explorer.exe", "/select," + FilePath);
        }


        public void AddNewStruct(EventItem item)
        {
            XmlElement eventElemen = xmlDoc.CreateElement("EventItem");
            object[,] obj = item.ToArray(EventItem.ArrayToElement.xml);

            for(int i = 0; i< obj.Length; i++)
            {
                eventElemen.AppendChild(NewElement($"{obj[i,0]}", obj[i, 0].ToString()));
            }

            
            //eventElemen.AppendChild(NewElement("name", item.Name.ToString()));
            //eventElemen.AppendChild(NewElement("description", item.Description.ToString()));
            //eventElemen.AppendChild(NewElement("date", item.dateTime.ToString("d.m.y")));
            //if (item.IsAllert) eventElemen.AppendChild(NewElement("isAllert", "1"));
            //else eventElemen.AppendChild(NewElement("isAllert", "0"));

            xmlDoc.DocumentElement.AppendChild(eventElemen);

        }

        public void Save()
        {
            xmlDoc.Save(this.FilePath);
        }


        private void CreateDocument()
        {
            xmlDoc = new System.Xml.XmlDocument();
            XmlElement root = xmlDoc.CreateElement("EventItems");
            xmlDoc.AppendChild(root);
        }

        private XmlElement NewElement(string name, string value)
        {
            XmlElement xmlElement = xmlDoc.CreateElement(name);
            xmlElement.InnerText = value;
            return xmlElement;
        }

    }
}
