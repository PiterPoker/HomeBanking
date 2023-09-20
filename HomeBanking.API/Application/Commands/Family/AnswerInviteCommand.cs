using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class AnswerInviteCommand : IRequest<FamilyDTO>
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int FamilyId { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        public AnswerInviteCommand() { }

        public AnswerInviteCommand(int userId, int familyId,int statusId)
        {
            UserId = userId;
            FamilyId = familyId;
            StatusId = statusId;
        }
    }
}
