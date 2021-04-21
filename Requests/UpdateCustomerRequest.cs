using System;
using System.Collections.Generic;

namespace CustomerAPI.Requests
{
    /// <summary>
    /// Update Customer Request
    /// </summary>
    public class UpdateCustomerRequest
    {
        /// <summary>
        /// Gets or sets the journal identifier.
        /// </summary>
        /// <value>
        /// The journal identifier.
        /// </value>
        public int CustomerIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the Fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Date of Birth
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public ICollection<AddressRequest> Address { get; set; }
    }
}
