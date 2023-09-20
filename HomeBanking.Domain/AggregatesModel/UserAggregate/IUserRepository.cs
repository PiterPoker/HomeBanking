using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User user);
        User Update(User user);
        Task<User> GetAsync(int userId);
        Task<IEnumerable<User>> GetAsync();
        Task DeleteAsync(int userId);
    }
}
