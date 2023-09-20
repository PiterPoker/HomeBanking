using HomeBanking.Domain.AggregatesModel.UserAggregate;
using System;

namespace HomeBanking.API.Application.Models
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }

        public static ProfileDTO FromProfile(Profile profile)
        {
            return profile != null ? new ProfileDTO()
            {
                Id = profile.Id,
                PhoneNumber = profile.PhoneNumber,
                Birthday = profile.Birthday,
                Name = profile.Name
            } : null;
        }
    }
}
