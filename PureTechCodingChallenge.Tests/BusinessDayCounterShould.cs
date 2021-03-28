using NUnit.Framework;
using System;
using System.Collections.Generic;
using WorkDayCounter.Models;

namespace WorkDayCounter.Tests
{
    [TestFixture]
    class BusinessDayCounterShould
    {
        private BusinessDaysCounter mSut;
        private List<DateTime> mSampleHolidays;
        private List<PublicHoliday> mHolidays;

        [SetUp]
        public void Setup()
        {
            mSut = new BusinessDaysCounter();
        }

        [Test]
        [TestCase("2021-3-27", "2021-3-27", ExpectedResult = 0)]
        [TestCase("2021-3-27", "2021-3-28", ExpectedResult = 0)]
        [TestCase("2021-3-27", "2021-3-26", ExpectedResult = 0)]
        [TestCase("2021-3-27", "2021-3-1", ExpectedResult = 0)]
        public int ReturnZeroDays(DateTime firstDate, DateTime secondDate)
        {
            return mSut.WeekdaysBetweenTwoDates(firstDate, secondDate);
        }

        [Test]
        [TestCase("2013-10-7", "2013-10-9", ExpectedResult = 1)]
        [TestCase("2013-10-5", "2013-10-14", ExpectedResult = 5)]
        [TestCase("2013-10-7", "2014-1-1", ExpectedResult = 61)]
        [TestCase("2013-10-7", "2013-10-5", ExpectedResult = 0)]
        public int ReturnNumOfWeekdays(DateTime firstDate, DateTime secondDate)
        {
            return mSut.WeekdaysBetweenTwoDates(firstDate, secondDate);
        }

        [Test]
        [TestCase("2013-10-7", "2013-10-9", ExpectedResult = 1)]
        [TestCase("2013-12-24", "2013-12-27", ExpectedResult = 0)]
        [TestCase("2013-10-7", "2014-1-1", ExpectedResult = 59)]
        public int ReturnNumOfBusinessDays(DateTime firstDate, DateTime secondDate)
        {
            mSampleHolidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            return mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mSampleHolidays);
        }

        [Test]
        public void CheckNumOfBusinessDaysDifference()
        {
            mSampleHolidays = new List<DateTime>
            {
                new DateTime(2021, 1, 1),
                new DateTime(2022, 1, 1)
            };
            Assert.That(mSut.BusinessDaysBetweenTwoDates(new DateTime(2021, 12, 1), new DateTime(2022, 2, 1), mSampleHolidays),
                Is.EqualTo(mSut.WeekdaysBetweenTwoDates(new DateTime(2021, 12, 1), new DateTime(2022, 2, 1))),
                "Number of days should be the same when the holiday falls on a weekend");

            Assert.That(mSut.BusinessDaysBetweenTwoDates(new DateTime(2020, 12, 1), new DateTime(2021, 2, 1), mSampleHolidays),
                Is.Not.EqualTo(mSut.WeekdaysBetweenTwoDates(new DateTime(2020, 12, 1), new DateTime(2021, 2, 1))),
                "Number of days shouldn't be the same when the holiday falls on a weekday");
        }

