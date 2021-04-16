namespace CustomerAPI.Requests
{
    /// <summary>
    /// Create Address Request
    /// </summary>
    public class CreateAddressRequest
    {
        /// <summary>
        /// Gets or sets the Address Line 1
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the Address Line 2
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the State
        /// </summary>
        public string State { get; set; }
    }
}
