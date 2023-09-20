using Dapper;
using HomeBanking.API.Application.Models.Queries;
using HomeBanking.API.Application.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBanking.API.Application.Queries
{
    public class ReportQueries : IReportQueries
    {
        private string _connectionString = string.Empty;

        public ReportQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByWalletAsync(int walletId, ReportRequest request)
        {
            var yearFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Year : DateTime.UtcNow.Year;
            var monthFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Month : DateTime.UtcNow.Month;
            var yearTill = request.DateTill.HasValue ? request.DateTill.Value.Year : DateTime.UtcNow.Year;
            var monthTill = request.DateTill.HasValue ? request.DateTill.Value.Month : DateTime.UtcNow.Month;
            var page = request.Page;
            var pageSize = request.PageSize;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>($"SELECT e.\"Create\", e.\"Comment\", e.\"Cost\", ec.\"Name\", ec.\"ColorId\" \"Color\" " +
                    $"FROM homebanking.wallets w LEFT JOIN homebanking.expenses e ON e.\"WalletId\" = w.\"Id\" " +
                    $"LEFT JOIN homebanking.categories ec ON ec.\"Id\" = e.\"CategoryId\" " +
                    $"WHERE w.\"Id\" = @walletId " +
                    $"AND EXTRACT(YEAR FROM e.\"Create\") BETWEEN @yearFrom AND @yearTill " +
                    $"AND EXTRACT(MONTH FROM e.\"Create\") BETWEEN @monthFrom AND @monthTill " +
                    $"LIMIT @pageSize OFFSET @page; ", new { walletId, yearFrom, monthFrom, yearTill, monthTill, page, pageSize });

                var expenses = MapExpense(result);

                return new PaginatedItemsViewModel<ExpenseViewModel>(page, pageSize, expenses.Count(), expenses);
            }
        }

        public async Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByPersonAsync(int userId, ReportRequest request)
        {
            var yearFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Year : DateTime.UtcNow.Year;
            var monthFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Month : DateTime.UtcNow.Month;
            var yearTill = request.DateTill.HasValue ? request.DateTill.Value.Year : DateTime.UtcNow.Year;
            var monthTill = request.DateTill.HasValue ? request.DateTill.Value.Month : DateTime.UtcNow.Month;
            var page = request.Page;
            var pageSize = request.PageSize;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>($"SELECT e.\"Create\", e.\"Comment\", e.\"Cost\", ec.\"Name\", ec.\"ColorId\" \"Color\" " +
                    $"FROM homebanking.users u LEFT JOIN homebanking.wallets w ON w.\"OwnerId\" = u.\"Id\" " +
                    $"LEFT JOIN homebanking.expenses e ON e.\"WalletId\" = w.\"Id\" " +
                    $"LEFT JOIN homebanking.categories ec ON ec.\"Id\" = e.\"CategoryId\" " +
                    $"WHERE u.\"Id\" = @userId AND e.\"WalletId\" IS NOT NULL " +
                    $"AND EXTRACT(YEAR FROM e.\"Create\") BETWEEN @yearFrom AND @yearTill " +
                    $"AND EXTRACT(MONTH FROM e.\"Create\") BETWEEN @monthFrom AND @monthTill " +
                    $"LIMIT @pageSize OFFSET @page; ", new { userId, yearFrom, monthFrom, yearTill, monthTill, page, pageSize });

                var expenses = MapExpense(result);

                return new PaginatedItemsViewModel<ExpenseViewModel>(page, pageSize, expenses.Count(), expenses);
            }
        }

        public async Task<PaginatedItemsViewModel<ExpenseViewModel>> ReportByFamilyAsync(int familyId, ReportRequest request)
        {
            var yearFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Year : DateTime.UtcNow.Year;
            var monthFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Month : DateTime.UtcNow.Month;
            var yearTill = request.DateTill.HasValue ? request.DateTill.Value.Year : DateTime.UtcNow.Year;
            var monthTill = request.DateTill.HasValue ? request.DateTill.Value.Month : DateTime.UtcNow.Month;
            var page = request.Page;
            var pageSize = request.PageSize;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>($"SELECT e.\"Create\", e.\"Comment\", e.\"Cost\", ec.\"Name\", ec.\"ColorId\"  \"Color\"" +
                    $"FROM homebanking.families f " +
                    $"LEFT JOIN homebanking.relatives r ON r.\"FamilyId\" = f.\"Id\" " +
                    $"LEFT JOIN homebanking.wallets w ON w.\"OwnerId\" = r.\"UserId\" " +
                    $"LEFT JOIN homebanking.expenses e ON e.\"WalletId\" = w.\"Id\" " +
                    $"LEFT JOIN homebanking.categories ec ON ec.\"Id\" = e.\"CategoryId\" " +
                    $"WHERE f.\"Id\" = @familyId AND e.\"WalletId\" IS NOT NULL " +
                    $"AND EXTRACT(YEAR FROM e.\"Create\") BETWEEN @yearFrom AND @yearTill " +
                    $"AND EXTRACT(MONTH FROM e.\"Create\") BETWEEN @monthFrom AND @monthTill " +
                    $"LIMIT @pageSize OFFSET @page; ", new { familyId, yearFrom, monthFrom, yearTill, monthTill, page, pageSize });

                var expenses = MapExpense(result);

                return new PaginatedItemsViewModel<ExpenseViewModel>(page, pageSize, expenses.Count(), expenses);
            }
        }

        public async Task<FamilyViewModel> StatisticByFamilyAsync(int familyId, StatisticRequest request)
        {
            var family = new FamilyViewModel();
            var yearFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Year : DateTime.UtcNow.Year;
            var monthFrom = request.DateFrom.HasValue ? request.DateFrom.Value.Month : DateTime.UtcNow.Month;
            var yearTill = request.DateTill.HasValue ? request.DateTill.Value.Year : DateTime.UtcNow.Year;
            var monthTill = request.DateTill.HasValue ? request.DateTill.Value.Month : DateTime.UtcNow.Month;
            var page = request.Page;
            var pageSize = request.PageSize;
            var currencyId = request.CurrencyId;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>($"SELECT profile.\"Name\" AS \"FirstName\", fam.\"Name\" AS \"LastName\", SUM(e2.\"Cost\") AS \"AmountTotal\" " +
                    $"FROM homebanking.expenses e2 " +
                    $"INNER JOIN homebanking.wallets w ON w.\"Id\" = e2.\"WalletId\" " +
                    $"INNER JOIN homebanking.profiles profile ON profile.\"UserId\" = w.\"OwnerId\" " +
                    $"INNER JOIN homebanking.families fam ON fam.\"Id\" = e2.\"FamilyId\" " +
                    $"WHERE w.\"CurrencyId\" = @currencyId AND e2.\"FamilyId\" = @FamilyId " +
                    $"AND EXTRACT(YEAR FROM e2.\"Create\") BETWEEN @yearFrom AND @yearTill " +
                    $"AND EXTRACT(MONTH FROM e2.\"Create\") BETWEEN @monthFrom AND @monthTill " +
                    $"GROUP BY(profile.\"Name\", fam.\"Name\") " +
                    $"ORDER BY fam.\"Name\", profile.\"Name\"", new { familyId, currencyId, yearFrom, monthFrom, yearTill, monthTill, page, pageSize });

                family = MapStatisticRelatives(result, family);

                result = await connection.QueryAsync<dynamic>($"SELECT ec.\"Name\" AS \"Category\", e.\"Cost\", (e.\"Cost\"/e2.\"CostAllCats\" * 100) AS \"Percentage\" " +
                    $"FROM(SELECT e.\"CategoryId\", SUM(e.\"Cost\") AS \"Cost\" " +
                    $"FROM homebanking.expenses e " +
                    $"INNER JOIN homebanking.wallets w ON w.\"Id\" = e.\"WalletId\"" +
                    $"WHERE w.\"CurrencyId\" = @currencyId AND e.\"FamilyId\" = @familyId " +
                    $"GROUP BY e.\"CategoryId\") e CROSS JOIN(SELECT SUM(e2.\"Cost\") OVER() AS \"CostAllCats\" " +
                    $"FROM homebanking.expenses e2 " +
                    $"INNER JOIN homebanking.wallets w ON w.\"Id\" = e2.\"WalletId\" " +
                    $"WHERE w.\"CurrencyId\" = @currencyId AND e2.\"FamilyId\" = @familyId " +
                    $"LIMIT 1) e2 " +
                    $"RIGHT OUTER JOIN homebanking.categories ec ON ec.\"Id\" = e.\"CategoryId\"", new { familyId, currencyId, yearFrom, monthFrom, yearTill, monthTill, page, pageSize });

                family = MapStatisticCategories(result, family);

                return family;
            }
        }

        private IEnumerable<ExpenseViewModel> MapExpense(IEnumerable<dynamic> result)
        {
            var expenses = new List<ExpenseViewModel>();
            foreach (var item in result)
            {
                var expense = new ExpenseViewModel()
                {
                    Create = item.Create,
                    Comment = !string.IsNullOrWhiteSpace(item.Comment) ? item.Comment : string.Empty,
                    Cost = item.Cost,
                    Category = new CategoryViewModel()
                    {
                        Name = item.Name,
                        Color = item.Color,
                    }
                };

                expenses.Add(expense);
            }

            return expenses;
        }

        private FamilyViewModel MapStatisticCategories(dynamic result, FamilyViewModel family)
        {
            var categories = new List<StatisticViewModel>();

            foreach (var item in result)
            {
                categories.Add(new StatisticViewModel() 
                {
                    Name = item.Category,
                    Amount = item.Cost != null ? item.Cost : 0,
                    Percentage = item.Percentage != null ? Math.Round(item.Percentage, 2, MidpointRounding.AwayFromZero) : 0
                });
            }
            family.Categories = categories;
            family.AmountTotal = categories.Sum(c=>c.Amount);
            return family;
        }

        private FamilyViewModel MapStatisticRelatives(dynamic result, FamilyViewModel family)
        {
            var relatives = new List<RelativeViewModel>();

            foreach (var item in result)
            {
                relatives.Add(new RelativeViewModel() 
                {
                    Name = item.FirstName,
                    Amount = item.AmountTotal != null ? item.AmountTotal : 0
                });
            }

            family.Name = result[0].LastName;
            family.Relatives = relatives;
            return family;
        }
    }
}
