using System;

namespace HomeBanking.API.Application.Models.Queries
{
    public class ReportRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTill { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
