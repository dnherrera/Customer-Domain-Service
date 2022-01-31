using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Components.Enums;
using Customer.Components.Exceptions;
using Customer.Components.Requests;
using Customer.Components.Validators;
using CustomerAPI.Services.Contracts;
using MediatR;

namespace CustomerAPI.Handlers
{
    /// <summary>
    /// The Delete Customer Handler
    /// </summary>
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, int>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCustomerHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public DeleteCustomerHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="Customer.Components.Exceptions.BadInputException"></exception>
        /// <exception cref="Customer.Components.Exceptions.NotFoundException"></exception>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            // Validates the Identifier
            var errorInfo = IdentifierValidator.Validate(request.Id);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

           // Validates the customer info
            var userDetails = await _customerRepository.GetCustomerByIdentifierAsync(request.Id);

            errorInfo = CustomerObjectValidator.Validate(userDetails, request.Id);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new NotFoundException(errorInfo);
            }

            // delete user document
            var deleteReponse = await _customerRepository.DeleteCustomerByIdentifierAsync(request.Id);

            if (deleteReponse < 1)
            {
                errorInfo.ErrorCode = ErrorTypes.CannotDeleteUser;
                errorInfo.ErrorMessage = "Cannot delete user.";
                throw new Exception(errorInfo.ErrorMessage);
            }

            return request.Id;
        }
    }
}
