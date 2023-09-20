using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public interface IFamilyRepository : IRepository<Family>
    {
        Family Add(Family family);
        Family Update(Family family);
        Task<Family> GetAsync(int familyId);
        Task<IEnumerable<Family>> GetAsync();
        Task DeleteAsync(int familyId);
    }
}
