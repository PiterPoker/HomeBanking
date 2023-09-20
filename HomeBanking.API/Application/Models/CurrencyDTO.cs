using HomeBanking.Domain.AggregatesModel.WalletAggregate;

namespace HomeBanking.API.Application.Models
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CurrencyDTO FromCurrency(Currency currency)
        {
            return currency != null ? new CurrencyDTO()
            {
                Id = currency.Id,
                Name = currency.Name
            } : null;
        }
    }
}
