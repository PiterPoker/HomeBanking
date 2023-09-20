using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public class Relative
        : Entity
    {
        private int _userId;
        private int _familyId;
        private Family _family;
        private int _relationshipId;

        public int UserId { get => _userId; }
        public Family Family { get => _family; }
        public Relationship Relationship { get => Relationship.From(_relationshipId); }

        protected Relative()
        {
            _relationshipId = Relationship.Parent.Id;
        }

        public Relationship GetRelationship()
        {
            return Relationship.From(_relationshipId);
        }

        public Relative(Family family, int userId, Relationship relationship) : this()
        {
            _family = family != null ? family : throw new HomeBankingDomainException($"Invalid {nameof(family)} ID");
            _userId = userId > 0 ? userId : throw new HomeBankingDomainException($"Invalid {nameof(userId)} ID");
            _relationshipId = relationship.Id > 0 ? relationship.Id : throw new HomeBankingDomainException($"Invalid {nameof(relationship)} ID");
        }
    }
}
