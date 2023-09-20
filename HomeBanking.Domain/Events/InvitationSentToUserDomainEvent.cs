using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class InvitationSentToUserDomainEvent
         : INotification
    {
        public Invitation Invitation { get; set; }

        public InvitationSentToUserDomainEvent(Invitation invitation) 
        {
            Invitation = invitation;
        }
    }
}
