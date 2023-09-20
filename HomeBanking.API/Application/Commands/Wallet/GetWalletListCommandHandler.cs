using HomeBanking.API.Application.Commands.Wallet;
using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class GetWalletListCommandHandler
        : IRequestHandler<GetWalletListCommand, PaginatedItemsViewModel<WalletDTO>>
    {
        private readonly IWalletRepository _walletRepository;

        public GetWalletListCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<PaginatedItemsViewModel<WalletDTO>> Handle(GetWalletListCommand command, CancellationToken cancellationToken)
        {
            var wallets = await _walletRepository.GetAsync();
            var walletByUser = wallets.Where(w => w.OwnerId == command.UserId);
            var paginationWallets = walletByUser.Select(c => WalletDTO.FromWallet(c))
                .Skip(command.PageSize * command.PageIndex)
                .Take(command.PageSize);

            return new PaginatedItemsViewModel<WalletDTO>(command.PageIndex, command.PageSize, paginationWallets.Count(), paginationWallets);
        }
    }
}
