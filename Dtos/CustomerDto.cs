using System;
using System.Collections.Generic;

namespace CustomerAPI.Dtos
{
    /// <summary>
    /// Customer DTO
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Date of Birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or set the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public ICollection<AddressDto> Address { get; set; }
    }
}
