using System;
using FluentValidation;
using GoLogs.Services.Customer.Api.Models;

namespace GoLogs.Services.Customer.Api.Application.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyInputDto>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.CompanyName)
                .NotNull()
                .Length(3, 64);

            RuleFor(c => c.Npwp)
                .NotNull()
                .Length(15)
                .NumbersOnly();

            RuleFor(c => c.Nib)
                .Length(13)
                .NumbersOnly();

            RuleFor(c => c.Nik)
                .Length(8)
                .NumbersOnly();

            RuleFor(c => c.Npppjk)
                .Length(6)
                .NumbersOnly();

            RuleFor(c => c.CompanyTypeId)
                .NotNull()
                .InclusiveBetween(1, Int32.MaxValue);

            RuleFor(c => c.BrokerCompanyId)
                .InclusiveBetween(1, Int32.MaxValue)
                .When(c => c.BrokerCompanyId.HasValue);
        }
    }
}
