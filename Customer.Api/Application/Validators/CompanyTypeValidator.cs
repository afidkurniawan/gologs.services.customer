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
