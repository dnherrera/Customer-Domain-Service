using System.Collections.Generic;
using Customer.Components.Dtos.Responses;

namespace Customer.Components.Dtos.Requests
{
    /// <summary>
    /// Create Customer Dto
    /// </summary>
    public class CreateCustomerDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ICollection<AddressDto> Addresses { get; set; }
    }
}
