using HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Models
{
    public class InvitationDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsActual { get; set; }
        public StatusDTO Status { get; set; }
        public RelationshipDTO Relationship { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }

        public static InvitationDTO FromInvitation(Invitation invitation)
        {
            return invitation != null ? new InvitationDTO()
            {
                Id = invitation.Id,
                Comment = invitation.Comment,
                IsActual = invitation.IsActual,
                Status = StatusDTO.FromStatus(invitation.GetStatus()),
                Relationship = RelationshipDTO.FromRelationship(invitation.GetRelationship()),
                FromId = invitation.FromId,
                ToId = invitation.ToId
            } : null;
        }
    }
}
