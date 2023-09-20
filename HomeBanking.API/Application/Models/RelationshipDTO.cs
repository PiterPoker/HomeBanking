using HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Models
{
    public class RelationshipDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static RelationshipDTO FromRelationship(Relationship relationship)
        {
            return relationship != null ? new RelationshipDTO()
            {
                Id = relationship.Id,
                Name = relationship.Name
            } : null;
        }
    }
}
