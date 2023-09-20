using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Family
{
    [DataContract]
    public class GetFamiliesByUserIdCommand : IRequest<PaginatedItemsViewModel<FamilyDTO>>
    {

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        public GetFamiliesByUserIdCommand()
        {

        }
        public GetFamiliesByUserIdCommand(int userId ,int pageIndex, int pageSize)
        {
            UserId = userId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
