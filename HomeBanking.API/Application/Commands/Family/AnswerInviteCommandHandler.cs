using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class AnswerInviteCommandHandler
        : IRequestHandler<AnswerInviteCommand, FamilyDTO>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public AnswerInviteCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<FamilyDTO> Handle(AnswerInviteCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetAsync(command.FamilyId);

            family.SetStatus(command.UserId, command.StatusId);

            _familyRepository.Update(family);

            await _familyRepository.UnitOfWork.SaveEntitiesAsync();

            return FamilyDTO.FromFamily(family);
        }
    }
}
