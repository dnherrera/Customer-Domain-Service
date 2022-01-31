using System;
using Customer.Components.Validators.Models;

namespace Customer.Components.Exceptions
{
    /// <summary>
    /// The base exception.
    /// </summary>
    /// <seealso cref="Exception" />
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Gets or set the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="errorInfo">The error info.</param>
        public BaseException(ErrorInfo errorInfo)
            : base(errorInfo.ErrorMessage)
        {
            Title = errorInfo.ErrorTitle;
            ErrorCode = (int)errorInfo.ErrorCode;
        }
    }
}
