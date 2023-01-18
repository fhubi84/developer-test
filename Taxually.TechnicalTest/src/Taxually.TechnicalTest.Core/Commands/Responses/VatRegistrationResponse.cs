using System;
using System.Collections.Generic;
using System.Text;

namespace Taxually.TechnicalTest.Core.Commands.Responses
{
    public class VatRegistrationResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
