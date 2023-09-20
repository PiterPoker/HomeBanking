using HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Models
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static StatusDTO FromStatus(Status status)
        {
            return status != null ? new StatusDTO()
            {
                Id = status.Id,
                Name = status.Name
            } : null;
        }
    }
}
