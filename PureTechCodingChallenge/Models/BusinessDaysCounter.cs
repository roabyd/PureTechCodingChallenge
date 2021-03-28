using System;
using System.Collections.Generic;
using System.Text;
using WorkDayCounter.Helpers;

namespace WorkDayCounter.Models
{
    public class BusinessDaysCounter
    {
        //Funtion to calculate the number of weekdays between 2 dates (non-inclusive)
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            int numOfWeekdays = 0;
            if (DateHelper.DatesHaveDaysInBetween(firstDate, secondDate))
            {
                for (DateTime date = firstDate.AddDays(1); date.Date < secondDate.Date; date = date.AddDays(1))
                {
                    if (DateHelper.IsWeekday(date))
                    {
                        numOfWeekdays++;
                    }
                }
            }

            return numOfWeekdays;
        }

        //Function to calculate the number of weekdays between 2 dates (non-inclusive) that are not in a public holiday list
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            int numOfBusinessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (numOfBusinessDays > 0)
            {
                for (DateTime date = firstDate.AddDays(1); date.Date < secondDate.Date; date = date.AddDays(1))
                {
                    if (publicHolidays.Contains(date) && DateHelper.IsWeekday(date))
                    {
                        numOfBusinessDays--;
                    }
                }
            }

            return numOfBusinessDays;
        }

        //Function to calculate the number of weekdays between 2 dates (non-inclusive) that are not in a public holiday data structure list
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            int numOfBusinessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (numOfBusinessDays > 0)
            {
                List<DateTime> publicHolidayDates = new List<DateTime>();
                foreach (PublicHoliday holiday in publicHolidays)
                {
                    publicHolidayDates.AddRange(holiday.GetListOfHolidaysBetweenDates(firstDate, secondDate));
                }
                numOfBusinessDays = BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidayDates);
            }

            return numOfBusinessDays;
        }
    }
}
