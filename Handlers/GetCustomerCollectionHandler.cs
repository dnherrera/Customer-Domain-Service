using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAPI.Dtos;
using CustomerAPI.Dtos.Responses;
using CustomerAPI.Models;
using CustomerAPI.Requests;
using CustomerAPI.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Handlers
{
    /// <summary>
    /// Get Customer Collection Handler
    /// </summary>
    public class GetCustomerCollectionHandler : IRequestHandler<GetCustomerRequest, PagingDto<CustomerDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly int _maxPageSize;

        /// <summary>
        /// Get Customer Collection Handler
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="customerRepository"></param>
        /// <param name="configuration"></param>
        public GetCustomerCollectionHandler(
            IMapper mapper,
            ICustomerRepository customerRepository,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _maxPageSize = configuration.GetValue<int>("AppSetting:MaximumPageSize");
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PagingDto<CustomerDto>> Handle(GetCustomerRequest request, CancellationToken cancellationToken) 
        {
            // Paging
            var errorInfo = PagingValidator.Validate(request.PageIndex, request.PageSize, _maxPageSize, out int pageIndex, out int pageSize);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            // Sort Field
            errorInfo = SortFieldValidator.Validate<CustomerModel>(request.SortField, out string sortField);
            if (errorInfo.ErrorCode != ErrorTypes.OK)
            {
                throw new BadInputException(errorInfo);
            }

            var result = await _customerRepository.GetCustomersListAsync();
            var count = result.Count();

            var resultDto = new PagingDto<CustomerDto>
            {
                Collection = _mapper.Map<List<CustomerDto>>(result),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = count,
                TotalPages = (int)((count - 1) / pageSize + 1)
            };

            return resultDto;
        }
    }
}
