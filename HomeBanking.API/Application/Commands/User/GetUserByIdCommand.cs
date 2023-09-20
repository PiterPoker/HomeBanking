using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class GetUserByIdCommand : IRequest<UserDTO>
    {
        [DataMember]
        public int Id { get; set; }

        public GetUserByIdCommand()
        {

        }
        public GetUserByIdCommand(int id)
        {
            Id = id;
        }
    }
}
