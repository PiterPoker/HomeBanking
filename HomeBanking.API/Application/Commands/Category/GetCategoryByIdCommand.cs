using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Category
{
    [DataContract]
    public class GetCategoryByIdCommand : IRequest<CategoryDTO>
    {

        [DataMember]
        public int Id { get; set; }

        public GetCategoryByIdCommand()
        {

        }
        public GetCategoryByIdCommand(int id)
        {
            Id = id;
        }
    }
}
