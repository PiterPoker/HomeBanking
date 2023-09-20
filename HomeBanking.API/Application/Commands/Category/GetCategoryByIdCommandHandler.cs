using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryDomain = HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Commands.Category
{
    public class GetCategoryByIdCommandHandler
        : IRequestHandler<GetCategoryByIdCommand, CategoryDTO>
    {
        private readonly CategoryDomain.ICategoryRepository _categoryRepository;

        public GetCategoryByIdCommandHandler(CategoryDomain.ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(command.Id);

            return CategoryDTO.FromCategory(category);
        }
    }
}
