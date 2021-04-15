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
        /// <param name="user">The user.</param>
        /// <param name="userIdentifier">The user identifier.</param>
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
    }
}
