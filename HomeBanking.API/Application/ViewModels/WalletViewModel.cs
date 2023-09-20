namespace HomeBanking.API.Application.ViewModels
{
    public class WalletViewModel
    {
        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public int OwnerId { get; set; }

        public WalletViewModel() { }
        public WalletViewModel(int ownerId, int currencyId, decimal amount) 
        {
            OwnerId = ownerId;
            CurrencyId = currencyId;
            Amount = amount;
        }

    }
}
