using System.Linq;
using System.Text.RegularExpressions;

namespace CustomerAPI.Dtos
{
    /// <summary>
    /// The error info.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ErrorTypes ErrorCode { get; set; } = ErrorTypes.OK;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets pretty error title.
        /// </summary>
        public string ErrorTitle
        {
            get
            {
                var messageArr = Regex.Split(ErrorCode.ToString().Substring(1), @"(?<!^)(?=[A-Z])");
                return ErrorCode.ToString().ToUpper().First() + string.Join(" ", messageArr).ToLower();
            }
        }
    }
}
