namespace CustomerAPI.Dtos
{
    /// <summary>
    /// The bad input exception.
    /// </summary>
    /// <seealso cref="BaseException" />
    public class BadInputException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadInputException"/> class.
        /// </summary>
        /// <param name="errorInfo">The error info.</param>
        public BadInputException(ErrorInfo errorInfo)
            : base(errorInfo)
        {
        }
    }
}
