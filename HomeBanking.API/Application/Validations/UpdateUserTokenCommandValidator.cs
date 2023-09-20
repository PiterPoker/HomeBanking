using FluentValidation;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class UpdateUserTokenCommandValidator : AbstractValidator<UpdateUserTokenCommand>
    {
        public UpdateUserTokenCommandValidator(ILogger<UpdateUserTokenCommandValidator> logger)
        {
            RuleFor(user => user.Id).NotEmpty().Must(BeValidId).WithMessage("No id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int? id) => id.HasValue ? id.Value > 0 : true;
    }
}

