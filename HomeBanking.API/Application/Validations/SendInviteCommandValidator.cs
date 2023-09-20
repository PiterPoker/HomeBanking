using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class SendInviteCommandValidator : AbstractValidator<SendInviteCommand>
    {
        public SendInviteCommandValidator(ILogger<SendInviteCommandValidator> logger)
        {
            RuleFor(family => family.ToId).NotEmpty().Must(BeValidId).WithMessage("No ToId found");
            RuleFor(family => family.FromId).NotEmpty().Must(BeValidId).WithMessage("No FromId found");
            RuleFor(family => family.RelationshipId).NotEmpty().Must(BeValidId).WithMessage("No RelationshipId found");
            RuleFor(family => family.FamilyId).NotEmpty().Must(BeValidId).WithMessage("No FamilyId found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
