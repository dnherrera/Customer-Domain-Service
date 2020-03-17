using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Dtos
{
    public class CustomerDto
    {
        [Required]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }

        public ICollection<AddressDto> Address { get; set; }
    }
}
