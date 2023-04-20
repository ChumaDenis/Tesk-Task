using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime resDate = startDate;
            resDate= resDate.AddDays(dayCount-1);

            if (weekEnds!=null)
            {
                weekEnds.ToList().ForEach(weekEnd =>
                {
                    DateTime dateOfWeekEnd = weekEnd.StartDate;
                    while (dateOfWeekEnd.CompareTo(weekEnd.EndDate) <= 0)
                    {
                        if (dateOfWeekEnd.CompareTo(startDate) >= 0 && dateOfWeekEnd.CompareTo(resDate) <= 0)
                        {
                            resDate = resDate.AddDays(1);
                        }
                        dateOfWeekEnd = dateOfWeekEnd.AddDays(1);
                    }
                });
            }
            return resDate;
        }
    }
}
