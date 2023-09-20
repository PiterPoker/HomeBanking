using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Wallet
{
    [DataContract]
    public class GetWalletListCommand : IRequest<PaginatedItemsViewModel<WalletDTO>>
    {
        [DataMember]
        public int UserId { get; private set; }
        [DataMember]
        public int PageIndex { get; private set; }

        [DataMember]
        public int PageSize { get; private set; }

        public GetWalletListCommand()
        {

        }
        public GetWalletListCommand(int userId, int pageIndex, int pageSize)
        {
            UserId = userId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
