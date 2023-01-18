using MediatR;
using Taxually.TechnicalTest.Core.Commands.Responses;

namespace Taxually.TechnicalTest.Core.Commands.Requests
{
    public class VatRegistrationRequest : IRequest<VatRegistrationResponse>
    {
        public string? CompanyName { get; set; }
        public string? CompanyId { get; set; }
        public string? Country { get; set; }
    }
}
