using HomeBanking.API.Application.Models;
using HomeBanking.Domain.Exceptions;
using HomeBanking.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserDomain = HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Commands.User
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public CreateUserCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync();
            var userExists = users.FirstOrDefault(u => u.Login.Equals(command.Login));

            if (userExists != null)
                throw new HomeBankingDomainException($"User already exists");

            var user = UserDomain.User.NewUser(command.Login, command.Password);

            user.SetNewProfile(command.Name, command.Birthday, command.PhoneNumber);

            foreach (var role in command.Roles)
            {
                user.AddRole(role);
            }

            _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return UserDTO.FromUser(user);
        }
    }

    // Use for Idempotency in Command process
    public class CreateUserIdenfifiedCommandHandler : IdentifiedCommandHandler<CreateUserCommand, UserDTO>
    {
        public CreateUserIdenfifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateUserCommand, UserDTO>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override UserDTO CreateResultForDuplicateRequest()
        {
            return default(UserDTO);                // Ignore duplicate requests for processing order.
        }
    }
}
