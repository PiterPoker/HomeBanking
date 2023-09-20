using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class DeleteFamilyByIdCommandValidator : AbstractValidator<DeleteFamilyByIdCommand>
    {
        public DeleteFamilyByIdCommandValidator(ILogger<DeleteFamilyByIdCommandValidator> logger)
        {
            RuleFor(family => family.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
