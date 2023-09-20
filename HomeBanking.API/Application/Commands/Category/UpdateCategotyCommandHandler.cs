using HomeBanking.API.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryDomain = HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Commands.Category
{
    public class UpdateCategotyCommandHandler
        : IRequestHandler<UpdateCategotyCommand, CategoryDTO>
    {
        private readonly CategoryDomain.ICategoryRepository _categoryRepository;

        public UpdateCategotyCommandHandler(CategoryDomain.ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<CategoryDTO> Handle(UpdateCategotyCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(command.Id);
            category.SetName(command.Name);
            category.SetColorById(command.ColorId);

            _categoryRepository.Update(category);

            await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return CategoryDTO.FromCategory(category);
        }
    }
}
