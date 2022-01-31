using System;
using System.Linq;
using System.Text.RegularExpressions;
using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// The Name Validator
    /// </summary>
    public static class NameValidator
    {
        /// <summary>
        /// Validates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns><see cref="ErrorInfo"/></returns>
        public static ErrorInfo Validate(string name, out string validValue)
        {
            validValue = null;

            var e = new ErrorInfo();

            if (string.IsNullOrWhiteSpace(name))
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name cannot be empty.";
                return e;
            }

            name = name.Trim();

            if (name.Length > 24)
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name must be less than 24 characters.";
                return e;
            }

            if (name.Length < 5)
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name must be more than 5 characters.";
                return e;
            }

            if (name.Count(x => x == '.') > 3)
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name cannot have more than 3 dots.";
                return e;
            }

            if (name.Count(x => x == '_') > 3)
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name cannot have more than 3 underscores.";
                return e;
            }

            // check for illegal characters
            var pattern = @"[^a-zA-Z0-9._]";
            var result = Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            if (result)
            {
                e.ErrorCode = ErrorTypes.InvalidName;
                e.ErrorMessage = "Name has illegal characters. Only [a-z], [0-9], [.], [_] are valid.";
                return e;
            }

            validValue = name.ToLower();

            return e;
        }
    }
}
