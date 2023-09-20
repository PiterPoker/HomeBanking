using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public DateTime? Birthday { get; set; }

        public UpdateUserCommand()
        {

        }
        public UpdateUserCommand(int id, string login, string name, string phonenumber, DateTime birthday)
        {
            Id = id;
            Login = login;
            Name = name;
            PhoneNumber = phonenumber;
            Birthday = birthday;
        }
    }
}
