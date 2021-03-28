using PureTechCodingChallenge.Data;
using System;
using System.Collections.Generic;
using WorkDayCounter.Models;

namespace PureTechCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessDaysCounter counter = new BusinessDaysCounter();
            Console.WriteLine("Hello PureTech!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Please find samples below of the functions implemented for this coding challenge");
            Console.WriteLine("");
           
            Console.WriteLine("The number of weekdays between today and the end of the year is:");
            Console.WriteLine(counter.WeekdaysBetweenTwoDates(DateTime.Now, TestData.lastDayOfThisYear));
            Console.WriteLine("");

            Console.WriteLine("The number of weekdays between today and the end of the 2021, excluding the 25th of December,");
            Console.WriteLine("the 26th of December, Good Friday and Easter Monday is:");
            Console.WriteLine(counter.BusinessDaysBetweenTwoDates(DateTime.Now, TestData.lastDayOfThisYear, TestData.sampleHolidays));
            Console.WriteLine("");

            Console.WriteLine("The number of business days between today and the end of the year, excluding Christmas Day,");
            Console.WriteLine("boxing day, Anzac Day, the Queens Birthday and the NSW Bank Holiday is:");
            Console.WriteLine(counter.BusinessDaysBetweenTwoDates(DateTime.Now, TestData.lastDayOfThisYear, TestData.sampleHolidaysExtended));
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("That you for your considering me for the open position of Software Engineer");
            Console.WriteLine("");

            Console.WriteLine("Your sincerely,");
            Console.WriteLine("Robert Dunn");
        }
    }
}
