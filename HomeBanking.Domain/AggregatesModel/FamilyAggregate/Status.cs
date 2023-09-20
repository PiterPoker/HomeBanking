using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public class Status
        : Enumeration
    {
        public static Status Accepted = new Status(1, nameof(Accepted).ToLowerInvariant());
        public static Status Rejected = new Status(2, nameof(Rejected).ToLowerInvariant());
        public static Status Approval = new Status(3, nameof(Approval).ToLowerInvariant());

        public Status(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Status> List() =>
            new[] { Accepted, Rejected, Approval };

        public static Status FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new HomeBankingDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Status From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new HomeBankingDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
