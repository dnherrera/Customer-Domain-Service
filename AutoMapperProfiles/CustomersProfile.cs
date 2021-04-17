using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CustomerAPI.Dtos;
using CustomerAPI.Models;
using CustomerAPI.Requests;

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
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(cm => cm.Age, ccr => ccr.MapFrom(d => CalculateAge.Calculate(d.DateOfBirth)));

            // Mapping Address Model to Address DTO 
            CreateMap<AddressModel, AddressDto>()
                .ReverseMap()
                .EqualityComparison((adto, adm) => adto.Id == adm.Id);

            // Mapping Create Customer Request to Customer Model 
            CreateMap<CreateCustomerRequest, CustomerModel>()
                .ForMember(cm => cm.Id, ccr => ccr.Ignore())
                .ForMember(cm => cm.Age, ccr => ccr.MapFrom(d => CalculateAge.Calculate(d.DateOfBirth)));

            // Mapping Address Request to Address Model
            CreateMap<AddressRequest, AddressModel>()
                .ForMember(am => am.Id, car => car.Ignore())
                .ForMember(am => am.Customer, car => car.Ignore())
                 .ForMember(am => am.CustomerId, car => car.Ignore());
        }
    }
}
