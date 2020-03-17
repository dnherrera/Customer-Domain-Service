using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class TimeService : ITimeService
    {
        private DateTime LastDateTime;
        private TimeSpan CurrentTime;


        public TimeService()
        {

        }
        public DateTime GetLastTimeNow()
        {
            LastDateTime = DateTime.Now;
            return LastDateTime;
        }

        public TimeSpan GetCurrentTimeNow()
        {
            CurrentTime = DateTime.Now.TimeOfDay;
            return CurrentTime;
        }
    }
}
