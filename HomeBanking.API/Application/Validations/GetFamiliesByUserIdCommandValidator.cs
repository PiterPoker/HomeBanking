using FluentValidation;
using HomeBanking.API.Application.Commands.Family;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class GetFamiliesByUserIdCommandValidator : AbstractValidator<GetFamiliesByUserIdCommand>
    {
        public GetFamiliesByUserIdCommandValidator(ILogger<GetFamiliesByUserIdCommandValidator> logger)
        {
            RuleFor(family => family.UserId).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
