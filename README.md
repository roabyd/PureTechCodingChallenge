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
This function calculates the number of business days between two dates, excluding any dates that are passed in publicHolidays 
list. This function utilizes the same iteration functionality to go through the list of possible weekdays, however the count is 
only increased if the date is a weekday, and it does not appear in the list of publicHolidays. 
  
  
# BusinessDaysBetweenTwoDates (Second overload taking a list of PublicHoliday objects) 
This method further extends the functionality of the previous overload by accepting a list of public holiday rules that are  
defined using a custom PublicHoliday class. Public holidays can be defined in three different ways: 
- Holidays that always fall on the same day, regardless of the day of the week 
- Holidays which are always on the same day, except when that falls on a weekend, in which case the holiday is the next Monday 
- Holidays that occur on a certain day on a certain month, eg. the second Monday in June every year 
  
To achieve this, the PublicHoliday class implements 2 constructors, one which takes a day, a month and a Boolean that determines  
if holidays should be skipped if they fall on a weekend, and another that takes a day of the week, a day occurrence value and a  
month. Once the type of holiday has been determined via the constructor parameters, I have created a method that will return a list  
of public holiday dates that would fall between 2 supplied dates. This will also take into account dates that have already been found
in order to make up for 2 public holidays that occur on the weekend (ie Christmas Day and Boxing Day). This list is then passed as 
a parameter into the first overload of this method to return the number of business days.  
  
  
# NUnit Test project 
This project has been created to test the cases supplied in the brief, plus several others to ensure the functionality of the above 
methods always return the expected results, based on the brief and the above descriptions. 


Thank you for reading! 
