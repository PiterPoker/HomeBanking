using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteExpenseByIdCommandValidator : AbstractValidator<DeleteExpenseByIdCommand>
    {
        public DeleteExpenseByIdCommandValidator(ILogger<DeleteExpenseByIdCommandValidator> logger)
        {
            RuleFor(wallet => wallet.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");
            RuleFor(wallet => wallet.UserId).NotEmpty().Must(BeValidId).WithMessage("No UserId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
