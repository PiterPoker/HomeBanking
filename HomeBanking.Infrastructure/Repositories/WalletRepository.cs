using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly HomeBankingContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public WalletRepository(HomeBankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public Wallet Add(Wallet wallet)
        {
            if (wallet.IsTransient())
            {
                return _context.Wallets
                    .Add(wallet)
                    .Entity;
            }
            else
            {
                return wallet;
            }
        }

        public async Task DeleteAsync(int walletId)
        {
            var wallet = await _context.Wallets
                .Where(c => c.Id == walletId)
                .SingleOrDefaultAsync();

            _context.Wallets.Remove(wallet);
        }

        public async Task<Wallet> GetAsync(int walletId)
        {
            var wallet = await _context.Wallets
                .Include(b => b.Expenses)
                .Where(b => b.Id == walletId)
                .SingleOrDefaultAsync();

            return wallet;
        }

        public async Task<IEnumerable<Wallet>> GetAsync()
        {
            var wallets = await _context.Wallets
                .Include(b => b.Expenses)
                .ToListAsync();

            return wallets;
        }

        public Wallet Update(Wallet wallet)
        {
            return _context.Wallets
                    .Update(wallet)
                    .Entity;
        }
    }
}
