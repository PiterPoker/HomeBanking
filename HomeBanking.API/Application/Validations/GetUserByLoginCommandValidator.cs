using FluentValidation;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;

namespace HomeBanking.API.Application.Validations
{
    public class GetUserByLoginCommandValidator : AbstractValidator<GetUserByLoginCommand>
    {
        public GetUserByLoginCommandValidator(ILogger<GetUserByLoginCommandValidator> logger)
        {
            RuleFor(category => category.Login).NotEmpty().WithMessage("No Login found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
