using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class DeleteInviteCommandHandler
        : IRequestHandler<DeleteInviteCommand, bool>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public DeleteInviteCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<bool> Handle(DeleteInviteCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetAsync(command.Id);

            if (family == null)
                throw new ArgumentNullException(nameof(family));

            family.DeleteInvitation(command.InvateId);

            _familyRepository.Update(family);

            return await _familyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
