using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteWalletByIdCommandValidator : AbstractValidator<DeleteWalletByIdCommand>
    {
        public DeleteWalletByIdCommandValidator(ILogger<DeleteWalletByIdCommandValidator> logger)
        {
            RuleFor(wallet => wallet.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
