using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public class Relationship
        : Enumeration
    {
        public static Relationship Parent = new Relationship(1, nameof(Parent).ToLowerInvariant());
        public static Relationship Child = new Relationship(2, nameof(Child).ToLowerInvariant());

        public Relationship(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Relationship> List() =>
            new[] { Parent, Child };

        public static Relationship FromName(string name)
        {
            var relationship = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (relationship == null)
            {
                throw new HomeBankingDomainException($"Possible values for Relationship: {String.Join(",", List().Select(s => s.Name))}");
            }

            return relationship;
        }

        public static Relationship From(int id)
        {
            var relationship = List().SingleOrDefault(s => s.Id == id);

            if (relationship == null)
            {
                throw new HomeBankingDomainException($"Possible values for Relationship: {String.Join(",", List().Select(s => s.Name))}");
            }

            return relationship;
        }
    }
}
