using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.User
{
    [DataContract]
    public class UpdateUserTokenCommand : IRequest<UserDTO>
    {
        public UpdateUserTokenCommand() { }
        public UpdateUserTokenCommand(int? id, string refreshToken, DateTime? dateTime)
        {
            Id = id;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = dateTime;
        }

        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string RefreshToken { get; set; }
        [DataMember]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
