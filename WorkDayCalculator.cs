using System;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (dayCount < 0)
                throw new ArgumentException("Incorrect count of days");
            DateTime expectedEndDate = startDate.AddDays(dayCount-1); // Start date is the first day of the period.
            if (weekEnds == null || weekEnds.Length == 0)            
                return expectedEndDate;           
            foreach(WeekEnd weekEnd in weekEnds)
            {
                if(!startDate.IsTheSameKind(weekEnd.StartDate, weekEnd.EndDate))
                    throw new ArgumentException("Input dates have different kind");
                bool isIntersectWeekEnd = weekEnd.StartDate.IsInRange(startDate, expectedEndDate);
                if (isIntersectWeekEnd)
                {
                    int weekEndDays = LengthOfTimeInterval(weekEnd.StartDate, weekEnd.EndDate);
                    expectedEndDate = expectedEndDate.AddDays(weekEndDays);
                }
                else if (weekEnd.StartDate <= startDate && weekEnd.EndDate >= startDate)
                {
                    startDate = weekEnd.EndDate;
                    expectedEndDate = startDate.AddDays(dayCount - 1);
                }
                else break;
            }
            return expectedEndDate;         
        }        
        private int LengthOfTimeInterval(DateTime startDate, DateTime endDate)
        {
            return endDate.Subtract(startDate).Days+1; // length of [t1;t2] interval
        }
    }
    public static class DateTimeExtension
    {
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate;
        }
        public static bool IsTheSameKind(this DateTime dateToCheck, params DateTime[] dateTimes)
        {
            foreach(DateTime dt in dateTimes)
            {
                if (dateToCheck.Kind != dt.Kind)
                    return false;
            } 
            return true;
        }
    }
}
