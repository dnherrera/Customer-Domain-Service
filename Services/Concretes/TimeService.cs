using System;
using CustomerAPI.Services.Contracts;

namespace CustomerAPI.Services.Concretes
{
    /// <summary>
    /// Time Service
    /// </summary>
    public class TimeService : ITimeService
    {
        private DateTime LastDateTime;
        private TimeSpan CurrentTime;

        /// <summary>
        /// Get Last Date Time Now
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastTimeNow()
        {
            LastDateTime = DateTime.Now;
            return LastDateTime;
        }

        /// <summary>
        /// Get the Current Time 
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCurrentTimeNow()
        {
            CurrentTime = DateTime.Now.TimeOfDay;
            return CurrentTime;
        }
    }
}
