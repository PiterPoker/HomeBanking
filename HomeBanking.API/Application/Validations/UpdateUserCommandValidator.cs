using FluentValidation;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;
using System;

namespace HomeBanking.API.Application.Validations
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(ILogger<UpdateUserCommandValidator> logger)
        {
            RuleFor(user => user.Id).NotEmpty().Must(BeValidId).WithMessage("No id found");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidId(int id) => id > 0;
    }
}
