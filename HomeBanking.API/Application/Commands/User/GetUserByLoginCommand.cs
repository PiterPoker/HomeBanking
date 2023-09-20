using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class GetUserByLoginCommand : IRequest<UserDTO>
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }

        public GetUserByLoginCommand()
        {

        }
        public GetUserByLoginCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
