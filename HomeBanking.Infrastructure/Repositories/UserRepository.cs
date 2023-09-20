using HomeBanking.Domain.AggregatesModel.UserAggregate;
using HomeBanking.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeBankingContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public UserRepository(HomeBankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Add(User user)
        {
            if (user.IsTransient())
            {
                return _context.Users
                    .Add(user)
                    .Entity;
            }
            else
            {
                return user;
            }
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.Profile)
                .Where(c => c.Id == userId)
                .SingleOrDefaultAsync();

            _context.Users.Remove(user);
        }

        public async Task<User> GetAsync(int userId)
        {
            var user = await _context.Users
                .Include(b => b.Profile)
                .Include(u => u.UserRoles)
                .Where(b => b.Id == userId)
                .SingleOrDefaultAsync();

            return user;
        }

        public User Update(User user)
        {
            return _context.Users
                    .Update(user)
                    .Entity;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var users = await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.UserRoles)
                .ToListAsync();

            return users;
        }
    }
}
