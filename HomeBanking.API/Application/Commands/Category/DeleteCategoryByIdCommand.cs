using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Category
{
    [DataContract]
    public class DeleteCategoryByIdCommand : IRequest<bool>
    {

        [DataMember]
        public int Id { get; set; }

        public DeleteCategoryByIdCommand()
        {

        }

        public DeleteCategoryByIdCommand(int id)
        {
            Id = id;
        }
    }
}
