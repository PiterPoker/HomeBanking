using HomeBanking.Domain.AggregatesModel.UserAggregate;

namespace HomeBanking.API.Application.Models
{
    public class UserRoleDTO
    {
        public RoleDTO Role { get; set; }

        public static UserRoleDTO FromUserRole(UserRole userRole)
        {
            return userRole != null ? new UserRoleDTO()
            {
                Role = RoleDTO.FromRole(userRole.GetRole())
            } : null;
        }
    }
}
