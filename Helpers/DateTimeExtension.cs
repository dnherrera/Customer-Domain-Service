using System;

namespace CustomerAPI.Helpers
{
    /// <summary>
    /// The date time extension.
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Converts specified date time to universal time.
        /// </summary>
        /// <param name="localTime">The local time.</param>
        /// <param name="timezoneOffset">The timezone offset.</param>
        /// <returns>The universal time.</returns>
        public static DateTime ToUniversalTime(this DateTime localTime, double timezoneOffset)
        {
            return localTime.Add(new TimeSpan(0, (int)timezoneOffset, 0));
        }

        /// <summary>
        /// Converts specified date time to local time.
        /// </summary>
        /// <param name="universalTime">The universal time.</param>
        /// <param name="timezoneOffset">The timezone offset.</param>
        /// <returns>The universal time.</returns>
        public static DateTime ToLocalTime(this DateTime universalTime, double timezoneOffset)
        {
            return universalTime.Subtract(new TimeSpan(0, (int)timezoneOffset, 0));
        }
    }
}
