using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class GetInvitationsByUserIdCommand : IRequest<PaginatedItemsViewModel<FamilyDTO>>
    {

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        public GetInvitationsByUserIdCommand()
        {

        }
        public GetInvitationsByUserIdCommand(int userId, int pageIndex, int pageSize)
        {
            UserId = userId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
