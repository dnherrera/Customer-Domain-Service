using CustomerAPI.Dtos;
using CustomerAPI.Dtos.Requests;
using CustomerAPI.Dtos.Responses;
using MediatR;

namespace CustomerAPI.Requests
{
    /// <summary>
    /// Get Customer Request
    /// </summary>
    /// <seealso cref="CustomerAPI.Dtos.Requests.GetCustomerDto" />
    public class GetCustomerRequest : GetCustomerDto, IRequest<PagingDto<CustomerDto>>
    {
    }
}
