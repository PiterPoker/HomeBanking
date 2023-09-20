using HomeBanking.API.Application.Models;
using MediatR;
using System;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class CreateExpenseCommand : IRequest<WalletDTO>
    {
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public DateTime? Create { get; set; }

        public int WalletId { get; set; }
        public int CategoryId { get; set; }
        public int FamilyId { get; set; }

        public CreateExpenseCommand() { }
        public CreateExpenseCommand(decimal cost, string comment, DateTime create, int walletId, int categoryId, int familyId) 
        {
            Cost = cost;
            Comment = comment;
            Create = create;
            WalletId = walletId;
            CategoryId = categoryId;
            FamilyId = familyId;
        }
    }
}
