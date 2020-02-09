using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 29))
            };
            
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }        
        [TestMethod]
        public void TestSameStartEndDates()
        {
            DateTime startDate = new DateTime(2012, 2, 1);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2012, 2, 5), new DateTime(2012, 2, 5))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2012, 2, 11)));
        }
        [TestMethod]
        public void TestWeekEndStartBeforeAndEndAfterStartDate()
        {
            DateTime startDate = new DateTime(2015, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2015, 4, 19), new DateTime(2015, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2015, 4, 27)));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNegativeDaysCount()
        {            
            DateTime result = new WorkDayCalculator().Calculate(DateTime.Now, -42, null);            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDifferentKindOfTimeZones()
        {
            DateTime startDate = DateTime.UtcNow;
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(DateTime.Now, DateTime.Now)
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends); // should throw an exception                        
        }
    }
}
