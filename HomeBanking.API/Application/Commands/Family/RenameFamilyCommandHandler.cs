using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class RenameFamilyCommandHandler
        : IRequestHandler<RenameFamilyCommand, FamilyDTO>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public RenameFamilyCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<FamilyDTO> Handle(RenameFamilyCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetAsync(command.Id);

            family.SetName(command.Name);

            _familyRepository.Update(family);

            await _familyRepository.UnitOfWork.SaveEntitiesAsync();

            return FamilyDTO.FromFamily(family);
        }
    }
}
