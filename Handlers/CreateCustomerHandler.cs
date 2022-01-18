using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Components.Dtos.Responses;
using Customer.Components.Enums;
using Customer.Components.Exceptions;
using Customer.Components.Requests;
using Customer.Components.Validators;
using CustomerAPI.Models;
using CustomerAPI.Services.Contracts;
using MediatR;

namespace CustomerAPI.Handlers
{
    /// <summary>
    /// Create Customer Handler
    /// </summary>
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomerHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public CreateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository)
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
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<CustomerDto> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            // Validates Name
            var errorInfo = NameValidator.Validate(request.Name, out string validName);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            // Validates DOB
            errorInfo = DateTimeValidator.Validate(request.DateOfBirth, out DateTime? validDateOfBirth);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            // Check if user already exists
            var user = await _customerRepository.GetCustomerByName(validName, validDateOfBirth);
            if (user?.Id != null)
            {
                errorInfo.ErrorCode = ErrorTypes.InvalidName;
                errorInfo.ErrorMessage = $"This name '{validName}' already exists.";
                throw new BadInputException(errorInfo);
            }

            // Validates Address
            if (request.Addresses is null)
            {
                errorInfo.ErrorMessage = "Address is required";
                errorInfo.ErrorCode = ErrorTypes.InvalidAddressLine;
                throw new BadInputException(errorInfo);
            }

            // Validate ICollection Address
            var listAddress = new List<AddressModel>();
            foreach (var item in request.Addresses)
            {
                errorInfo = AddressLineValidator.Validate(item.AddressLine1, out string validAddress);
                if (errorInfo.ErrorCode != ErrorTypes.OK)
                {
                    throw new BadInputException(errorInfo);
                }

                string validAddress2 = null;
                if (!string.IsNullOrWhiteSpace(item.AddressLine2))
                {
                    errorInfo = AddressLineValidator.Validate(item.AddressLine2, out validAddress2);
                    if (errorInfo.ErrorCode != ErrorTypes.OK)
                    {
                        throw new BadInputException(errorInfo);
                    }
                }

                errorInfo = CityValidator.Validate(item.City, out string validCity);
                if (errorInfo.ErrorCode != ErrorTypes.OK)
                {
                    throw new BadInputException(errorInfo);
                }

                errorInfo = StateValidator.Validate(item.State, out string validState);
                if (errorInfo.ErrorCode != ErrorTypes.OK)
                {
                    throw new BadInputException(errorInfo);
                }

                listAddress.Add(new AddressModel()
                {
                    AddressLine1 = validAddress,
                    AddressLine2 = validAddress2,
                    City = validCity,
                    State = validState
                });
            }
            
            // Map request into model
            var customerModel = _mapper.Map<CustomerModel>(request);
            customerModel.Name = validName;
            customerModel.DateOfBirth = validDateOfBirth;
            customerModel.Addresses = listAddress;

            var resultModel = await _customerRepository.CreateCustomerAsync(customerModel);
            
            var resultDto = _mapper.Map<CustomerDto>(resultModel);

            return resultDto;
        }
    }
}
