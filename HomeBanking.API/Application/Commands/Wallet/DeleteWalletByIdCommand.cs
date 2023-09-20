using MediatR;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class DeleteWalletByIdCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteWalletByIdCommand() { }
        public DeleteWalletByIdCommand(int id) 
        {
            Id = id;
        }
    }
}
