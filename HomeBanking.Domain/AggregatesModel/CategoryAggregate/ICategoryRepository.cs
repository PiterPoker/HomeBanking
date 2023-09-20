using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeBanking.Domain.AggregatesModel.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Add(Category category);
        Category Update(Category category);
        Task<Category> GetAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task DeleteAsync(int categoryId);
    }
}
