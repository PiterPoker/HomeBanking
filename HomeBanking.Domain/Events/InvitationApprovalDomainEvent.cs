using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class InvitationApprovalDomainEvent
         : INotification
    {
        public Invitation Invitation { get; set; }

        public InvitationApprovalDomainEvent(Invitation invitation) 
        {
            Invitation = invitation;
        }
    }
}
