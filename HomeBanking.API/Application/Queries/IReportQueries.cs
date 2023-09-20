using HomeBanking.API.Application.Models;
using HomeBanking.API.Application.Models.Queries;
using HomeBanking.API.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Queries
{
    public interface IReportQueries
    {
        Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByFamilyAsync(int familyId, ReportRequest request);

        Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByPersonAsync(int userId, ReportRequest request);

        Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByWalletAsync(int walletId, ReportRequest request);
        Task<FamilyViewModel> StatisticByFamilyAsync(int familyId, StatisticRequest request);
    }
}
