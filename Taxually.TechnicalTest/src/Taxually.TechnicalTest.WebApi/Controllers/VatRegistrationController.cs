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
        private readonly ILogger<VatRegistrationController> _logger;
        
        public VatRegistrationController(IMediator mediator, ILogger<VatRegistrationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            _logger.LogInformation($"Taxually.TechnicalTest.Post called with parameters:" +
                $" Country: {request.Country} CompanyName: {request.CompanyName} CompanyId: {request.CompanyId}");

            var response = await _mediator.Send(request);

            if (response.Success)
            {
                _logger.LogInformation($"VatRegistration is succcessful for company: {request.CompanyName}");
                return Ok();
            }
            else
            {
                _logger.LogError($"Vatregistration failed for company: {request.CompanyName}, error: {response.ErrorMessage}");
                return BadRequest(response.ErrorMessage);
            }
        }
    }
}
