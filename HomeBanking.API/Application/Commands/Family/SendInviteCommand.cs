using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class SendInviteCommand : IRequest<FamilyDTO>
    {
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public int RelationshipId { get; set; }
        [DataMember]
        public int FromId { get; set; }
        [DataMember]
        public int ToId { get; set; }
        [DataMember]
        public int FamilyId { get; set; }

        public SendInviteCommand() { }

        public SendInviteCommand(int familyId, int fromId, int toId, string comment, int relationshipId) 
        {
            FamilyId = familyId;
            FromId = fromId;
            ToId = toId;
            Comment = comment;
            RelationshipId = relationshipId;
        }
    }
}
