using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using HomeBanking.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Infrastructure.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly HomeBankingContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public FamilyRepository(HomeBankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Family Add(Family family)
        {
            if (family.IsTransient())
            {
                return _context.Families
                    .Add(family)
                    .Entity;
            }
            else
            {
                return family;
            }
        }

        public async Task DeleteAsync(int familyId)
        {
            var family = await _context.Families
                .Include(b => b.Invitations)
                .Include(b => b.Relatives)
                .Where(c => c.Id == familyId)
                .SingleOrDefaultAsync();

            _context.Families.Remove(family);
        }

        public async Task<Family> GetAsync(int familyId)
        {
            var family = await _context.Families
                .Include(b => b.Invitations)
                .Include(b => b.Relatives)
                .Where(b => b.Id == familyId)
                .SingleOrDefaultAsync();

            return family;
        }

        public Family Update(Family family)
        {
            return _context.Families
                    .Update(family)
                    .Entity;
        }

        public async Task<IEnumerable<Family>> GetAsync()
        {
            var families = await _context.Families
                .Include(f => f.Invitations)
                .Include(f => f.Relatives)
                .ToListAsync();

            return families;
        }
    }
}
