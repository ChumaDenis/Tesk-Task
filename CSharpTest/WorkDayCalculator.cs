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
            int daysinWeekEnds = 0;
            if (weekEnds != null)
            {
                weekEnds.ToList().ForEach(x =>
                {
                    daysinWeekEnds += x.EndDate.Day - x.StartDate.Day;
                });
                daysinWeekEnds++;
            }
            
            return startDate.AddDays(dayCount+daysinWeekEnds-1);
        }
    }
}
