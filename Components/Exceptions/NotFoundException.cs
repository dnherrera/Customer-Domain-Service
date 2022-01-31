using Customer.Components.Validators.Models;

namespace Customer.Components.Exceptions
{
    /// <summary>
    /// The not found exception
    /// </summary>
    /// <seealso cref="BaseException" />
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="errorInfo">The error info.</param>
        public NotFoundException(ErrorInfo errorInfo)
            : base(errorInfo)
        {
        }
    }
}
