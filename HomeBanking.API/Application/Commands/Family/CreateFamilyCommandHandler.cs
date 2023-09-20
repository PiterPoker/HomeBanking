using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class CreateFamilyCommandHandler
        : IRequestHandler<CreateFamilyCommand, FamilyDTO>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public CreateFamilyCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<FamilyDTO> Handle(CreateFamilyCommand command, CancellationToken cancellationToken)
        {
            var family = FamilyDomain.Family.NewFamily();
            family.SetName(command.Name);
            family.AddRelative(command.UserId, command.RelationshipId);
            _familyRepository.Add(family);

            await _familyRepository.UnitOfWork.SaveEntitiesAsync();

            return FamilyDTO.FromFamily(family);
        }
    }
}
