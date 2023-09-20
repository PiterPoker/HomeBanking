using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserDomain = HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Commands.User
{
    public class GetUserByLoginCommandHandler
        : IRequestHandler<GetUserByLoginCommand, UserDTO>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public GetUserByLoginCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> Handle(GetUserByLoginCommand command, CancellationToken cancellationToken)
        {
            var user = default(UserDomain.User);
            var users = await _userRepository.GetAsync();
            if (string.IsNullOrWhiteSpace(command.Password))
            { 
                user = users.SingleOrDefault(u => u.Login == command.Login);
            }
            else
            {
                user = users.SingleOrDefault(u => u.Login == command.Login && u.Password == command.Password);
            }

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return UserDTO.FromUser(user);
        }
    }
}
