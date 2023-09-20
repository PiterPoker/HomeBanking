using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class SendInviteCommandHandler
        : IRequestHandler<SendInviteCommand, FamilyDTO>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public SendInviteCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<FamilyDTO> Handle(SendInviteCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetAsync(command.FamilyId);

            family.SendInvite(command.FromId, command.ToId, command.RelationshipId, command.Comment);

            _familyRepository.Update(family);

            await _familyRepository.UnitOfWork.SaveChangesAsync();

            return FamilyDTO.FromFamily(family);
        }
    }
}
