using CustomerAPI.Dtos;
using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CustomerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IErrorLogging _errorLogging;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, IErrorLogging errorLogging)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _errorLogging = errorLogging;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get customer list")]
        [SwaggerResponse(200, "Success", typeof(CustomerModel))]
        [SwaggerResponse(401, "Unauthorized. Incorrect authentication key")]
        [ProducesResponseType(typeof(CustomerModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                IEnumerable<CustomerModel> customer = await _customerRepository.GetCustomersListAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _errorLogging.ErrorLogging(ex);
                return BadRequest(); ;
            }
        }

        [HttpGet("{CustomerId}")]
        [SwaggerOperation(Summary = "Get customer by Id")]
        [SwaggerResponse(200, "Success", typeof(CustomerModel))]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(401, "Unauthorized. Incorrect authentication key")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCustomer(int CustomerId)
        {
            try
            {
                CustomerModel customer = await _customerRepository.GetCustomerAsync(CustomerId);
                if (customer == null)
                    return NotFound($"Customer {CustomerId} not found");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _errorLogging.ErrorLogging(ex);
                return BadRequest(); ;
            }
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
            catch (Exception ex)
            {
                _errorLogging.ErrorLogging(ex);
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

                CustomerModel customerFromRepos = await _customerRepository.GetCustomerAsync(CustomerId);

                if (customerFromRepos == null)
                    return NotFound($"Could not find user with an ID of {CustomerId}");


                _mapper.Map(customerToUpdate, customerFromRepos);

                if (!await _customerRepository.SaveAll())
                    throw new Exception($"Updating user {CustomerId} failed on save");

                return Accepted(customerFromRepos);
            }
            catch (Exception ex)
            {
                _errorLogging.ErrorLogging(ex);
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
                await _customerRepository.DeleteCustomerAsync(CustomerId);
                return StatusCode(200, "Deleted successfully");
            }
            catch (Exception ex)
            {
                _errorLogging.ErrorLogging(ex);
                return BadRequest(); ;
            }
        }
    }
}