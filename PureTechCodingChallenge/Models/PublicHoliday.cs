using System;
using System.Collections.Generic;
using System.Text;
using WorkDayCounter.Helpers;

namespace WorkDayCounter.Models
{
    public class PublicHoliday
    {
        int mDay;
        int mDayOccurance;
        DayOfWeek mDayOfWeek;
        int mMonth;
        bool mIsSkippedOnWeekend;
        bool mUsesSpecificDate;

        //Constructor for a public holiday that always occurs on the same day that may or may not be skipped
        //when it occurs on a weekend on a weekend
        public PublicHoliday(int day, int month, bool isSkippedOnWeekend)
        {
            mDay = day;
            mMonth = month;
            mIsSkippedOnWeekend = isSkippedOnWeekend;
            mUsesSpecificDate = true;
        }

        //Constructor for a public holiday that occurs on a certain day on a certain month
        public PublicHoliday(DayOfWeek dayOfWeek, int dayOccurance, int month)
        {
            mDayOccurance = dayOccurance;
            mDayOfWeek = dayOfWeek;
            mMonth = month;
            mUsesSpecificDate = false;
        }

        public List<DateTime> GetListOfHolidaysBetweenDates(DateTime firstDate, DateTime secondDate, IList<DateTime> foundHolidayDates)
        {
            List<DateTime> resultingDates = new List<DateTime>();
            if (DateHelper.DatesHaveDaysInBetween(firstDate, secondDate))
            {
                if (mUsesSpecificDate)
                {
                    if (mIsSkippedOnWeekend)
                    {
                        resultingDates = GetAlwaysSameDayHolidayDates(firstDate, secondDate);
                    }
                    else
                    {
                        resultingDates = GetWeekendExtendedHolidayDates(firstDate, secondDate, foundHolidayDates);
                    }
                }
                else
                {
                    resultingDates = GetDayInMonthHolidayDates(firstDate, secondDate);
                }
            }

            return resultingDates;
        }

        private List<DateTime> GetAlwaysSameDayHolidayDates(DateTime firstDate, DateTime secondDate)
        {
            List<DateTime> resultingDates = new List<DateTime>();

            for (DateTime date = firstDate.AddDays(1); date.Date < secondDate.Date; date = date.AddDays(1))
            {
                if (date.Day.Equals(mDay) && date.Month.Equals(mMonth) && DateHelper.IsWeekday(date))
                {
                    resultingDates.Add(date.Date);
                }
            }

            return resultingDates;
        }

        private List<DateTime> GetWeekendExtendedHolidayDates(DateTime firstDate, DateTime secondDate, IList<DateTime> foundHolidayDates)
        {
            List<DateTime> resultingDates = new List<DateTime>();

            for (DateTime date = firstDate.AddDays(1); date.Date < secondDate.Date; date = date.AddDays(1))
            {
                if (date.Day.Equals(mDay) && date.Month.Equals(mMonth))
                {
                    if (DateHelper.IsWeekday(date))
                    {
                        resultingDates.Add(date.Date);
                    }
                    else if (DateHelper.GetNextWeekday(date.Date, foundHolidayDates) < secondDate)
                    {
                        resultingDates.Add(DateHelper.GetNextWeekday(date.Date, foundHolidayDates));
                    }        
                }
            }

            return resultingDates;
        }

        private List<DateTime> GetDayInMonthHolidayDates(DateTime firstDate, DateTime secondDate)
        {
            List<DateTime> resultingDates = new List<DateTime>();

            for (DateTime date = firstDate.AddDays(1).Date; date < secondDate.Date; date = date.AddDays(1))
            {
                if (date.Equals(DateHelper.GetDateOfDayInMonth(mDayOfWeek, mDayOccurance, mMonth, date.Year)))
                {
                    resultingDates.Add(date);
                }
            }

            return resultingDates;
        }
    }
}
