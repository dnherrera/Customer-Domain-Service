using System;
using Customer.Components.Enums;
using Customer.Components.Exceptions;
using Customer.Components.Validators;
using Customer.Components.Validators.Models;

namespace Customer.Components.Helpers
{
    /// <summary>
    /// Calculate Age Based on Birthdate
    /// </summary>
    public static class CalculateAge
    {
        /// <summary>
        /// Calculate Method
        /// </summary>
        /// <param name="theDateTime"></param>
        /// <returns></returns>
        public static int Calculate(string theDateTime)
        {
            var errorInfo = new ErrorInfo();
            // Validate Date of Birth
            errorInfo = DateTimeValidator.Validate(theDateTime, out DateTime? validDate);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            int age = DateTime.Today.Year - validDate.Value.Year;

            if (validDate.Value.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}
