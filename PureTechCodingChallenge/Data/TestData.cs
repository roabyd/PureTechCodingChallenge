using System;
using System.Collections.Generic;
using System.Text;
using WorkDayCounter.Models;

namespace PureTechCodingChallenge.Data
{
    public static class TestData
    {
        public static DateTime lastDayOfThisYear = new DateTime(DateTime.Now.Year, 12, 31);

        public static List<DateTime> sampleHolidays = new List<DateTime>
        {
            new DateTime(2021, 12, 25),
            new DateTime(2021, 12, 26),
            new DateTime(2021, 4, 2),
            new DateTime(2021, 4, 5)
        };

        public static List<PublicHoliday> sampleHolidaysExtended = new List<PublicHoliday>
        {
            new PublicHoliday(25, 12, false),
            new PublicHoliday(26, 12, false),
            new PublicHoliday(25, 4, true),
            new PublicHoliday(DayOfWeek.Monday, 2, 6),
            new PublicHoliday(DayOfWeek.Monday, 1, 8)
        };
    }
}
