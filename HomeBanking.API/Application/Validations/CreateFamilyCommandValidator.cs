using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator(ILogger<CreateFamilyCommandValidator> logger)
        {
            RuleFor(family => family.Name).NotEmpty().WithMessage("Name invalid");
            RuleFor(family => family.UserId).NotEmpty().Must(BeValidId).WithMessage("User id invalid");
            RuleFor(family => family.RelationshipId).NotEmpty().Must(BeValidId).WithMessage("User id invalid");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;

    }
}
