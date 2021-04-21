namespace CustomerAPI.Dtos
{
    /// <summary>
    /// The customer Identifier Validator.
    /// </summary>
    public static class IdentifierValidator
    {
        /// <summary>
        /// Validates journal object.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The error info object.</returns>
        public static ErrorInfo Validate(object identifier)
        {
            var e = new ErrorInfo();

            if (identifier == null)
            {
                e.ErrorCode = ErrorTypes.InvalidIdentifier;
                e.ErrorMessage = $"Customer Id '{identifier}' is empty.";
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
        public static ErrorInfo Validate(int? identifier1, int? identifier2)
        {
            var e = new ErrorInfo();

            if (!identifier1.HasValue || !identifier2.HasValue)
            {
                e.ErrorCode = ErrorTypes.IdentifierMismatch;
                e.ErrorMessage = "Identifier is null or empty.";
                return e;
            }

            if (identifier1 != identifier2)
            {
                e.ErrorCode = ErrorTypes.IdentifierMismatch;
                e.ErrorMessage = "Identifiers don't match.";
                return e;
            }

            return e;
        }
    }
}
