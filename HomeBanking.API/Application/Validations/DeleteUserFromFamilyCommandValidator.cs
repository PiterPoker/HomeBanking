using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteUserFromFamilyCommandValidator : AbstractValidator<DeleteUserFromFamilyCommand>
    {
        public DeleteUserFromFamilyCommandValidator(ILogger<DeleteUserFromFamilyCommandValidator> logger)
        {
            RuleFor(family => family.UserId).NotEmpty().Must(BeValidId).WithMessage("No UserId found");
            RuleFor(family => family.Id).NotEmpty().Must(BeValidId).WithMessage("No FamilyId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
