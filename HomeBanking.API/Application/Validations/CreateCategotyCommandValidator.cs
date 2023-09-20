using FluentValidation;
using HomeBanking.API.Application.Commands;
using HomeBanking.API.Application.Commands.Category;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class CreateCategotyCommandValidator : AbstractValidator<CreateCategotyCommand>
    {
        public CreateCategotyCommandValidator(ILogger<CreateCategotyCommandValidator> logger)
        {
            RuleFor(category => category.Name).NotEmpty().WithMessage("No name found");
            RuleFor(category => category.ColorId).NotEmpty().Must(BeValidId).WithMessage("No color id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
