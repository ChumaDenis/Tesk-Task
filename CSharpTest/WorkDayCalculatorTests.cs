using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };
            
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }



        [TestMethod]
        public void TestWeekendBeforeEnds()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 7;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 15), new DateTime(2021, 4, 19)),
                new WeekEnd(new DateTime(2021, 4, 25), new DateTime(2021, 4, 29)),
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 2)));
        }



        [TestMethod]
        public void TestWeekendSuccessively()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 21), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 26), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 4)));
        }

        [TestMethod]
        public void TestWeekendWithPartialCrossing()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 19), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 30)));
        }

        //I had another implementation option in mind, but if I understood correctly
        //from the condition "The array is sorted by the start date, periods do not intersect.".
        //This test is not completely correct.
        //[TestMethod]
        //public void TestWeekendIntersect()
        //{
        //    DateTime startDate = new DateTime(2021, 4, 21);
        //    int count = 5;
        //    WeekEnd[] weekends = new WeekEnd[2]
        //    {
        //        new WeekEnd(new DateTime(2021, 4, 21), new DateTime(2021, 4, 25)),
        //        new WeekEnd(new DateTime(2021, 4, 25), new DateTime(2021, 4, 29))
        //    };

        //    DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

        //    Assert.IsTrue(result.Equals(new DateTime(2021, 5, 4)));
        //}
    }
}
