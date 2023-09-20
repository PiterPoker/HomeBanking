using HomeBanking.API.Application.Commands.User;
using HomeBanking.API.Application.Commands.Wallet;
using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WalletsController> _logger;
        public WalletsController(IMediator mediator,
            ILogger<WalletsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of wallet
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageIndex">Number of pages</param>
        /// <returns>Returns the given page</returns>
        [Route("wallets")]
        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<WalletDTO>>> GetWalletsAsync([FromQuery] int userId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var wallets = await _mediator.Send(new GetWalletListCommand(userId, pageIndex, pageSize));
            return Ok(wallets);
        }

        /// <summary>
        /// Create a new Wallet
        /// </summary>
        /// <param name="newWallet">Data to create a Wallet</param>
        /// <returns>Returns the created Wallet</returns>
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<WalletDTO>> CreateWalletAsync([FromBody] WalletViewModel newWallet)
        {
            var user = await _mediator.Send(new GetUserByIdCommand(newWallet.OwnerId));
            if (user != null)
            {
                var createWalletCommand = new CreateWalletCommand(user, newWallet.CurrencyId, newWallet.Amount);
                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    createWalletCommand.GetGenericTypeName(),
                    nameof(createWalletCommand.Owner),
                    createWalletCommand.Owner.Login,
                    createWalletCommand);

                return await _mediator.Send(createWalletCommand);
            }
            return BadRequest();
        }

        /// <summary>
        /// Put money in wallet 
        /// </summary>
        /// <param name="transactionCommand">Data to add amount</param>
        /// <returns>Returns the updated Wallet</returns>
        [Route("transaction")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> TransactionAsync([FromBody] TransactionCommand transactionCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                transactionCommand.GetGenericTypeName(),
                nameof(transactionCommand.FromId),
                transactionCommand.FromId,
                transactionCommand);
            var result = await _mediator.Send(transactionCommand);

            if (result)
                return Ok("The transaction was successful");
            else
                return BadRequest();
        }

        /// <summary>
        /// New expense
        /// </summary>
        /// <param name="createExpenceCommand">Data to create expanse</param>
        /// <returns>Returns the updated Wallet</returns>
        [Route("create/expense")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WalletDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WalletDTO>> CreateExpenseAsync([FromBody] CreateExpenseCommand createExpenceCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createExpenceCommand.GetGenericTypeName(),
                nameof(createExpenceCommand.WalletId),
                createExpenceCommand.WalletId,
                createExpenceCommand);

            return await _mediator.Send(createExpenceCommand);
        }

        /// <summary>
        /// Update expense by ID
        /// </summary>
        /// <param name="updateExpenseByIdCommand">Data to update expanse</param>
        /// <returns>Returns the Wallet</returns>
        [Route("update/expense")]
        [HttpPut]
        [ProducesResponseType(typeof(WalletDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WalletDTO>> UpdateExpenseByIdAsync([FromBody] UpdateExpenseByIdCommand updateExpenseByIdCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                updateExpenseByIdCommand.GetGenericTypeName(),
                nameof(updateExpenseByIdCommand.WalletId),
                updateExpenseByIdCommand.WalletId,
                updateExpenseByIdCommand);

            var Wallet = await _mediator.Send(updateExpenseByIdCommand);

            return Ok(Wallet);
        }

        /// <summary>
        /// Delete Wallet
        /// </summary>
        /// <param name="id">Wallet ID</param>
        /// <returns></returns>
        [HttpDelete("wallet/{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteWalletByIdAsync(int id)
        {
            await _mediator.Send(new DeleteWalletByIdCommand(id));
        }

        /// <summary>
        /// Delete expense
        /// </summary>
        /// <param name="userid">Deleter userID</param>
        /// <param name="id">Expense ID</param>
        /// <returns></returns>
        [HttpDelete("owner/{userid:int}/expense/{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteExpenseByIdAsync(int userid, int id)
        {
            await _mediator.Send(new DeleteExpenseByIdCommand(id, userid));
        }
    }
}
