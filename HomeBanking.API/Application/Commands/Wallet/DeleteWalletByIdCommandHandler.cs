using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class DeleteWalletByIdCommandHandler
        : IRequestHandler<DeleteWalletByIdCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;

        public DeleteWalletByIdCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<bool> Handle(DeleteWalletByIdCommand command, CancellationToken cancellationToken)
        {
            await _walletRepository.DeleteAsync(command.Id);
            
            return await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
