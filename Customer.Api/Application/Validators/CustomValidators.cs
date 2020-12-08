using System;
using System.Linq;
using FluentValidation;

namespace GoLogs.Services.Customer.Api.Application.Validators
{
    public static class CustomValidators
    {
        /// <summary>
        ///     Defines a name validator on the current rule builder.
        ///     A name can only contains letters, spaces, dots, and commas.
        /// </summary>
        public static IRuleBuilderInitial<T, string> PersonName<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((name, context) =>
            {
                var chars = name.ToCharArray();
                if (!Char.IsLetter(chars[0]))
                {
                    context.AddFailure($"'{context.DisplayName}' must start with letter.");
                }

                if (chars.Any(Char.IsDigit))
                {
                    context.AddFailure($"'{context.DisplayName}' can not contain numbers.");
                }

                if (chars.Any(c => Char.IsPunctuation(c) && c != '.' && c != ','))
                {
                    context.AddFailure($"'{context.DisplayName}' can not contain punctuations.");
                }
            });
        }

        /// <summary>
        ///     Defines a numbers-only string validator on the current rule builder.
        /// </summary>
        public static IRuleBuilderOptions<T, string> NumbersOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(p => p.ToCharArray().All(Char.IsDigit))
                .WithMessage("'{PropertyName}' can only contain numbers.");
        }
    }
}
