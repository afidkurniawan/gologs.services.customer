// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using FluentValidation;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api.Application.Validators
{
    public class PersonValidator : AbstractValidator<PersonInputDto>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .MaximumLength(256)
                .EmailAddress();

            RuleFor(p => p.Firstname)
                .NotNull()
                .Length(3, 64)
                .PersonName();

            RuleFor(p => p.Lastname)
                .Length(3, 64)
                .PersonName();

            RuleFor(p => p.Npwp)
                .Length(15)
                .NumbersOnly();

            RuleFor(c => c.CompanyId)
                .InclusiveBetween(1, Int32.MaxValue)
                .When(c => c.CompanyId.HasValue);

            RuleFor(c => c.CompanyRoleId)
                .InclusiveBetween(1, Int32.MaxValue)
                .When(c => c.CompanyRoleId.HasValue);
        }
    }
}
