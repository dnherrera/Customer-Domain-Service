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

        /// <summary>
        /// The Update Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetailsDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsDto))]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto requestDto)
        {
            var request = _mapper.Map<UpdateCustomerRequest>(requestDto);
            request.IdFromRoute = id;

            var result = await _mediator.Send(request);

            return Ok(result);
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
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerRequest { Id = id });
            return Ok(result);
        }
    }
}