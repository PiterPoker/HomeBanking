using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class DeleteExpenseByIdCommandHandler
        : IRequestHandler<DeleteExpenseByIdCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;

        public DeleteExpenseByIdCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<bool> Handle(DeleteExpenseByIdCommand command, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetAsync(command.Id);

            if (wallet != null)
                throw new HomeBankingDomainException("Wallet not found");

            if (wallet.OwnerId == command.UserId)
                throw new HomeBankingDomainException("The creator cannot edit the cost");

            await _walletRepository.DeleteAsync(wallet.Id);

            return await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
