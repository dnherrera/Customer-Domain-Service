using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    public static class CityValidator
    {
        /// <summary>
        /// Validates the City
        /// </summary>
        /// <param name="city">The City.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns></returns>
        public static ErrorInfo Validate(string city, out string validValue)
        {
            validValue = null;

            var errorInfo = new ErrorInfo();

            if (!string.IsNullOrWhiteSpace(city))
            {
                city = city.Trim();

                if (city.Length > 100)
                {
                    errorInfo.ErrorCode = ErrorTypes.InvalidCity;
                    errorInfo.ErrorMessage = "City must not exceed to 100 characters";
                    return errorInfo;
                }
            }

            validValue = city;

            return errorInfo;
        }
    }
}
