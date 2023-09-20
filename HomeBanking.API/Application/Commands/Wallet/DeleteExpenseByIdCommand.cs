using MediatR;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class DeleteExpenseByIdCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DeleteExpenseByIdCommand() { }
        public DeleteExpenseByIdCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
