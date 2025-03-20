using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses.CustomDateTime
{
    /// <summary>
    /// Представляет собой класс даты в формате DD.MM.YYYY
    /// </summary>
    public partial class CustomDateTime
    {
        public enum DateParam
        {
            None = 0,
            Day = 1,
            Year = 2,
            Month = 3
        }

        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int OffsetStart { get { return OffsetCalculation(); } }
        public bool IsLeapYear
        {
            get
            {
                if (Year % 400 == 0) return true;
                else if (Year % 100 == 0) return false;
                else if (Year % 4 == 0) return true;

                return false;
            }
        }


        private const int minYear = 1852;
        private const int yearStep = 11;


        private CustomDateTime()
        {
            this.Year = minYear;
            this.Month = 1;
            this.Day = 1;
        }
        public CustomDateTime(string data, char sep = '.') : this()
        {
            string[] newData = data.Split(sep);

            Year = Math.Max(minYear, int.Parse(newData[2]));
            Month = CS_Math.Clamp(int.Parse(newData[1]), 0, 11);
            Day = CS_Math.Clamp(int.Parse(newData[0]), 1, DayInMonth(Month, Year));
        }
        private CustomDateTime(CustomDateTime customDateTime)
        {
            this.Month = customDateTime.Month;
            this.Day = customDateTime.Day;
            this.Year = customDateTime.Year;
        }


        public string ToString(string format = null, char joinSep = '.')
        {
            format = format ?? "d.m.y";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < format.Split('.').Length; i++)
            {
                switch (format.Split('.')[i])
                {
                    case "d": case "D": { sb.Append(Day.ToString()); break; }
                    case "m": case "M": { sb.Append(Month < 10 ? $"0{Month}" : Month.ToString()); break; }
                    case "y": case "Y": { sb.Append(Year.ToString()); break; }
                    case "mm":
                    case "MM":
                        {
                            sb.Append(MonthFullNames[Month - 1]); break;
                        }
                }

                if (i < format.Length - 1) sb.Append(joinSep);
            }

            return sb.ToString();
        }

        public void AddToDate(DateParam param, int value)
        {
            switch (param)
            {
                case DateParam.Year:
                    {
                        Year = Math.Max(1852, Year + yearStep * value);
                        break;
                    }

                case DateParam.Month:
                    {
                        Month += 1 * value;

                        if (Month <= 0)
                        {
                            Math.Max(1852, Year - 1);
                            Year--;
                            Month = 12;
                        }
                        else if (Month > 12)
                        {
                            Year++;
                            Month = 1;
                        }

                        break;
                    }
            }
        }

        public void DataChange(DateParam param, int value)
        {
            switch (param)
            {
                case DateParam.Day:
                    { Day = CS_Math.Clamp(value, 1, DayInMonth(Month, Year)); break; }
                case DateParam.Month:
                    { Month = CS_Math.Clamp(value, 0, 12); break; }
                case DateParam.Year:
                    { Year = Math.Max(1852, value); break; }

            }
        }

        private int OffsetCalculation()
        {
            string nameStart = GetDayOfWeekName(1, Month, Year);

            for (int i = 0; i < WeekFullNames.Length; i++)
                if (nameStart == WeekFullNames[i]) return i;

            return 0;
        }
    }
}
