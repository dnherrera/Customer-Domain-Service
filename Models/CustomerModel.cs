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
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public virtual List<AddressModel> Addresses { get; set; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        public CustomerModel Copy()
        {
           return (CustomerModel)MemberwiseClone();
        }
    }
}