        [Test]
        [TestCase("2020-3-1", "2021-5-1", Reason = "Days should be the same when public holdays occur on weekends")]
        public void CheckNumOfBusinessDaysEquality(DateTime firstDate, DateTime secondDate)
        {
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(25, 4, true)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays),
                Is.EqualTo(mSut.WeekdaysBetweenTwoDates(firstDate, secondDate)));
        }

        [Test]
        [TestCase("2020-3-1", "2021-5-1", Reason = "Days shouldn't be the same when public holdays occur on weekends")]
        public void CheckNumOfBusinessDaysInequality(DateTime firstDate, DateTime secondDate)
        {
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(25, 4, false)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays),
                Is.Not.EqualTo(mSut.WeekdaysBetweenTwoDates(firstDate, secondDate)));
        }

        [Test]
        [TestCase("2021-12-1", "2022-2-1")]
        public void ReturnNumOfBusinessDaysWithWeekendExtendedHolidays(DateTime firstDate, DateTime secondDate)
        {
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(1, 1, false)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays),
                Is.Not.EqualTo(mSut.WeekdaysBetweenTwoDates(firstDate, secondDate)),
                "Days shouldn't be the same when public holdays occur on weekends");

            mSampleHolidays = new List<DateTime>
            {
                new DateTime(2020, 1, 1),
                new DateTime(2021, 1, 1),
                new DateTime(2022, 1, 1),
                new DateTime(2022, 3, 5)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate.AddYears(-2), secondDate.AddYears(-1), mHolidays),
                Is.EqualTo(mSut.BusinessDaysBetweenTwoDates(firstDate.AddYears(-2), secondDate.AddYears(-1), mSampleHolidays)),
                "Days should be the same when public holdays occur on weekdays");

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays),
                Is.Not.EqualTo(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mSampleHolidays)),
                "Days shouldn't be the same when public holdays occur on a weekend");
        }

        [Test]
        [TestCase("2022-2-1", "2022-3-7")]
        public void CheckNumOfBusinessDaysWithMondayHoliday(DateTime firstDate, DateTime secondDate)
        {
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(5, 3, false)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays),
                Is.EqualTo(mSut.WeekdaysBetweenTwoDates(firstDate, secondDate)),
                "Days should be the same when public holdays occur on a weekend and the second date is the following Monday");

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate.AddDays(1), mHolidays),
                Is.Not.EqualTo(mSut.WeekdaysBetweenTwoDates(firstDate, secondDate.AddDays(1))),
                "Days shouldn't be the same when public holdays occur on a weekend and the second date is the following Tuesday");
        }

        [Test]
        public void ReturnNumOfBusinessDaysWithDyInMonthHolidays()
        {
            DateTime firstJan2010 = new DateTime(2010, 1, 1);
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(DayOfWeek.Sunday, 1, 1)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstJan2010, firstJan2010.AddYears(10), mHolidays),
                Is.EqualTo(mSut.WeekdaysBetweenTwoDates(firstJan2010, firstJan2010.AddYears(10))),
                "Days should be the same when public holdays occur on weekends");

            DateTime firstJan2020 = new DateTime(2020, 1, 1);
            mHolidays = new List<PublicHoliday>
            {
                new PublicHoliday(DayOfWeek.Wednesday, 2, 2)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(new DateTime(2021, 1, 31), new DateTime(2021, 3, 1), mHolidays), Is.EqualTo(19));

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstJan2020, firstJan2020.AddYears(2), mHolidays), 
                Is.EqualTo(mSut.WeekdaysBetweenTwoDates(firstJan2020, firstJan2020.AddYears(2)) - 2),
                "There should be 2 less days when 2 public holidays happen over the period");
        }

        [Test]
        [TestCase("2021-5-31", "2021-7-1")]
        public void CheckNumOfDaysWithDifferentHolidayTypes(DateTime firstDate, DateTime secondDate)
        {
            mHolidays = new List<PublicHoliday>
            {                
                new PublicHoliday(5, 6, false),
                new PublicHoliday(DayOfWeek.Tuesday, 1, 6),
                new PublicHoliday(12, 6, true)
            };

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays), Is.EqualTo(20),
                "Only 2 days from the list will result in weekday holidays");

            mHolidays.RemoveAt(2);
            mHolidays.Add(new PublicHoliday(15, 6, true));

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays), Is.EqualTo(19),
                "Three days should be public holidays for this month");

            mHolidays.RemoveAt(2);
            mHolidays.Add(new PublicHoliday(7, 6, true));

            Assert.That(mSut.BusinessDaysBetweenTwoDates(firstDate, secondDate, mHolidays), Is.EqualTo(20),
                "Only 2 days should be public holidays if 2 of the arguements fall on the same day");
        }
    }
}
