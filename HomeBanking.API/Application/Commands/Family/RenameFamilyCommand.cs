using HomeBanking.API.Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class RenameFamilyCommand : IRequest<FamilyDTO>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        public RenameFamilyCommand()
        {

        }
        public RenameFamilyCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
