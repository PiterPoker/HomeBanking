using FluentValidation;
using HomeBanking.API.Application.Commands.Category;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class UpdateCategotyCommandValidator : AbstractValidator<UpdateCategotyCommand>
    {
        public UpdateCategotyCommandValidator(ILogger<UpdateCategotyCommandValidator> logger)
        {
            RuleFor(category => category.Id).NotEmpty().Must(BeValidId).WithMessage("No id found");
            RuleFor(category => category.Name).NotEmpty().WithMessage("No name found");
            RuleFor(category => category.ColorId).NotEmpty().Must(BeValidId).WithMessage("No color id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
