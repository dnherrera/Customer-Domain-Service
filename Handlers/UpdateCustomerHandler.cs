using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Components.Dtos.Responses;
using Customer.Components.Enums;
using Customer.Components.Exceptions;
using Customer.Components.Helpers;
using Customer.Components.Requests;
using Customer.Components.Validators;
using CustomerAPI.Models;
using CustomerAPI.Services.Contracts;
using MediatR;

namespace CustomerAPI.Handlers
{
    /// <summary>
    /// The Update Customer Handler
    /// </summary>
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, CustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomerHandler"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public UpdateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository)
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
        public async Task<CustomerDto> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            // Validates Identifier
            var errorInfo = IdentifierValidator.Validate(request.CustomerIdentifier, request.IdFromRoute);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            // Find customer to update
            var customerDetails = await _customerRepository.GetCustomerByIdentifierAsync(request.CustomerIdentifier);

            errorInfo = CustomerObjectValidator.Validate(customerDetails, request.CustomerIdentifier);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

             bool isModified = false;

            // Validate name
            if (request.Name != null && customerDetails.Name != request.Name)
            {
                errorInfo = NameValidator.Validate(request.Name, out string validName);
                if (errorInfo.ErrorCode != ErrorTypes.OK)
                {
                    throw new BadInputException(errorInfo);
                }

                isModified = true;
                customerDetails.Name = validName;
            }

            // Validates Date of Birth
            if (request.DateOfBirth != null && customerDetails.DateOfBirth.ToString() != request.DateOfBirth)
            {
                errorInfo = DateTimeValidator.Validate(request.DateOfBirth, out DateTime? validDate);
                if (errorInfo.ErrorCode != ErrorTypes.OK)
                {
                    throw new BadInputException(errorInfo);
                }

                isModified = true;
                customerDetails.DateOfBirth = validDate;
                customerDetails.Age = CalculateAge.Calculate(validDate.ToString());
            }

            // Validates Address
            var listAddress = new List<AddressModel>();
            var result = new AddressModel();
            if (request.Address != null || request.Address.Any())
            {
                foreach (var item in request.Address)
                {
                    errorInfo = AddressLineValidator.Validate(item.AddressLine1, out string validAddressLine1);
                    if (errorInfo.ErrorCode != ErrorTypes.OK)
                    {
                        throw new BadInputException(errorInfo);
                    }

                    errorInfo = AddressLineValidator.Validate(item.AddressLine2, out string validAddressLine2);
                    if (errorInfo.ErrorCode != ErrorTypes.OK)
                    {
                        throw new BadInputException(errorInfo);
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

                    listAddress.Add(new AddressModel
                    { 
                        AddressLine1 = validAddressLine1,
                        AddressLine2 = validAddressLine2,
                        State = validState,
                        City = validCity
                    });
                }

                customerDetails.Addresses = listAddress;
                isModified = true;
            }

            if (isModified)
            {
                // TODO: To implement the updated and created date in Model
                // newCustomer.UpdatedDate = DateTime.UtcNow;
                await _customerRepository.UpdateCustomerAsync(customerDetails);
            }

            var resultDto = _mapper.Map<CustomerDto>(customerDetails);

            return resultDto;
        }
    }
}
