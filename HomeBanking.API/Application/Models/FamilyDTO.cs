using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using System.Collections.Generic;
using System.Linq;

namespace HomeBanking.API.Application.Models
{
    public class FamilyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RelativeDTO> Relatives { get; set; }
        public IEnumerable<InvitationDTO> Invitations { get; set; }

        public static FamilyDTO FromFamily(Family family)
        {
            return family != null ? new FamilyDTO()
            {
                Id = family.Id,
                Name = family.Name,
                Relatives = family.Relatives.Select(r=>RelativeDTO.FromRelative(r)),
                Invitations = family.Invitations.Select(r => InvitationDTO.FromInvitation(r))
            } : null;
        }
    }
}
