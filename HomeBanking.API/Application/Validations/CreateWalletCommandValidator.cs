using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator(ILogger<CreateWalletCommandValidator> logger)
        {
            RuleFor(wallet => wallet.Amount).Must(BeValidAmount).WithMessage("No Amount found");
            RuleFor(wallet => wallet.CurrencyId).NotEmpty().Must(BeValidId).WithMessage("No CurrencyId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
        private bool BeValidAmount(decimal amount) => amount >= 0;
    }
}
