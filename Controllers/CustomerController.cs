using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAPI.Dtos;
using CustomerAPI.Models;
using CustomerAPI.Requests;
using CustomerAPI.Services;
using CustomerAPI.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseLogger
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;

        public CustomerController(
            ICustomerRepository customerRepository, 
            IMapper mapper, 
            ILogger<CustomerController> logger,
            IOptions<AppSetting> appSetting) : base(logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _appSetting = appSetting.Value;
        }

        /// <summary>
        /// Gets Customer Collection.
        /// </summary>
        /// <returns>The list collection of customer collection.</returns>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(PagingDto<CustomerDto>))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagingDto<CustomerDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<ActionResult> GetCustomerCollection([FromQuery] GetCustomerRequest request)
        {
            LogStart();
            try
            {
                var errorInfo = new ErrorInfo();

                // Paging
                errorInfo = PagingValidator.Validate(request.PageIndex, request.PageSize, _appSetting.MaximumPageSize, out int pageIndex, out int pageSize);
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

                LogEnd();
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gets the customer by identifier.
        /// </summary>
        /// <param name="id">The Customer Identifier.</param>
        /// <returns>
        /// Customer detail by Identifier
        /// </returns>
        [HttpGet("{customerId}")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<ActionResult> GetCustomerById(int customerId)
        {
            var result = await _customerRepository.GetCustomerByIdentifierAsync(customerId);
            return Ok(result);
        }


        [HttpPost("Customer")]
        [SwaggerOperation(Summary = "Create New Customer Profile")]
        [SwaggerResponse(201, "Success", typeof(CustomerModel))]
        [SwaggerResponse(401, "Unauthorized. Incorrect authentication key")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SaveCustomer([FromBody] CustomerDto customerdto)
        {
            try
            {
                CustomerModel customer = null;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                CustomerModel createdCustomer = _mapper.Map(customerdto, customer);
                await _customerRepository.CreateCustomerAsync(createdCustomer);

                return Accepted(createdCustomer);
            }
            catch (Exception)
            {
                return BadRequest(); ;
            }
        }

        [HttpPut("{CustomerId}")]
        [SwaggerOperation(Summary = "To Update Customer Profile")]
        [SwaggerResponse(201, "Success", typeof(CustomerModel))]
        [SwaggerResponse(400, "Bad Request. Please make sure your address Id is correct.")]
        [SwaggerResponse(401, "Unauthorized. Incorrect authentication key.")]
        [ProducesResponseType(typeof(CustomerModel), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int CustomerId, [FromBody] CustomerDto customerToUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                CustomerModel customerFromRepos = await _customerRepository.GetCustomerByIdentifierAsync(CustomerId);

                if (customerFromRepos == null)
                    return NotFound($"Could not find user with an ID of {CustomerId}");


                _mapper.Map(customerToUpdate, customerFromRepos);

                //if (!await _customerRepository.SaveAll())
                //    throw new Exception($"Updating user {CustomerId} failed on save");

                return Accepted(customerFromRepos);
            }
            catch (Exception)
            {
                return BadRequest($"Please make sure your address Id - {customerToUpdate.Address.SingleOrDefault().Id} is correct to update the profile.");
            }
        }

        [HttpDelete("{CustomerId}")]
        [SwaggerOperation(Summary = "To Delete Customer Profile")]
        [SwaggerResponse(200, "Success", typeof(CustomerModel))]
        [SwaggerResponse(401, "Unauthorized. Incorrect authentication key.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int CustomerId)
        {
            try
            {
                await _customerRepository.DeleteCustomerByIdentifierAsync(CustomerId);
                return StatusCode(200, "Deleted successfully");
            }
            catch (Exception)
            {
                return BadRequest(); ;
            }
        }
    }
}