using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteInviteCommandValidator : AbstractValidator<DeleteInviteCommand>
    {
        public DeleteInviteCommandValidator(ILogger<DeleteInviteCommandValidator> logger)
        {
            RuleFor(family => family.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");
            RuleFor(family => family.InvateId).NotEmpty().Must(BeValidId).WithMessage("No Invate Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
