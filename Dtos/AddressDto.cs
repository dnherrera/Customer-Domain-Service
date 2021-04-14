namespace CustomerAPI.Dtos
{
    /// <summary>
    /// Address DTO
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the Address Identifier
        /// </summary>
        public int Id { get; set; }

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
