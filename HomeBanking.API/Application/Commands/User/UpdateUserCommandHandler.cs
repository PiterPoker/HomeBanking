using HomeBanking.API.Application.Models;
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
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public UpdateUserCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync();
            var user = await _userRepository.GetAsync(command.Id);

            if (user != null)
            {

                if (!users.Any(u => u.Login == command.Login))
                    user.ChangeLogin(command.Login);

                if (user.Profile != null )
                {
                    if (!string.IsNullOrWhiteSpace(command.Name))
                        user.Profile.SetName(command.Name);
                    if (command.Birthday.HasValue)
                        user.Profile.SetBirthday(command.Birthday.Value);

                    user.Profile.SetPhoneNumber(command.PhoneNumber);
                }

                _userRepository.Update(user);

                await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            }

            return UserDTO.FromUser(user);
        }
    }

    // Use for Idempotency in Command process
    public class UpdateUserIdenfifiedCommandHandler : IdentifiedCommandHandler<UpdateUserCommand, UserDTO>
    {
        public UpdateUserIdenfifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<UpdateUserCommand, UserDTO>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override UserDTO CreateResultForDuplicateRequest()
        {
            return default(UserDTO);                // Ignore duplicate requests for processing order.
        }
    }
}
