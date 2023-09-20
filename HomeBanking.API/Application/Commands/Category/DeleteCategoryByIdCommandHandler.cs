using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryDomain = HomeBanking.Domain.AggregatesModel.CategoryAggregate;

namespace HomeBanking.API.Application.Commands.Category
{
    public class DeleteCategoryByIdCommandHandler
        : IRequestHandler<DeleteCategoryByIdCommand, bool>
    {
        private readonly CategoryDomain.ICategoryRepository _categoryRepository;

        public DeleteCategoryByIdCommandHandler(CategoryDomain.ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<bool> Handle(DeleteCategoryByIdCommand command, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(command.Id);

            return await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
