// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using FluentValidation;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api.Application.Validators
{
    public class CompanyTypeValidator : AbstractValidator<CompanyTypeInputDto>
    {
        public CompanyTypeValidator()
        {
            RuleFor(ct => ct.TypeName)
                .NotNull()
                .Length(3, 32);
        }
    }
}
