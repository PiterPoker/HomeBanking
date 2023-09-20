using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.WalletAggregate
{
    public class Currency
        : Enumeration
    {

        public static Currency USD = new Currency(1, nameof(USD).ToLowerInvariant());
        public static Currency RUB = new Currency(2, nameof(RUB).ToLowerInvariant());
        public static Currency EUR = new Currency(3, nameof(EUR).ToLowerInvariant());
        public static Currency BYN = new Currency(4, nameof(BYN).ToLowerInvariant());

        public Currency(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Currency> List() =>
            new[] { USD, RUB, EUR, BYN };

        public static Currency FromName(string name)
        {
            var currency = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (currency == null)
            {
                throw new HomeBankingDomainException($"Possible values for Currency: {String.Join(",", List().Select(s => s.Name))}");
            }

            return currency;
        }

        public static Currency From(int id)
        {
            var currency = List().SingleOrDefault(s => s.Id == id);

            if (currency == null)
            {
                throw new HomeBankingDomainException($"Possible values for Currency: {String.Join(",", List().Select(s => s.Name))}");
            }

            return currency;
        }
    }
}
