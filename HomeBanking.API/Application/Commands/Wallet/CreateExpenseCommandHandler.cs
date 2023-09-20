using HomeBanking.API.Application.Models;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WalletDomain = HomeBanking.Domain.AggregatesModel.WalletAggregate;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class CreateExpenseCommandHandler
        : IRequestHandler<CreateExpenseCommand, WalletDTO>
    {
        private readonly IWalletRepository _walletRepository;

        public CreateExpenseCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDTO> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetAsync(command.WalletId);
            if (wallet != null)
            {
                var create = command.Create.HasValue ? command.Create.Value : System.DateTime.UtcNow;
                wallet.AddExpense(command.Cost,command.Comment, command.CategoryId, command.FamilyId, create);

                _walletRepository.Update(wallet);

                await _walletRepository.UnitOfWork.SaveEntitiesAsync();
            }

            return WalletDTO.FromWallet(wallet);
        }

    }
}
