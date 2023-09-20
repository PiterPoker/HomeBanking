using HomeBanking.API.Application.Models;
using MediatR;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class TransactionCommand : IRequest<bool>
    {
        public int FromId { get; set; }
        public int FromWalletId { get; set; }
        public int ToId { get; set; }
        public int ToWalletId { get; set; }
        public decimal Amount { get; set; }

        public TransactionCommand() { }
        public TransactionCommand(int id, decimal amount) { }

    }
}
