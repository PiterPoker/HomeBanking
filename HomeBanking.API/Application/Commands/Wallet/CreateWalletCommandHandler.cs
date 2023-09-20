using HomeBanking.API.Application.Models;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WalletDomain = HomeBanking.Domain.AggregatesModel.WalletAggregate;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class CreateWalletCommandHandler
        : IRequestHandler<CreateWalletCommand, WalletDTO>
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWalletCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDTO> Handle(CreateWalletCommand command, CancellationToken cancellationToken)
        {
            var wallet = WalletDomain.Wallet.NewWallet();
            wallet.SetOwner(command.Owner.Id);
            wallet.SetCurrency(command.CurrencyId);
            wallet.SetAmount(command.Amount);

            _walletRepository.Add(wallet);

            await _walletRepository.UnitOfWork.SaveEntitiesAsync();

            return WalletDTO.FromWallet(wallet);
        }
    }
}
