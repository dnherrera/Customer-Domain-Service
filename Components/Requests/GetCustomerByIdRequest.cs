using Customer.Components.Dtos.Responses;
using MediatR;

namespace Customer.Components.Requests
{
    /// <summary>
    /// Get Customer By Id Request
    /// </summary>
    public class GetCustomerByIdRequest : IRequest<CustomerDto>
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }
    }
}
