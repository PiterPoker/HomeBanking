using System.Collections.Generic;

namespace HomeBanking.API.Application.Models.Queries
{
    public class FamilyViewModel
    {
        public string Name { get; set; }
        public decimal AmountTotal { get; set; }
        public IEnumerable<StatisticViewModel> Categories { get; set; }
        public IEnumerable<RelativeViewModel> Relatives { get; set; }

        public FamilyViewModel() 
        {
            Categories = new List<StatisticViewModel>();
            Relatives = new List<RelativeViewModel>();
        }
    }
}
