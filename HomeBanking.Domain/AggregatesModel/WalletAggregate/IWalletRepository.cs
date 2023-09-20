using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Domain.AggregatesModel.WalletAggregate
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet Add(Wallet wallet);
        Wallet Update(Wallet wallet);
        Task<Wallet> GetAsync(int walletId);
        Task<IEnumerable<Wallet>> GetAsync();
        Task DeleteAsync(int walletId);
    }
}
