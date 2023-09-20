using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class DeleteFamilyByIdCommandHandler
        : IRequestHandler<DeleteFamilyByIdCommand, bool>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public DeleteFamilyByIdCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<bool> Handle(DeleteFamilyByIdCommand command, CancellationToken cancellationToken)
        {
            await _familyRepository.DeleteAsync(command.Id);

            return await _familyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
