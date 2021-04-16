using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace CustomerAPI.Dtos
{
    /// <summary>
    /// A full name has the following rule
    /// Allowed chars: [a-z] , [0-9] , [.] , [_]
    /// Maximum Lenght 24 characters
    /// No more then 3 dots
    /// No more then 3 underscores.
    /// </summary>
    public static class FullNameValidator
    {
        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns>The <see cref="ErrorInfo"/>.</returns>
        public static ErrorInfo Validate(string username, out string validValue)
        {
            validValue = null;

            var e = new ErrorInfo();

            if (string.IsNullOrWhiteSpace(username))
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "Fullname cannot be empty.";
                return e;
            }

            username = username.Trim();

            if (username.Length > 50)
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "Fullname cannot be longer than 50 characters.";
                return e;
            }

            // check for illegal characters
            var pattern = @"^[\p{L}\p{M}' \.\-]+$";
            var result = Regex.IsMatch(username, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            if (!result)
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "Fullname is not valid";
                return e;
            }

            validValue = username.Replace("'", "&#39;");

            return e;
        }
    }
}
