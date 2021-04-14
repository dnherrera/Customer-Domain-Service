using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CustomerAPI.Dtos;
using CustomerAPI.Models;

namespace CustomerAPI.Helpers
{
    /// <summary>
    /// The Customers Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile"/>
    /// <seealso cref="https://dotnettutorials.net/lesson/reverse-mapping-using-automapper/"/>
    public class CustomersProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersProfile" /> class.
        /// </summary>
        public CustomersProfile()
        {
            // Mapping Customer Model to Customer DTO 
            CreateMap<CustomerModel, CustomerDto>()
                .ReverseMap()
                .ForMember(m => m.Id, opt => opt.Ignore());

            // Mapping Address Model to Address DTO 
            CreateMap<AddressModel, AddressDto>()
                .ReverseMap()
                .EqualityComparison((sir, si) => sir.Id == si.Id);
        }
    }
}
