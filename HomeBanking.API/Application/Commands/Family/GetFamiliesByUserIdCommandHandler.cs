using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FamilyDomain = HomeBanking.Domain.AggregatesModel.FamilyAggregate;

namespace HomeBanking.API.Application.Commands.Family
{
    public class GetFamiliesByUserIdCommandHandler
        : IRequestHandler<GetFamiliesByUserIdCommand, PaginatedItemsViewModel<FamilyDTO>>
    {
        private readonly FamilyDomain.IFamilyRepository _familyRepository;

        public GetFamiliesByUserIdCommandHandler(FamilyDomain.IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository ?? throw new ArgumentNullException(nameof(familyRepository));
        }

        public async Task<PaginatedItemsViewModel<FamilyDTO>> Handle(GetFamiliesByUserIdCommand command, CancellationToken cancellationToken)
        {
            var families = await _familyRepository.GetAsync();

            var familiesByUserId = families.Where(f=>f.Relatives.Any(r=>r.UserId == command.UserId));

            var paginationFamiles = familiesByUserId.Select(f => FamilyDTO.FromFamily(f))
                .Skip(command.PageSize * command.PageIndex)
                .Take(command.PageSize);

            return new PaginatedItemsViewModel<FamilyDTO>(command.PageIndex, command.PageSize, paginationFamiles.Count(), paginationFamiles);
        }
    }
}
