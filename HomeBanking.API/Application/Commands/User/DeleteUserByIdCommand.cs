using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class DeleteUserByIdCommand : IRequest<bool>
    {
        [DataMember]
        public int Id { get; set; }

        public DeleteUserByIdCommand()
        {

        }
        public DeleteUserByIdCommand(int id)
        {
            Id = id;
        }
    }
}
