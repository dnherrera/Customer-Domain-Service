using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Components.Dtos.Responses;
using Customer.Components.Enums;
using Customer.Components.Exceptions;
using Customer.Components.Requests;
using Customer.Components.Validators;
using CustomerAPI.Services.Contracts;
using MediatR;

namespace CustomerAPI.Handlers
{
    /// <summary>
    /// Get Customer By Id Handler
    /// </summary>
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, CustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCustomerByIdHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public GetCustomerByIdHandler(IMapper mapper, ICustomerRepository customerRepository)
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
        public async Task<CustomerDto> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var errorInfo = IdentifierValidator.Validate(request.CustomerId);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            var customerModel = await _customerRepository.GetCustomerByIdentifierAsync(request.CustomerId);

            // Validates Customer Object
            errorInfo = CustomerObjectValidator.Validate(customerModel, request.CustomerId);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new NotFoundException(errorInfo);
            }

            // Map model to dto
            var resultDto = _mapper.Map<CustomerDto>(customerModel);
            return resultDto;
        }
    }
}
