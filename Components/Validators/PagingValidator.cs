using Customer.Components.Enums;
using Customer.Components.Validators.Models;

namespace Customer.Components.Validators
{
    /// <summary>
    /// Paging validator
    /// </summary>
    public static class PagingValidator
    {
        /// <summary>
        /// Validates the specified page size.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="maximumPageSize">The maximum page size.</param>
        /// <param name="validPageIndex">The valid page index.</param>
        /// <param name="validPageSize">The valid page size.</param>
        /// <returns>
        /// Error info <see cref="ErrorInfo" />.
        /// </returns>
        public static ErrorInfo Validate(int? pageIndex, int? pageSize, int maximumPageSize, out int validPageIndex, out int validPageSize)
        {
            validPageIndex = 0;
            validPageSize = 0;

            var errorInfo = new ErrorInfo();

            pageIndex = !pageIndex.HasValue ? 1 : pageIndex;
            pageSize = !pageSize.HasValue ? 10 : pageSize;

            if (pageIndex <= 0)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidPaging;
                errorInfo.ErrorMessage = $"Page index '{pageIndex}' must be greater than zero.";
                return errorInfo;
            }

            if (pageSize <= 0 || pageSize > maximumPageSize)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidPaging;
                errorInfo.ErrorMessage = $"Page size '{pageSize}' should be between 1 to {maximumPageSize}.";
                return errorInfo;
            }

            validPageIndex = pageIndex.Value;
            validPageSize = pageSize.Value;

            return errorInfo;
        }
    }
}
