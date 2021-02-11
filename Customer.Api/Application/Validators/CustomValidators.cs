// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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
        /// <typeparam name="T">The type to be validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
        /// <returns>The rule builder.</returns>
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
        /// <typeparam name="T">The type to be validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
        /// <returns>The rule builder.</returns>
        public static IRuleBuilderOptions<T, string> NumbersOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(p => p.ToCharArray().All(Char.IsDigit))
                .WithMessage("'{PropertyName}' can only contain numbers.");
        }
    }
}
