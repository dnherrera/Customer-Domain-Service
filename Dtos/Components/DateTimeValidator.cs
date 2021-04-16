using System;

namespace CustomerAPI.Dtos
{
    /// <summary>
    /// The duration validator.
    /// </summary>
    public static class DateTimeValidator
    {
        /// <summary>
        /// Validates the specified start date and end date.
        /// </summary>
        /// <param name="dateRequest">The date request.</param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(DateTime? dateRequest)
        {
            var errorInfo = new ErrorInfo();
            var currentYear = DateTime.Now.Year;

            // Make sure duration request must have start date and end date
            if (!dateRequest.HasValue)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "Date cannot be empty.";
                return errorInfo;
            }

            if (dateRequest.Value.Year < 1900 || dateRequest.Value.Year > currentYear)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Year '{dateRequest.Value.Year}' is invalid.";
                return errorInfo;
            }

            if (dateRequest.Value.Month < 0 || dateRequest.Value.Month > 12)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Month cannot be more than 12 or less than 1.";
                return errorInfo;
            }

            if (dateRequest.Value.Day < 0 || dateRequest.Value.Day > 31)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Day cannot be more than 31 or less than 1.";
                return errorInfo;
            }

            return errorInfo;
        }
    }
}
