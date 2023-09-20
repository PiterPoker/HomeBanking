using System;

namespace HomeBanking.API.Application.Models.Queries
{
    public class ExpenseViewModel
    {
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public DateTime Create { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
