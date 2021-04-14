namespace CustomerAPI.Models
{
    /// <summary>
    /// Address Model
    /// </summary>
    public class AddressModel
    {
        /// <summary>
        /// Gets or sets Address Identifier
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

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        public CustomerModel Customer { get; set; }

        /// <summary>
        /// Gets or sets the Customer Id
        /// </summary>
        public int CustomerId { get; set; }
    }
}
