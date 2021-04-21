using CustomerAPI.Requests;

namespace CustomerAPI.Dtos
{
    /// <summary>
    /// Address Validator
    /// </summary>
    public static class AddressValidator
    {
        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="addressRequest"></param>
        /// <returns>The <see cref="ErrorInfo"/>.</returns>
        public static ErrorInfo Validate(AddressRequest addressRequest)
        {
            var e = new ErrorInfo();

            if (string.IsNullOrWhiteSpace(addressRequest.AddressLine1))
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "Address 1 cannot be empty.";
                return e;
            }

            if (string.IsNullOrWhiteSpace(addressRequest.City))
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "City cannot be empty.";
                return e;
            }

            if (string.IsNullOrWhiteSpace(addressRequest.State))
            {
                e.ErrorCode = ErrorTypes.InvalidFullName;
                e.ErrorMessage = "State cannot be empty.";
                return e;
            }

            return e;
        }
    }
}
