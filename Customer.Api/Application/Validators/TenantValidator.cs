// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Linq;
using FluentValidation;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api.Application.Validators
{
    public class TenantValidator : AbstractValidator<TenantInputDto>
    {
        public TenantValidator()
        {
            RuleFor(t => t.CompanyId)
                .NotNull()
                .InclusiveBetween(1, Int32.MaxValue);

            RuleFor(t => t.TenantName)
                .NotNull()
                .Length(3, 128)
                .Custom((db, context) =>
                {
                    var chars = db.ToCharArray();
                    if (!Char.IsLetter(chars[0]))
                    {
                        context.AddFailure($"{nameof(Tenant.TenantName)} must start with letter.");
                    }

                    if (chars.Any(Char.IsPunctuation))
                    {
                        context.AddFailure($"{nameof(Tenant.TenantName)} can only contain letters, numbers and underscores.");
                    }
                });
        }
    }
}
