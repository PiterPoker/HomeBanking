using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class GetWalletListCommandValidator : AbstractValidator<GetWalletListCommand>
    {
        public GetWalletListCommandValidator(ILogger<GetWalletListCommandValidator> logger)
        {
            RuleFor(wallet => wallet.UserId).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
