using HomeBanking.API.Application.Models;
using MediatR;
using System;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class UpdateExpenseByIdCommand : IRequest<WalletDTO>
    {
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int ExpenseId { get; set; }
        public decimal? Cost { get; set; }
        public string? Comment { get; set; }
        public DateTime? Create { get; set; }
        public int? CategoryId { get; set; }

        public UpdateExpenseByIdCommand() { }
        public UpdateExpenseByIdCommand(int userId, int walletId, int expenseId, decimal? cost, string? comment, DateTime? create, int? categoryId)
        {
            UserId = userId;
            WalletId = walletId;
            ExpenseId = expenseId;
            Cost = cost;
            Comment = comment;
            Create = create;
            CategoryId = categoryId;
        }

    }
}
