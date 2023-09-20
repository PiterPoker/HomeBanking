using HomeBanking.API.Application.Models;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class UpdateExpenseByIdCommandHandler
        : IRequestHandler<UpdateExpenseByIdCommand, WalletDTO>
    {
        private readonly IWalletRepository _walletRepository;

        public UpdateExpenseByIdCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDTO> Handle(UpdateExpenseByIdCommand command, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetAsync(command.WalletId);

            if (wallet != null)
                throw new HomeBankingDomainException("Wallet not found");

            if (wallet.OwnerId == command.UserId)
                throw new HomeBankingDomainException("The creator cannot edit the cost");

            wallet.ChangeExpense(command.ExpenseId, command.Cost, command.Comment, command.Create, command.CategoryId);

            _walletRepository.Update(wallet);

            await _walletRepository.UnitOfWork.SaveChangesAsync();

            return WalletDTO.FromWallet(wallet);
        }
    }
}
