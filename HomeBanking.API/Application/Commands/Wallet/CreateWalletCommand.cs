using HomeBanking.API.Application.Models;
using MediatR;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class CreateWalletCommand : IRequest<WalletDTO>
    {
        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public UserDTO Owner { get; set; }

        public CreateWalletCommand() { }
        public CreateWalletCommand(UserDTO owner, int currencyId, decimal amount)
        {
            Owner = owner;
            CurrencyId = currencyId;
            Amount = amount;
        }
    }
}
