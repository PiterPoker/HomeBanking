using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserDomain = HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Commands.User
{
    public class DeleteUserByIdCommandHandler
        : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly UserDomain.IUserRepository _userRepository;

        public DeleteUserByIdCommandHandler(UserDomain.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(command.Id);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
