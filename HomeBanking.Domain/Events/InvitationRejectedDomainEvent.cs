using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class InvitationRejectedDomainEvent
         : INotification
    {
        public Invitation Invitation { get; set; }

        public InvitationRejectedDomainEvent(Invitation invitation) 
        {
            Invitation = invitation;
        }
    }
}
