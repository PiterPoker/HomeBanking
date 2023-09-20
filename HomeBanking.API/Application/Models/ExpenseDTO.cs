using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using System;

namespace HomeBanking.API.Application.Models
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public DateTime Create { get; set; }

        public int WalletId { get; set; }
        public int CategoryId { get; set; }

        public static ExpenseDTO FromExpense(Expense expense)
        {
            return expense != null ? new ExpenseDTO()
            {
                Id = expense.Id,
                Cost = expense.Cost,
                Comment = expense.Comment,
                Create = expense.Create,
                WalletId = expense.WalletId,
                CategoryId = expense.CategoryId
            } : null;
        }

    }
}
