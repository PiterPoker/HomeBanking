using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryDomain = HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Commands.Category
{
    public class CreateCategotyCommandHandler
        : IRequestHandler<CreateCategotyCommand, CategoryDTO>
    {
        private readonly CategoryDomain.ICategoryRepository _categoryRepository;

        public CreateCategotyCommandHandler(CategoryDomain.ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<CategoryDTO> Handle(CreateCategotyCommand command, CancellationToken cancellationToken)
        {
            var category = CategoryDomain.Category.NewCategory();
            category.SetName(command.Name);
            category.SetColorById(command.ColorId);

            _categoryRepository.Add(category);

            await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return CategoryDTO.FromCategory(category);
        }
    }
}
