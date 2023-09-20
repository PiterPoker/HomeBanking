using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class DeleteUserFromFamilyCommand : IRequest<bool>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }

        public DeleteUserFromFamilyCommand()
        {

        }
        public DeleteUserFromFamilyCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
