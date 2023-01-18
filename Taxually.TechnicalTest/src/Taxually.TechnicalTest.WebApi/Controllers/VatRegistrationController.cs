using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Core.Commands.Requests;

namespace Taxually.TechnicalTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public VatRegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok() : BadRequest(response.ErrorMessage);
        }
    }
}
