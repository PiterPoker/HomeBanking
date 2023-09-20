using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class DeleteFamilyByIdCommand : IRequest<bool>
    {
        [DataMember]
        public int Id { get; set; }

        public DeleteFamilyByIdCommand()
        {

        }
        public DeleteFamilyByIdCommand(int id)
        {
            Id = id;
        }
    }
}
