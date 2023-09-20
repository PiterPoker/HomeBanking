using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.ViewModels;
using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeBanking.API.Application.Commands.Category
{
    /// <summary>Category get page command model</summary>
    [DataContract]
    public class GetCategoryListCommand : IRequest<PaginatedItemsViewModel<CategoryDTO>>
    {

        [DataMember]
        public int PageIndex { get; private set; }

        [DataMember]
        public int PageSize { get; private set; }

        public GetCategoryListCommand()
        {

        }
        public GetCategoryListCommand(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
