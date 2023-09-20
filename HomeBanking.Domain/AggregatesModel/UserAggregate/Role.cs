using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.UserAggregate
{
    public class Role
        : Enumeration
    {
        public static Role Admin = new Role(1, nameof(Admin).ToLowerInvariant());
        public static Role User = new Role(2, nameof(User).ToLowerInvariant());

        public Role(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Role> List() =>
            new[] { Admin, User };

        public static Role FromName(string name)
        {
            var role = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (role == null)
            {
                throw new HomeBankingDomainException($"Possible values for Role: {String.Join(",", List().Select(s => s.Name))}");
            }

            return role;
        }

        public static Role From(int id)
        {
            var role = List().SingleOrDefault(s => s.Id == id);

            if (role == null)
            {
                throw new HomeBankingDomainException($"Possible values for Role: {String.Join(",", List().Select(s => s.Name))}");
            }

            return role;
        }
    }
}
