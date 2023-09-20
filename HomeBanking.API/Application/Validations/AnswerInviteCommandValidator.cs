using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class AnswerInviteCommandValidator : AbstractValidator<AnswerInviteCommand>
    {
        public AnswerInviteCommandValidator(ILogger<AnswerInviteCommandValidator> logger)
        {
            RuleFor(family => family.UserId).NotEmpty().Must(BeValidId).WithMessage("No UserId found");
            RuleFor(family => family.FamilyId).NotEmpty().Must(BeValidId).WithMessage("No FamilyId found");
            RuleFor(command => command.StatusId).NotEmpty();

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
