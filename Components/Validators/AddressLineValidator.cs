using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// Address Line Validator
    /// </summary>
    public static class AddressLineValidator
    {
        /// <summary>
        /// Validates the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns></returns>
        public static ErrorInfo Validate(string address, out string validValue)
        {
            validValue = null;

            var errorInfo = new ErrorInfo();

            if (!string.IsNullOrWhiteSpace(address) && address.Length > 100)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidAddressLine;
                errorInfo.ErrorMessage = "Address Line must not exceed to 100 characters";
                return errorInfo;
            }

            address = address.Trim();

            validValue = address;

            return errorInfo;
        }
    }
}
