using HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Models
{
    public class RelativeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RelationshipDTO Relationship { get; set; }

        public static RelativeDTO FromRelative(Relative relative)
        {
            return relative != null ? new RelativeDTO()
            {
                Id = relative.Id,
                UserId = relative.UserId,
                Relationship = RelationshipDTO.FromRelationship(relative.GetRelationship())
            } : null;
        }
    }
}
