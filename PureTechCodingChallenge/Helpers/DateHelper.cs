using System;
using System.Collections.Generic;
using System.Text;

namespace WorkDayCounter.Helpers
{
    public static class DateHelper
    {
        public static bool DatesHaveDaysInBetween(DateTime firstDate, DateTime secondDate)
        {
            return secondDate.Date > firstDate.AddDays(1).Date;
        }

        public static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static DateTime GetNextWeekday(DateTime date, IList<DateTime> existingHolidays)
        {
            bool avalableDayFound = false;
            int daysAdded = 0;
            while (!avalableDayFound)
            {
                if (IsWeekday(date.AddDays(daysAdded)) && !existingHolidays.Contains(date.AddDays(daysAdded)))
                {
                    avalableDayFound = true;
                }
                else
                {
                    daysAdded++;
                }
            }
            return date.AddDays(daysAdded);
        }

        public static DateTime GetDateOfDayInMonth(DayOfWeek day, int dayOccurance, int month, int year)
        {
            DateTime resultingDate = new DateTime();
            DateTime monthStart = new DateTime(year, month, 1);
            DateTime nextMonthStart = month < 12 ? new DateTime(year, month + 1, 1) : new DateTime(year + 1, 1, 1);
            int daysFound = 0;

            for (DateTime date = monthStart; date < nextMonthStart; date = date.AddDays(1))
            {        
                if (date.DayOfWeek.Equals(day))
                {
                    daysFound++;
                    if (daysFound == dayOccurance)
                    {
                        resultingDate = date;
                    }
                }
            }
            return resultingDate;
        }

        public static int GetBusinessDaysBetweenDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            int numOfBusinessDays = 0;
            for (DateTime date = firstDate.AddDays(1); date.Date < secondDate.Date; date = date.AddDays(1))
            {
                if (publicHolidays == null && DateHelper.IsWeekday(date))
                {
                    numOfBusinessDays++;
                }
                else if (publicHolidays != null &&!publicHolidays.Contains(date.Date) && DateHelper.IsWeekday(date))
                {
                    numOfBusinessDays++;
                }              
            }
            return numOfBusinessDays;
        }
    }
}
