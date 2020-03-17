using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }

        public ICollection<Address> Address { get; set; }

        public Customer()
        {
            Address = new Collection<Address>();
        }
    }
}
