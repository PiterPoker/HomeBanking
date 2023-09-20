using HomeBanking.Domain.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class UserAndProfileVerifiedDomainEvent
        : INotification
    {
        public Profile Profile { get; private set; }

        public UserAndProfileVerifiedDomainEvent(Profile profile) 
        {
            Profile = profile;
        }
    }
}
