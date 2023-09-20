using FluentValidation;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
    {
        public GetUserByIdCommandValidator(ILogger<GetUserByIdCommandValidator> logger)
        {
            RuleFor(category => category.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
