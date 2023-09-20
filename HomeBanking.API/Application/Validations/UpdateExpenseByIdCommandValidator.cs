using FluentValidation;
using HomeBanking.API.Application.Commands.Wallet;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class UpdateExpenseByIdCommandValidator : AbstractValidator<UpdateExpenseByIdCommand>
    {
        public UpdateExpenseByIdCommandValidator(ILogger<UpdateExpenseByIdCommandValidator> logger)
        {
            RuleFor(expense => expense.Cost).NotEmpty().Must(BeValidAmount).WithMessage("Amount must be greater than zero");
            RuleFor(expense => expense.ExpenseId).NotEmpty().Must(BeValidId).WithMessage("No ExpenseId found");
            RuleFor(expense => expense.UserId).NotEmpty().Must(BeValidId).WithMessage("No UserId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
        private bool BeValidAmount(decimal? amount) => amount.HasValue ? amount.Value >= 0 : true;
    }
}
