using FluentValidation;
using HomeBanking.API.Application.Commands.Category;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class GetCategoryByIdCommandValidator : AbstractValidator<GetCategoryByIdCommand>
    {
        public GetCategoryByIdCommandValidator(ILogger<GetCategoryByIdCommandValidator> logger)
        {
            RuleFor(category => category.Id).NotEmpty().Must(BeValidId).WithMessage("No Id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
