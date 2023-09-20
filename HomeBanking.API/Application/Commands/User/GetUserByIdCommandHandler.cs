using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserDomain = HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Commands.User
{
    public class GetUserByIdCommandHandler
        : IRequestHandler<GetUserByIdCommand, UserDTO>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public GetUserByIdCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDTO> Handle(GetUserByIdCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.Id);

            return UserDTO.FromUser(user);
        }
    }
}
