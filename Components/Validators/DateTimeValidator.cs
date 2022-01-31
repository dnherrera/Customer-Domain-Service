using System;
using Customer.Components.Enums;
using Customer.Components.Extensions;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// The duration validator.
    /// </summary>
    public static class DateTimeValidator
    {
        /// <summary>
        /// Validates the specified start date and end date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="timezoneOffset">The timezone offset.</param>
        /// <param name="validStartDate">The valid start date.</param>
        /// <param name="validEndDate">The valid end date.</param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(string startDate, string endDate, double timezoneOffset, out DateTime? validStartDate, out DateTime? validEndDate)
        {
            var errorInfo = new ErrorInfo();

            validStartDate = null;
            validEndDate = null;

            DateTime newStartDate;

            if (string.IsNullOrWhiteSpace(startDate))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "Start date cannot be empty.";
                return errorInfo;
            }
            else if (!DateTime.TryParse(startDate, out newStartDate))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Start date '{startDate}' is not valid.";
                return errorInfo;
            }

            DateTime newEndDate;

            if (string.IsNullOrWhiteSpace(endDate))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "End date cannot be empty.";
                return errorInfo;
            }
            else if (!DateTime.TryParse(endDate, out newEndDate))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"End date '{endDate}' is not valid.";
                return errorInfo;
            }

            return Validate(newStartDate, newEndDate, timezoneOffset, out validStartDate, out validEndDate);
        }

        /// <summary>
        /// Validates the specified start date and end date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="timezoneOffset">The timezone offset.</param>
        /// <param name="validStartDate">The valid start date.</param>
        /// <param name="validEndDate">The valid end date.</param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(DateTime? startDate, DateTime? endDate, double timezoneOffset, out DateTime? validStartDate, out DateTime? validEndDate)
        {
            var errorInfo = new ErrorInfo();

            validStartDate = null;
            validEndDate = null;

            // Make sure duration request must have start date and end date
            if (!startDate.HasValue)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "Start date cannot be empty.";
                return errorInfo;
            }

            if (!endDate.HasValue)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "End date cannot be empty.";
                return errorInfo;
            }

            // Start date cannot be greater than end date
            if (startDate.Value > endDate.Value)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Start date '{startDate}' cannot be greater than end date '{endDate}'.";
                return errorInfo;
            }

            validStartDate = startDate.Value.ToUniversalTime(timezoneOffset);
            validEndDate = endDate.Value.ToUniversalTime(timezoneOffset);

            return errorInfo;
        }

        /// <summary>
        /// Validates the specified start date and end date.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <param name="validDate"></param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(string dateString, out DateTime? validDate)
        {
            var errorInfo = new ErrorInfo();
            validDate = null;

            DateTime newStartDate;

            if (string.IsNullOrWhiteSpace(dateString))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = "Date cannot be empty.";
                return errorInfo;
            }
            else if (!DateTime.TryParse(dateString, out newStartDate))
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidDate;
                errorInfo.ErrorMessage = $"Date '{dateString}' is not valid.";
                return errorInfo;
            }

            return Validate(newStartDate, out validDate);
        }


        /// <summary>
        /// Validates the specified start date and end date.
        /// </summary>
        /// <param name="dateRequest">The date request.</param>
        /// <param name="validDate"></param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(DateTime? dateRequest, out DateTime? validDate)
        {
            var errorInfo = new ErrorInfo();
            var currentYear = DateTime.Now.Year;
            validDate = null;

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

            validDate = dateRequest.Value;

            return errorInfo;
        }
    }
}
