using HomeBanking.Domain.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class PasswordChangedDomainEvent
        : INotification
    {
        public User User { get; }

        public PasswordChangedDomainEvent(User user) => User = user;
    }
}
