using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Category
{
    [DataContract]
    public class CreateCategotyCommand : IRequest<CategoryDTO>
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ColorId { get; set; }

        public CreateCategotyCommand()
        {

        }
        public CreateCategotyCommand(string name, int colorId) 
        {
            Name = name;
            ColorId = colorId;
        }
    }
}
