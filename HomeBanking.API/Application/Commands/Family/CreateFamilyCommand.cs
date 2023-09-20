using HomeBanking.API.Application.Models;
using MediatR;

namespace HomeBanking.API.Application.Commands.Family
{
    public class CreateFamilyCommand : IRequest<FamilyDTO>
    {
        public int UserId { get; set; }
        public int RelationshipId { get; set; }
        public string Name { get; set; }

        public CreateFamilyCommand() { }
        public CreateFamilyCommand(int userId, string name, int relationshipId) 
        {
            UserId = userId;
            Name = name;
            RelationshipId = relationshipId;
        }
    }
}
