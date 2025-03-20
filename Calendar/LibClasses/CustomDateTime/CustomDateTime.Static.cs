using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses.CustomDateTime
{
    /// <summary>
    /// Статический класс CustomDateTime
    /// </summary>
    public partial class CustomDateTime
    {
        public static readonly string[] WeekFullNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        public static readonly string[] WeekShortNames = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
        public static readonly string[] MonthFullNames = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };



        public static int DayInMonth(int month, int year)
        {
            month -= 1;
            int[] daysInWeek = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (month == 1 && IsLerpYear(year))
                return daysInWeek[month] + 1;

            return daysInWeek[month];
        }

        public static bool IsLerpYear(int year)
        {
            if (year % 400 == 0) return true;
            else if (year % 100 == 0) return false;
            else if (year % 4 == 0) return true;

            return false;
        }

        public static string Now()
        {
            return System.DateTime.Now.ToString("dd.MM.yyyy");
        }
        public static string Now(string format = null, char joinSep = '.')
        {
            CustomDateTime time = new CustomDateTime(Now(), '.');
            return time.ToString(format, ' ');

        }

        public static string GetDayOfWeekName(int day, int month, int year)
        {
            string[] NameOfWeek = { "Суббота", "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

            // Формула Зеллера
            // h = (q + ⌊(13(m + 1)) / 5⌋ + K + ⌊K / 4⌋ + ⌊J / 4⌋ - 2J) mod 7

            if (month < 3)
            {
                month += 12;
                year -= 1;
            }

            int q = day;
            int m = month;
            int k = year % 100;
            int j = year / 100;
            /*
             * q — день месяца.
             * m — номер месяца (с учетом преобразования для января и февраля).
             * K — последние две цифры года.
             * J — первые две цифры года (век).
             * Формула вычисляет значение h, которое соответствует дню недели (0 = суббота, 1 = воскресенье, ..., 6 = пятница).
             */

            // Формула Зеллера, выч
            int h = (day + (13 * (m + 1)) / 5 + k + (k / 4) + (j / 4) - 2 * j) % 7;


            return NameOfWeek[h];
        }

    }
}
