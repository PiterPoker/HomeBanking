using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.Events;
using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.FamilyAggregate
{
    public class Invitation
        : Entity
    {
        private string _comment;
        private bool _isActual;
        private int _statusId;
        private int _relationshipId;
        private int _fromId;
        private int _toId;
        private int _familyId;
        private Family _family;

        public string Comment { get => _comment; set => _comment = value; }
        public bool IsActual { get => _isActual; set => _isActual = value; }

        public Status GetStatus()
        {
            return Status.From(_statusId);
        }
        public int FromId { get => _fromId; private set => _fromId = value; }

        public bool EqualsStatus(Status approval)
        {
            return _statusId == approval.Id;
        }

        public int ToId { get => _toId; private set => _toId = value; }

        protected Invitation()
        {
            _isActual = true;
            _statusId = Status.Approval.Id;
        }

        public Relationship GetRelationship()
        {
            return Relationship.From(_relationshipId);
        }

        public Invitation(Family family, string comment, Relationship relationship, int fromId, int toId) : this()
        {
            _comment = comment;

            _family = family != null ? family : throw new HomeBankingDomainException($"Invalid {nameof(family)} ID");
            _relationshipId = relationship.Id > 0 ? relationship.Id : throw new HomeBankingDomainException($"Invalid {nameof(relationship)} ID");
            _fromId = fromId > 0 ? fromId : throw new HomeBankingDomainException($"Invalid {nameof(fromId)} ID");
            _toId = toId > 0 ? toId : throw new HomeBankingDomainException($"Invalid {nameof(toId)} ID");
            AddDomainEvent(new InvitationSentToUserDomainEvent(this));
        }

        public void ReadInvitation()
        {
            if (_isActual)
            {
                AddDomainEvent(new ReadInvitationDomainEvent(this));

                _isActual = false;
            }
        }

        public void SetAcceptedStatus()
        {
            if (_statusId == Status.Approval.Id)
            {
                AddDomainEvent(new InvitationApprovalDomainEvent(this));

                _isActual = false;
                _statusId = Status.Accepted.Id;
            }
        }

        public void SetRejectedStatus()
        {
            if (_statusId == Status.Approval.Id)
            {
                AddDomainEvent(new InvitationRejectedDomainEvent(this));

                _isActual = false;
                _statusId = Status.Rejected.Id;
            }
        }

        public bool EqualsUserById(int userId)
        {
            return _toId == userId;
        }
    }
}
