using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class TransactionCommandValidator : AbstractValidator<TransactionCommand>
    {
        public TransactionCommandValidator(ILogger<TransactionCommandValidator> logger)
        {
            RuleFor(wallet => wallet.Amount).NotEmpty().Must(BeValidAmount).WithMessage("Amount must be greater than zero");
            RuleFor(wallet => wallet.FromWalletId).NotEmpty().Must(BeValidId).WithMessage("No FromWalletId found");
            RuleFor(wallet => wallet.FromId).NotEmpty().Must(BeValidId).WithMessage("No FromId found");
            RuleFor(wallet => wallet.ToWalletId).NotEmpty().Must(BeValidId).WithMessage("No ToWalletId found");
            RuleFor(wallet => wallet.ToId).NotEmpty().Must(BeValidId).WithMessage("No ToId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
        private bool BeValidAmount(decimal amount) => amount > 0;
    }
}
