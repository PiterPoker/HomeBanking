using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using System.Collections.Generic;
using System.Linq;

namespace HomeBanking.API.Application.Models
{
    public class WalletDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public CurrencyDTO Currency { get; set; }

        public int OwnerId { get; set; }

        public IEnumerable<ExpenseDTO> Expenses { get; set; }

        public static WalletDTO FromWallet(Wallet wallet)
        {
            return wallet != null ? new WalletDTO()
            {
                Id = wallet.Id,
                Amount = wallet.Amount,
                Currency = CurrencyDTO.FromCurrency(wallet.Currency),
                OwnerId = wallet.OwnerId,
                Expenses = wallet.Expenses.Select(w=>ExpenseDTO.FromExpense(w))
            } : null;
        }
    }
}
