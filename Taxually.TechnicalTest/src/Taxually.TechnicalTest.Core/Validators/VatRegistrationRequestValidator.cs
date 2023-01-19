using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Taxually.TechnicalTest.Core.Commands.Requests;

namespace Taxually.TechnicalTest.Core.Validators
{
    public class VatRegistrationRequestValidator : AbstractValidator<VatRegistrationRequest>
    {
        public override ValidationResult Validate(ValidationContext<VatRegistrationRequest> context)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate.Country))
            {
                return new ValidationResult(
                    new[]
                    {
                        new ValidationFailure(
                            "",
                            $"{nameof(VatRegistrationRequest.Country)} must be provided!")
                    });
            }

            return new ValidationResult();
        }

        //Additional validators might be required.
    }
}
