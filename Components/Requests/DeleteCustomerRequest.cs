using MediatR;

namespace Customer.Components.Requests
{
    /// <summary>
    /// The Delete Customer Request
    /// </summary>
    /// <seealso cref="IRequest{String}" />
    public class DeleteCustomerRequest : IRequest<int>
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }
    }
}
