using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;
using System;

namespace HomeBanking.API.Application.Validations
{
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseCommandValidator(ILogger<CreateExpenseCommandValidator> logger)
        {
            RuleFor(wallet => wallet.Cost).NotEmpty().Must(BeValidAmount).WithMessage("No Amount found");
            RuleFor(wallet => wallet.CategoryId).NotEmpty().Must(BeValidId).WithMessage("No CategoryId found");
            RuleFor(wallet => wallet.FamilyId).NotEmpty().Must(BeValidId).WithMessage("No FamilyId found");
            RuleFor(wallet => wallet.WalletId).NotEmpty().Must(BeValidId).WithMessage("No WalletId found");
            RuleFor(wallet => wallet.Create).Must(BeValidBirthday).WithMessage("Incorrect date");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
        private bool BeValidAmount(decimal amount) => amount >= 0;

        private bool BeValidBirthday(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value < DateTime.UtcNow : true;
        }
    }
}
