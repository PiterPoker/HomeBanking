using HomeBanking.API.Application.Commands;
using HomeBanking.API.Application.Commands.Category;
using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IMediator mediator,
            ILogger<CategoriesController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of categories
        /// </summary>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageIndex">Number of pages</param>
        /// <returns>Returns the given page</returns>
        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<CategoryDTO>>> GetCategoriesAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var categories = await _mediator.Send(new GetCategoryListCommand(pageIndex, pageSize));
            return Ok(categories);
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="createCategotyCommand">Data to create a category</param>
        /// <returns>Returns the created category</returns>
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategotyAsync([FromBody] CreateCategotyCommand createCategotyCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createCategotyCommand.GetGenericTypeName(),
                nameof(createCategotyCommand.ColorId),
                createCategotyCommand.ColorId,
                createCategotyCommand);

            return await _mediator.Send(createCategotyCommand);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="updateCategotyCommand">Data to update</param>
        /// <returns>Returns the updated category</returns>
        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryDTO>> UpdateCategotyAsync([FromBody] UpdateCategotyCommand updateCategotyCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                updateCategotyCommand.GetGenericTypeName(),
                nameof(updateCategotyCommand.Name),
                updateCategotyCommand.Name,
                updateCategotyCommand);

            return await _mediator.Send(updateCategotyCommand);
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Returns the category</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdCommand(id));

            return Ok(category);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteCategoryByIdAsync(int id)
        {
            await _mediator.Send(new DeleteCategoryByIdCommand(id));
        }
    }
}
