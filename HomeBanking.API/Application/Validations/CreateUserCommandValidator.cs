using FluentValidation;
using HomeBanking.API.Application.Commands;
using HomeBanking.API.Application.Commands.User;
using Microsoft.Extensions.Logging;
using System;

namespace HomeBanking.API.Application.Validations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(ILogger<CreateUserCommandValidator> logger)
        {
            RuleFor(user => user.Login).NotEmpty().WithMessage("Login is invalid");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is invalid");
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is invalid");
            RuleFor(user => user.Birthday).NotEmpty().Must(BeValidBirthday).WithMessage("Please specify a valid birthday date");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidBirthday(DateTime dateTime)
        {
            return dateTime <= DateTime.UtcNow.AddYears(-6) && dateTime >= DateTime.UtcNow.AddYears(-120);
        }
    }
}
