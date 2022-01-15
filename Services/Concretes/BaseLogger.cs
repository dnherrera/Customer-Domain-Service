using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Services.Concretes
{
    /// <summary>
    /// Base logger
    /// </summary>
    public abstract class BaseLogger : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogger"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected BaseLogger(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs the start.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void LogStart([CallerMemberName] string methodName = "")
        {
            _logger.LogDebug("The method {0} was started.", methodName);
        }

        /// <summary>
        /// Logs the start.
        /// </summary>
        /// <param name="value">The value that pass into method.</param>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void LogStart(string value, [CallerMemberName] string methodName = "")
        {
            _logger.LogDebug("The method {0} was started with {1}.", methodName, value);
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void LogInfo(string message, [CallerMemberName] string methodName = "")
        {
            _logger.LogInformation("The method {0} got an info: {1}", methodName, message);
        }

        /// <summary>
        /// Logs the end.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void LogEnd([CallerMemberName] string methodName = "")
        {
            _logger.LogDebug("The method {0} was end.", methodName);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void LogError(Exception exception, [CallerMemberName] string methodName = "")
        {
            _logger.LogError(exception, "The method {0} got an error.", methodName);
        }
    }
}
