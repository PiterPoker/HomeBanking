using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.UserAggregate
{
    public class UserRole
      : Entity
    {
        private int _userId;
        private User _user;
        private int _roleId;

        public Role GetRole()
        {
            return Role.From(_roleId);
        }

        public User User { get => _user; }
        public Role Role { get => Role.From(_roleId); }

        protected UserRole() { }

        public UserRole(User user, Role role)
        {
            _roleId = role.Id > 0 ? role.Id : throw new HomeBankingDomainException($"Invalid {nameof(role.Id)} ID");
            _user = user != null ? user : throw new HomeBankingDomainException($"Invalid {nameof(user)} ID");
        }
    }
}
