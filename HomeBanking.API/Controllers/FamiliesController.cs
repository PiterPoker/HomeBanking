using HomeBanking.API.Application.Commands.Family;
using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FamiliesController> _logger;
        public FamiliesController(IMediator mediator,
            ILogger<FamiliesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of family
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageIndex">Number of pages</param>
        /// <returns>Returns the given page</returns>
        [Route("families")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<FamilyDTO>>> GetFamiliesAsync([FromQuery] int userId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var famylies = await _mediator.Send(new GetFamiliesByUserIdCommand(userId, pageIndex, pageSize));
            return Ok(famylies);
        }

        /// <summary>
        /// Create a new family
        /// </summary>
        /// <param name="createFamilyCommand">Data to create a family</param>
        /// <returns>Returns the created family</returns>
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<FamilyDTO>> CreateFamilyAsync([FromBody] CreateFamilyCommand createFamilyCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createFamilyCommand.GetGenericTypeName(),
                nameof(createFamilyCommand.Name),
                createFamilyCommand.Name,
                createFamilyCommand);

            return await _mediator.Send(createFamilyCommand);
        }

        /// <summary>
        /// Update family
        /// </summary>
        /// <param name="updateFamilyCommand">Data to family</param>
        /// <returns>Returns the updated family</returns>
        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FamilyDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FamilyDTO>> UpdateFamilyAsync([FromBody] RenameFamilyCommand updateFamilyCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                updateFamilyCommand.GetGenericTypeName(),
                nameof(updateFamilyCommand.Name),
                updateFamilyCommand.Name,
                updateFamilyCommand);

            return await _mediator.Send(updateFamilyCommand);
        }

        /// <summary>
        /// Delete family
        /// </summary>
        /// <param name="id">Family ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteFamilyByIdAsync(int id)
        {
            await _mediator.Send(new DeleteFamilyByIdCommand(id));
        }

        /// <summary>
        /// Get a list of invate
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageIndex">Number of pages</param>
        /// <returns>Returns the given page</returns>
        [Route("invates")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<InvitationDTO>>> GetInvatesAsync([FromQuery] int userId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var invates = await _mediator.Send(new GetInvitationsByUserIdCommand(userId, pageIndex, pageSize));
            return Ok(invates);
        }

        /// <summary>
        /// Send invite in family
        /// </summary>
        /// <param name="sendInviteCommand">Data to invite</param>
        /// <returns>Returns the updated family</returns>
        [Route("invate/send")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FamilyDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FamilyDTO>> SendInviteInFamilyAsync([FromBody] SendInviteCommand sendInviteCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                sendInviteCommand.GetGenericTypeName(),
                nameof(sendInviteCommand.FromId),
                sendInviteCommand.FromId,
                sendInviteCommand);

            return await _mediator.Send(sendInviteCommand);
        }

        /// <summary>
        /// Answer on invite in family
        /// </summary>
        /// <param name="answerInviteCommand">Data to invite</param>
        /// <returns>Returns the updated family</returns>
        [Route("invate/answer")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FamilyDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FamilyDTO>> AnswerOnInviteInFamilyAsync([FromBody] AnswerInviteCommand answerInviteCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                answerInviteCommand.GetGenericTypeName(),
                nameof(answerInviteCommand.FamilyId),
                answerInviteCommand.FamilyId,
                answerInviteCommand);

            return await _mediator.Send(answerInviteCommand);
        }

        /// <summary>
        /// Remove invite
        /// </summary>
        /// <param name="id">Family ID</param>
        /// <param name="invateid">Invite ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}/invate/{invateid:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteInvateAsync(int id, int invateid)
        {
            await _mediator.Send(new DeleteInviteCommand(id, invateid));
        }

        /// <summary>
        /// Remove user from family
        /// </summary>
        /// <param name="id">Family ID</param>
        /// <param name="userid">User ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}/relative/{userid:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteUserFromFamilyAsync(int id, int userid)
        {
            await _mediator.Send(new DeleteUserFromFamilyCommand(id, userid));
        }
    }
}
