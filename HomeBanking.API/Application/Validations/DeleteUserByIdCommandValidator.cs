using FluentValidation;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
    {
        public DeleteUserByIdCommandValidator(ILogger<DeleteUserByIdCommandValidator> logger)
        {
            RuleFor(category => category.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
