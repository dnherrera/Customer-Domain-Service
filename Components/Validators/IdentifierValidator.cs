using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// The customer Identifier Validator.
    /// </summary>
    public static class IdentifierValidator
    {
        /// <summary>
        /// Validates the specificed identifier.
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>The error info.</returns>
        public static ErrorInfo Validate(int identifier)
        {
            var e = new ErrorInfo();

            if (identifier <= 0)
            {
                e.ErrorCode = ErrorTypes.InvalidIdentifier;
                e.ErrorMessage = "Identifier is invalid.";
                return e;
            }

            return e;
        }

        /// <summary>
        /// Validates the specificed identifiers are matched or not.
        /// </summary>
        /// <param name="identifier1">The identifier1.</param>
        /// <param name="identifier2">The identifier2</param>
        /// <returns>The error info.</returns>
        public static ErrorInfo Validate(int identifier1, int identifier2)
        {
            var e = new ErrorInfo();
            try
            {
                if (identifier1 != identifier2)
                {
                    e.ErrorCode = ErrorTypes.IdentifierMismatch;
                    e.ErrorMessage = "The specificed identifiers are mismatched.";
                    return e;
                }
            }
            catch
            {
                e.ErrorCode = ErrorTypes.IdentifierMismatch;
                e.ErrorMessage = "Any identifier is invalid.";
                return e;
            }

            return e;
        }
    }
}
