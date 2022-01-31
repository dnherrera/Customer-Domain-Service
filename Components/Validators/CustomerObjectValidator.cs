using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// The customer object validator.
    /// </summary>
    public static class CustomerObjectValidator
    {
        /// <summary>
        /// Validates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userIdentifier">The user identifier.</param>
        /// <returns></returns>
        public static ErrorInfo Validate(object user, object userIdentifier)
        {
            var e = new ErrorInfo();

            if (user == null)
            {
                e.ErrorCode = ErrorTypes.CustomerNotFound;
                e.ErrorMessage = $"Customer '{userIdentifier}' not found.";
                return e;
            }

            return e;
        }
    }
}
