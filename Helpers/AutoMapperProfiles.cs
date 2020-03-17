using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CustomerAPI.Dtos;
using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap()
           .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Address, AddressDto>().ReverseMap()
                .EqualityComparison((sir, si) => sir.Id == si.Id);
        }
    }
}
