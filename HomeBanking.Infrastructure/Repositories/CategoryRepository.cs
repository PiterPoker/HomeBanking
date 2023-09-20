using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using HomeBanking.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Infrastructure.Repositories
{
    public class CategoryRepository
        : ICategoryRepository
    {
        private readonly HomeBankingContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public CategoryRepository(HomeBankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category Add(Category category)
        {
            if (category.IsTransient())
            {
                return _context.Categories
                    .Add(category)
                    .Entity;
            }
            else
            {
                return category;
            }
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _context.Categories
                .Where(c => c.Id == categoryId)
                .SingleOrDefaultAsync();

            _context.Categories.Remove(category);
        }

        public async Task<Category> GetAsync(int categoryId)
        {
            var category = await _context.Categories
                .Where(b => b.Id == categoryId)
                .SingleOrDefaultAsync();

            return category;
        }

        public Category Update(Category category)
        {
            return _context.Categories
                    .Update(category)
                    .Entity;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }
    }
}
