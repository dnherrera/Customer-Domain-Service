using System;

namespace CustomerAPI.Services.Contracts
{
    /// <summary>
    /// Time Service Interface
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// Get Last Date Time Now
        /// </summary>
        /// <returns></returns>
        DateTime GetLastTimeNow();

        /// <summary>
        /// Get Current Time 
        /// </summary>
        /// <returns></returns>
        TimeSpan GetCurrentTimeNow();
    }
}
