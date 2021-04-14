using System;

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
