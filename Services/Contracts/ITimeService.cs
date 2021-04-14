using System;

namespace CustomerAPI.Services
{
    public interface ITimeService
    {
        DateTime GetLastTimeNow();
        TimeSpan GetCurrentTimeNow();
    }
}
