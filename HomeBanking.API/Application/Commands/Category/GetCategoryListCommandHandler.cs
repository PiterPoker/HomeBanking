using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Commands.Category
{
    public class GetCategoryListCommandHandler 
        : IRequestHandler<GetCategoryListCommand, PaginatedItemsViewModel<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryListCommandHandler(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PaginatedItemsViewModel<CategoryDTO>> Handle(GetCategoryListCommand command, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var paginationCategory = categories.Select(c=>CategoryDTO.FromCategory(c))
                .Skip(command.PageSize * command.PageIndex)
                .Take(command.PageSize);

            return new PaginatedItemsViewModel<CategoryDTO>(command.PageIndex, command.PageSize, paginationCategory.Count(), paginationCategory);
        }
    }
}
