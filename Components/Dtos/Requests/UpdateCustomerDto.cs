using System.Collections.Generic;
using Customer.Components.Dtos.Responses;

namespace Customer.Components.Dtos.Requests
{
    /// <summary>
    /// The Update Customer Dto
    /// </summary>
    public class UpdateCustomerDto
    {
        /// <summary>
        /// Gets or sets the journal identifier.
        /// </summary>
        /// <value>
        /// The journal identifier.
        /// </value>
        public int CustomerIdentifier { get; set; }

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
        public List<AddressDto> Address { get; set; }
    }
}
