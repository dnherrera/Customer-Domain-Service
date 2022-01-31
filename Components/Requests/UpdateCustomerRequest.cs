using System.Collections.Generic;
using Customer.Components.Dtos.Requests;
using Customer.Components.Dtos.Responses;
using MediatR;

namespace Customer.Components.Requests
{
    /// <summary>
    /// Update Customer Request
    /// </summary>
    public class UpdateCustomerRequest : UpdateCustomerDto, IRequest<CustomerDto>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int IdFromRoute { get; set; }
    }
}
