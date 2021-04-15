namespace CustomerAPI.Dtos
{
    /// <summary>
    /// The customer object validator.
    /// </summary>
    public static class CustomerObjectValidator
    {
        /// <summary>
        /// Validates journal object.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userIdentifier">The user identifier.</param>
        /// <returns>The error info object.</returns>
        public static ErrorInfo Validate(object user, object userIdentifier)
        {
            var e = new ErrorInfo();

            if (user == null)
            {
                e.ErrorCode = ErrorTypes.CustomerNotFound;
                e.ErrorMessage = $"Customer '{userIdentifier}' is not found.";
                return e;
            }

            return e;
        }
    }
}
