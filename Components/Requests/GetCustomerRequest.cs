using Customer.Components.Dtos.Requests;
using Customer.Components.Dtos.Responses;
using MediatR;

namespace Customer.Components.Requests
{
    /// <summary>
    /// Get Customer Request
    /// </summary>
    public class GetCustomerRequest : GetCustomerDto, IRequest<PagingDto<CustomerDto>>
    {
    }
}
