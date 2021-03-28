# PureTechCodingChallenge
A small project to display my coding style for the PureTech DesignCrowd Technical Challenge.

The main class in the project is the BusinessDaysCounter.cs which contains 3 methods:
- WeekdaysBetweenTwoDates
- BusinessDaysBetweenTwoDates (2 overloads)

These methods are used to calculate the number of weekdays between two dates (non-inclusive), allowing public holidays to be 
accounted for and/or defined using the DateHelper.cs helper class and the PublicHoliday.cs model.

To assist with the accuracy of this solution I have also included an NUnit Test project to test the functionality.

# WeekdaysBetweenTwoDates
This function will first check to see if there is a gap between the dates, and once this is confirmed, the number of weekdays
between the two dates is calculated. This is achieved by iterating through each day in between the 2 supplied dates and checking 
if each day is a weekday or not. If a weekday is found, the count is increased and returned to the user once all of the dates
have been checked.

# BusinessDaysBetweenTwoDates (First overload taking a list of DateTime public holiday dates)


# BusinessDaysBetweenTwoDates (Second overload taking a list of PublicHoliday objects)
