using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface IErrorLogging
    {
        void ErrorLogging(Exception ex);
    }
}
