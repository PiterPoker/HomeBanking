using HomeBanking.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBanking.API.Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime Update { get; set; }
        public ProfileDTO Profile { get; set; }
        public IEnumerable<UserRoleDTO> UserRoles { get; set; }

        public static UserDTO FromUser(User user)
        {
            return user != null ? new UserDTO()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Update = user.Update,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                Profile = ProfileDTO.FromProfile(user.Profile),
                UserRoles = user.UserRoles.Select(ur => UserRoleDTO.FromUserRole(ur))
            } : null;
        }
    }
}
