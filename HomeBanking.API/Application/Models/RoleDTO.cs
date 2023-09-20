using HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Models
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static RoleDTO FromRole(Role role)
        {
            return role != null ? new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name
            } : null;
        }
    }
}
