using Customer.Components.Dtos.Requests;
using Customer.Components.Dtos.Responses;
using MediatR;

namespace Customer.Components.Requests
{
    /// <summary>
    /// Create Customer Dto
    /// </summary>
    /// <seealso cref="Customer.Components.Dtos.Requests.CreateCustomerDto" />
    public class CreateCustomerRequest : CreateCustomerDto, IRequest<CustomerDto>
    {
    }
}
