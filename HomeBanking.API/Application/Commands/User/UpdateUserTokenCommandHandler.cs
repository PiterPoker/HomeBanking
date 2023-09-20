using HomeBanking.API.Application.Models;
using HomeBanking.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserDomain = HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Commands.User
{
    public class UpdateUserTokenCommandHandler
        : IRequestHandler<UpdateUserTokenCommand, UserDTO>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public UpdateUserTokenCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> Handle(UpdateUserTokenCommand command, CancellationToken cancellationToken)
        {
            var users = new List<UserDomain.User>();
            var userDTO = default(UserDTO);

            if (command.Id.HasValue)
            {
                var user = await _userRepository.GetAsync(command.Id.Value);

                if (user != null)
                {
                    users.Add(user);
                    userDTO = UserDTO.FromUser(user);
                }
                else
                {
                    throw new HomeBankingDomainException($"User ID = {command.Id} not found");
                }
            }
            else
            {
                users.AddRange(await _userRepository.GetAsync());
            }

            foreach (var user in users)
            {
                if (command.RefreshTokenExpiryTime.HasValue)
                    user.SetRefreshTokenExpiryTime(command.RefreshTokenExpiryTime.Value);
                else
                    user.SetRefreshTokenExpiryTime(DateTime.UtcNow);

                user.SetRefreshToken(command.RefreshToken);

                _userRepository.Update(user);

            }

            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return userDTO;
        }
    }
}
