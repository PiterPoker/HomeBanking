using HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColorDTO Color { get; set; }

        public static CategoryDTO FromCategory(Category category)
        {
            return category != null ? new CategoryDTO()
            {
                Color = ColorDTO.FromColor(category.Color),
                Name = category.Name,
                Id = category.Id,
            } : null;
        }
    }
}
