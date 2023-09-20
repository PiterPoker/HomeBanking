using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public class Family
      : Entity, IAggregateRoot
    {
        private string _name;
        private readonly List<Relative> _relatives;
        private readonly List<Invitation> _invitations;

        public string Name { get => _name; }
        public IReadOnlyCollection<Relative> Relatives => _relatives;
        public IReadOnlyCollection<Invitation> Invitations => _invitations;

        protected Family()
        {
            _relatives = new List<Relative>();
            _invitations = new List<Invitation>();
        }
        public Family(string name) : this()
        {
            _name = name;
        }

        public void DeleteInvitation(int invateId)
        {
            var invitation = _invitations.SingleOrDefault(i => i.Id == invateId);

            if (invitation == null)
                throw new ArgumentNullException($"{nameof(invitation)}");

            _invitations.Remove(invitation);
        }

        public void SetStatus(int userId, int statusId)
        {
            var invitation = _invitations.SingleOrDefault(i => i.IsActual
            && i.ToId == userId
            && i.EqualsStatus(Status.Approval));
            var status = Status.From(statusId);

            if (invitation == null)
                throw new ArgumentNullException($"{nameof(invitation)}");

            if (status == Status.Accepted)
            {
                invitation.SetAcceptedStatus();

                _relatives.Add(new Relative(this, userId, invitation.GetRelationship()));
            }
            if (status == Status.Rejected)
                invitation.SetRejectedStatus();
        }

        public void AddRelative(int userId, int relationshipId)
        {
            var relative = _relatives.SingleOrDefault(r=>r.UserId == userId);

            if (relative != null)
                throw new HomeBankingDomainException($"User with ID {userId} is already in the family");

            relative = new Relative(this, userId, Relationship.From(relationshipId));

            _relatives.Add(relative);
            //TODO: Notifying a Family Member
        }

        public void DeleteRelative(int userId)
        {
            var relative = _relatives.SingleOrDefault(i => i.UserId == userId);

            if (relative == null)
                throw new ArgumentNullException($"{nameof(relative)}");
            
            var invitations = _invitations.Where(i => i.EqualsUserById(userId));

            _relatives.Remove(relative);
            _invitations.RemoveAll(i=> invitations.Contains(i));
        }

        public static Family NewFamily() => new Family();

        public void SendInvite(int fromId, int toId, int relationshipId, string comment)
        {
            var invitation = _invitations.SingleOrDefault(i => i.IsActual == false
              && i.FromId == fromId
              && i.ToId == toId
              && i.EqualsStatus(Status.Accepted));

            if (invitation != null)
                throw new HomeBankingDomainException("Invitation already accepted");

            invitation = _invitations.SingleOrDefault(i => i.IsActual
              && i.FromId == fromId
              && i.ToId == toId
              && i.EqualsStatus(Status.Accepted));

            if (invitation != null)
                throw new HomeBankingDomainException("Invitation pending");

            _invitations.Add(new Invitation(this, comment, Relationship.From(relationshipId), fromId, toId));
        }

        public void SetName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                _name = name;
        }
    }
}
