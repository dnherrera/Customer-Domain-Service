namespace Customer.Components.Dtos.Requests
{
    /// <summary>
    /// Get Customer Dto
    /// </summary>
    public class GetCustomerDto
    {
        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        /// <value>
        /// The sort field.
        /// </value>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is asc sorting.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is asc sorting; otherwise, <c>false</c>.
        /// </value>
        public bool IsAscSorting { get; set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int? PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int? PageSize { get; set; }
    }
}
