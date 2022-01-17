using System;
using System.Collections.Generic;

namespace CustomerAPI.Models
{
    /// <summary>
    /// The Customer Model
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Gets or sets the Customer Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Customer Fullname
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Date of Birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets Address
        /// </summary>
        public virtual ICollection<AddressModel> Addresses { get; set; }
    }
}
