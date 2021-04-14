using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Customer Fullname
        /// </summary>
        [Required]
        public string FullName { get; set; }

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
        public ICollection<AddressModel> Address { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerModel" /> class.
        /// </summary>
        public CustomerModel()
        {
            Address = new Collection<AddressModel>();
        }
    }
}
