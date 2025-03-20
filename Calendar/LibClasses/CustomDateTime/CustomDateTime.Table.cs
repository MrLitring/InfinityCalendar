using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses.CustomDateTime
{
    /// <summary>
    /// Класс работы с таблицами для класса CustomDateTime
    /// </summary>
    partial class CustomDateTime
    {

        public DataTable GetDataTable(DateParam param)
        {
            DataTable dt = new DataTable();
            int from = 1;
            int to = 0;

            switch (param)
            {
                case DateParam.Day:
                    {
                        dt.Columns.AddRange(NewDataColumns(WeekShortNames));
                        to = DayInMonth(Month, Year);
                        break;
                    }
                case DateParam.Month:
                    {
                        dt.Columns.AddRange(NewDataColumns(new string[] { "Month1", "Month2", "Month3", "Month4" }));
                        to = MonthFullNames.Length - 1;
                        break;
                    }
                case DateParam.Year:
                    {
                        dt.Columns.AddRange(NewDataColumns(new string[] { "Year1", "Year2", "Year3", "Year4" }));
                        if (Year - yearStep / 2 <= minYear)
                        { 
                            from = minYear;
                            to = minYear + yearStep;
                        }
                        else
                        {
                            from = Year - yearStep / 2;
                            to = Year + yearStep / 2 + 1;
                        }
                        
                        break;
                    }
            }

            int localOffset = (param == DateParam.Day) ? OffsetStart : 0;

            do
            {
                if (param == DateParam.Month)
                    AddRow(dt, from - 1, to + 1, MonthFullNames);
                else
                {
                    AddRow(dt, from, to, localOffset);
                }

                from += dt.Columns.Count - localOffset;
                localOffset = 0;

            } while (from <= to);

            return dt;
        }



        private DataColumn NewDataColumn(string nameValue)
        {
            DataColumn dataColumn = new DataColumn();

            dataColumn.ColumnName = nameValue;
            dataColumn.DefaultValue = null;
            dataColumn.DataType = typeof(object);

            return dataColumn;
        }
        private DataColumn[] NewDataColumns(string[] nameValue)
        {
            DataColumn[] dataColumns = new DataColumn[nameValue.Length];

            for (int i = 0; i < nameValue.Length; i++)
            {
                dataColumns[i] = NewDataColumn(nameValue[i]);
            }

            return dataColumns;
        }


        private void AddRow(DataTable dataTable, int from, int to, int offset)
        {
            dataTable.Rows.Add();
            int row = dataTable.Rows.Count - 1;

            for (int i = offset; i < dataTable.Columns.Count && from <= to; i++)
            {
                dataTable.Rows[row][i] = from;
                from++;
            }
        }        

        private void AddRow(DataTable dataTable, int from, int to, object[] values)
        {
            dataTable.Rows.Add();
            int row = dataTable.Rows.Count - 1;

            for (int i = 0; i < dataTable.Columns.Count && from < to; i++)
            {
                dataTable.Rows[row][i] = values[from];
                from++;
            }

        }
    }
}
