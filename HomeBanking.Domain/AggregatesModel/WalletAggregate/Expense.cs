using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.WalletAggregate
{
    public class Expense
        : Entity
    {
        private decimal _cost;
        private string _comment;
        private DateTime _create;
        private int _walletId;
        private int _categoryId;
        private int _familyId;

        public decimal Cost { get => _cost; }
        public string Comment { get => _comment; }
        public DateTime Create { get => _create; }
        public int WalletId { get => _walletId; }
        public int CategoryId { get => _categoryId; }
        public int FamilyId { get => _familyId; }

        protected Expense() { }

        public Expense(decimal cost, string comment, int wallet, int category, int family)
        {
            _cost = cost > 0 ? cost : throw new HomeBankingDomainException("Cost must be greater than zero");
            _walletId = wallet > 0 ? wallet : throw new HomeBankingDomainException($"Invalid {nameof(wallet)} ID");
            _categoryId = category > 0 ? category : throw new HomeBankingDomainException($"Invalid {nameof(category)} ID");
            _familyId = family > 0 ? family : throw new HomeBankingDomainException($"Invalid {nameof(family)} ID");

            _comment = comment;
            _create = DateTime.UtcNow;
        }

        internal void SetCost(decimal cost)
        {
            if (cost > 0)
                _cost = cost;
        }

        internal void SetComment(string comment)
        {
            _comment = comment;
        }

        public void SetCategory(int categoryId)
        {
            _categoryId = categoryId;
        }

        internal void SetCreateDate(DateTime create)
        {
            _create = create;
        }
    }
}
