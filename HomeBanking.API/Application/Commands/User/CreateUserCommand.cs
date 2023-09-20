using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class CreateUserCommand : IRequest<UserDTO>
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public DateTime Birthday { get; set; }
        [DataMember]
        public List<string> Roles { get; set; }

        public CreateUserCommand()
        {

        }
        public CreateUserCommand(string login, string password, string name, string phonenumber, DateTime birthday)
        {
            Login = login;
            Password = password;
            Name = name;
            PhoneNumber = phonenumber;
            Birthday = birthday;
        }
    }
}
