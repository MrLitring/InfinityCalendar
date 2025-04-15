using Calendare.LibClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calendare.LibClasses.CustomDateTime;
using System.Xml.Serialization;

namespace Calendare.Modules.Calendar
{
    internal class CalendarModel
    {
        public enum CalendarModeType
        {
            DayMode = 0,
            MonthMode = 1,
            YearMode = 2
        }
        public CellPoint LastCellPoint = new CellPoint(0, 0);
        public CalendarModeType EditMode { get; private set; }

        public string GetCurrentDate { get { return dateTime.ToString("d.mm.y", ' '); } }
        public string GetNowDate { get { return CustomDateTime.Now("d.mm.y", ' '); } }



        public CustomDateTime dateTime { get; private set; }


        public CalendarModel()
        {
            EditMode = CalendarModeType.DayMode;
            dateTime = new CustomDateTime(CustomDateTime.Now());
        }

        public void SwitchEditMode()
        {
            switch (EditMode)
            {
                case CalendarModeType.DayMode: { EditMode = CalendarModeType.MonthMode; break; }
                case CalendarModeType.MonthMode: { EditMode = CalendarModeType.YearMode; break; }
                case CalendarModeType.YearMode: { EditMode = CalendarModeType.DayMode; break; }
            }

            Console.WriteLine($"EditModeChange Invoke -> {EditMode.ToString()}");
        }

        public DataTable GetDataTable(out bool visibleHeader)
        {
            DataTable dt = new DataTable();

            visibleHeader = false;

            switch (EditMode)
            {
                case CalendarModeType.DayMode:
                    { dt = dateTime.GetDataTable(CustomDateTime.DateParam.Day); visibleHeader = true; break; }
                case CalendarModeType.MonthMode:
                    { dt = dateTime.GetDataTable(CustomDateTime.DateParam.Month); break; }
                case CalendarModeType.YearMode:
                    { dt = dateTime.GetDataTable(CustomDateTime.DateParam.Year); break; }
            }

            return dt;
        }

        private DataColumn NewDataColumn(string nameValue)
        {
            DataColumn dataColumn = new DataColumn();

            dataColumn.ColumnName = nameValue;
            dataColumn.DefaultValue = nameValue;
            dataColumn.DataType = typeof(object);

            return dataColumn;
        }


        public string GetFromEditMode
        {
            get
            {
                switch (EditMode)
                {
                    case CalendarModeType.DayMode:
                        return dateTime.ToString("d.mm.y", ' ');
                    case CalendarModeType.MonthMode:
                        return dateTime.ToString("mm.y", ' ');
                    case CalendarModeType.YearMode:
                        return dateTime.ToString("y", ' ');
                    default:
                        return dateTime.ToString();
                }
            }
        }

        public void IncreaseDate(int value = 1)
        {
            switch (EditMode)
            {
                case CalendarModeType.DayMode:
                    {
                        dateTime.AddToDate(CustomDateTime.DateParam.Month, value); break;
                    }
                case CalendarModeType.MonthMode:
                    {
                        dateTime.AddToDate(CustomDateTime.DateParam.Year, value); break;
                    }
                case CalendarModeType.YearMode:
                    {
                        dateTime.AddToDate(CustomDateTime.DateParam.Year, value); break;
                    }
            }

        }

        public void DataChange(int value)
        {
            switch(EditMode)
            {
                case CalendarModeType.DayMode:
                    {
                        dateTime.DataChange(CustomDateTime.DateParam.Day, value);
                        break;
                    }
                case CalendarModeType.MonthMode:
                    {
                        dateTime.DataChange(CustomDateTime.DateParam.Month, value);
                        break;
                    }
                case CalendarModeType.YearMode:
                    {
                        dateTime.DataChange(CustomDateTime.DateParam.Year, value);
                        break;
                    }
            }
        }


        public object GetValueDataTable(CellPoint cellPosition)
        {
            DataTable dataTable = GetDataTable(out _);
            switch (EditMode)
            {
                case CalendarModeType.DayMode:
                    {
                        return dataTable.Rows[cellPosition.row][cellPosition.column];
                    }
                case CalendarModeType.MonthMode:
                    {
                        int row = (cellPosition.row + 1 ) * dataTable.Columns.Count;
                        int col = (dataTable.Columns.Count - cellPosition.column);
                        int sum = row - col + 1;

                        return sum;
                    }
                case CalendarModeType.YearMode:
                    {
                        return dataTable.Rows[cellPosition.row][cellPosition.column];
                    }
            }


            return 0;
        }

        public void ResetDate()
        {
            dateTime = new CustomDateTime(CustomDateTime.Now("d.m.y", '.'), ' ');
        }

    }
}
