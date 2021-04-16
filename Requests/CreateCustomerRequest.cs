﻿using System;
using System.Collections.Generic;

namespace CustomerAPI.Requests
{
    /// <summary>
    /// Create Customer Request
    /// </summary>
    public class CreateCustomerRequest
    {
        /// <summary>
        /// Gets or sets the Fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Date of Birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public ICollection<CreateAddressRequest> Address { get; set; }
    }
}
