using System;

namespace CSharpTest
{
    public class WeekEnd
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WeekEnd(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
