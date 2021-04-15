namespace CustomerAPI.Requests
{
    /// <summary>
    /// Get Customer Request
    /// </summary>
    public class GetCustomerRequest
    {
        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets the is asc sorting.
        /// </summary>
        public bool IsAscSorting { get; set; }

        /// <summary>
        /// Gets or sets the page index
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int? PageSize { get; set; }
    }
}
