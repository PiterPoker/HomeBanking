using HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Models
{
    public class ColorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ColorDTO FromColor(Color color)
        {
            return color != null ? new ColorDTO() 
            {
                Id = color.Id,
                Name = color.Name
            } : null;
        }
    }
}
