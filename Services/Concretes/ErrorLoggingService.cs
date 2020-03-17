using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class ErrorLoggingService : IErrorLogging
    {
        private readonly ITimeService _timeService;

        public ErrorLoggingService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void ErrorLogging(Exception ex)
        {
            var username = Environment.UserName;
            string strPath = @$"C:\Users\{username}\source\repos\Log.Txt";
             if (!File.Exists(strPath))
             {
                 File.Create(strPath).Dispose();
             }
             using (StreamWriter sw = File.AppendText(strPath))
             {
                 sw.WriteLine("=============Error Logging ===========");
                 sw.WriteLine("===========Start============= " + _timeService.GetLastTimeNow());
                 sw.WriteLine("Error Message: " + ex.Message);
                 sw.WriteLine("Stack Trace: " + ex.StackTrace);
                 sw.WriteLine("===========End============= " + _timeService.GetLastTimeNow());
             }
        }
        
    }
}
