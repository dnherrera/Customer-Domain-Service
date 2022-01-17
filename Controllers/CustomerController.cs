using System.Threading.Tasks;
using AutoMapper;
using Customer.Components.Dtos.Requests;
using Customer.Components.Dtos.Responses;
using Customer.Components.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerAPI.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The mapper.</param>
        public CustomerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
       

        /// <summary>
        /// Gets Customer Collection.
        /// </summary>
        /// <returns>
        /// The list collection of customer collection.
        /// </returns>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(PagingDto<CustomerDto>))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagingDto<CustomerDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<ActionResult> GetCustomerCollection([FromQuery] GetCustomerRequest getCustomerDto)
        {
            var request = _mapper.Map<GetCustomerRequest>(getCustomerDto);

            var result = await _mediator.Send(request);

            return Ok(result);
        }


        /// <summary>
        /// Gets the customer by identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<ActionResult> GetCustomerById(int customerId)
        {
            var request = new GetCustomerByIdRequest { CustomerId = customerId };

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Creates the customer information.
        /// </summary>
        /// <param name="requestDto">The request dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<IActionResult> CreateCustomerInfo([FromBody] CreateCustomerDto requestDto)
        {
            var request = _mapper.Map<CreateCustomerRequest>(requestDto);
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /*
                             [HttpPut("{id}")]
                             [Consumes("application/json")]
                             [Produces("application/json", Type = typeof(CustomerDto))]
                             [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
                             [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
                             [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
                             public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequest request)
                             {
                                 var errorInfo = new ErrorInfo();

                                 // Verify customer identifier from route and request body
                                 errorInfo = IdentifierValidator.Validate(id, request.CustomerIdentifier);
                                 if (errorInfo.ErrorCode != ErrorTypes.OK)
                                 {
                                     throw new BadInputException(errorInfo);
                                 }

                                 // Find customer to update
                                 var currentCustomer = await _customerRepository.GetCustomerByIdentifierAsync(id);

                                 // Customer
                                 errorInfo = CustomerObjectValidator.Validate(currentCustomer, id);
                                 if (errorInfo.ErrorCode != ErrorTypes.OK)
                                 {
                                     throw new BadInputException(errorInfo);
                                 }

                                 bool isModified = false;

                                 // Fullname
                                 if (request.FullName != null && currentCustomer.FullName != request.FullName)
                                 {
                                     errorInfo = FullNameValidator.Validate(request.FullName, out string fullName);
                                     if (errorInfo.ErrorCode != ErrorTypes.OK)
                                     {
                                         throw new BadInputException(errorInfo);
                                     }

                                     isModified = true;
                                     currentCustomer.FullName = fullName;
                                 }

                                 // Date of Birth
                                 if (request.DateOfBirth != null && currentCustomer.DateOfBirth.ToString() != request.DateOfBirth)
                                 {
                                     errorInfo = DateTimeValidator.Validate(request.DateOfBirth, out DateTime? validDate);
                                     if (errorInfo.ErrorCode != ErrorTypes.OK)
                                     {
                                         throw new BadInputException(errorInfo);
                                     }

                                     isModified = true;
                                     currentCustomer.DateOfBirth = validDate;
                                     currentCustomer.Age = CalculateAge.Calculate(request.DateOfBirth);
                                 }

                                 // Validate ICollection Address
                                 if (request?.Address != null)
                                 {
                                     foreach (var item in request.Address)
                                     {
                                         errorInfo = AddressValidator.Validate(item);
                                         if (errorInfo.ErrorCode != ErrorTypes.OK)
                                         {
                                             throw new BadInputException(errorInfo);
                                         }
                                     }

                                     isModified = true;
                                     currentCustomer.Address = _mapper.Map<List<AddressModel>>(request.Address);
                                 }

                                 if (isModified)
                                 {
                                     // TODO: To implement the updated and created date in Model
                                     // newCustomer.UpdatedDate = DateTime.UtcNow;
                                     await _customerRepository.UpdateCustomerAsync(currentCustomer);
                                 }

                                 // Map Journal Model to Journal Dto
                                 var resultDto = _mapper.Map<CustomerDto>(currentCustomer);

                                 return Ok(resultDto);
                             }

                             /// <summary>
                             /// Delete Customer Details
                             /// </summary>
                             /// <param name="id"></param>
                             /// <returns>
                             /// Deleted Customer Id
                             /// </returns>
                             [HttpDelete("{id}")]
                             [Consumes("application/json")]
                             [Produces("application/json", Type = typeof(int))]
                             [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
                             [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
                             [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
                             public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
                             {
                                 var errorInfo = new ErrorInfo();

                                 // Find customer to delete
                                 var customerToDelete = await _customerRepository.GetCustomerByIdentifierAsync(id);

                                 // Customer
                                 errorInfo = CustomerObjectValidator.Validate(customerToDelete, id);
                                 if (errorInfo.ErrorCode != ErrorTypes.OK)
                                 {
                                     throw new BadInputException(errorInfo);
                                 }

                                 // Delete the Customer By Id
                                 var deletedId = await _customerRepository.DeleteCustomerByIdentifierAsync(id);

                                 return Ok(new { customerId = deletedId });
                             }*/
    }
}