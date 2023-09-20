using HomeBanking.API.Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Category
{
    [DataContract]
    public class UpdateCategotyCommand : IRequest<CategoryDTO>
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ColorId { get; set; }
        public UpdateCategotyCommand(string name, int colorId) 
        {
            Name = name;
            ColorId = colorId;
        }
    }
}
