using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class DeleteInviteCommand : IRequest<bool>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int InvateId { get; set; }

        public DeleteInviteCommand()
        {

        }
        public DeleteInviteCommand(int id, int invateId)
        {
            Id = id;
            InvateId = invateId;
        }
    }
}
