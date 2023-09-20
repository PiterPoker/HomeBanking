using HomeBanking.Domain.Events;
using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet
      : Entity, IAggregateRoot
    {
        private decimal _amount;
        private int _currencyId;
        private int _ownerId;
        private readonly List<Expense> _expenses;
        public IReadOnlyCollection<Expense> Expenses => _expenses;
        public decimal Amount { get => _amount; }
        public int OwnerId { get => _ownerId; }
        public Currency Currency { get => Currency.From(_currencyId); }


        public void SetOwner(int ownerId)
        {
            _ownerId = ownerId;
        }

        public void ChangeExpense(int expenseId, decimal? cost, string? comment, DateTime? create, int? categoryId)
        {
            var expense = _expenses.SingleOrDefault(exp => exp.Id == expenseId);

            if (expense == null)
                throw new HomeBankingDomainException("Expense not found");

            if (cost.HasValue)
            {
                var costToInt = ConvertToInt(cost.Value, Currency.From(_currencyId));
                var newAmount = _amount + expense.Cost - costToInt;

                if (newAmount < 0)
                    throw new HomeBankingDomainException("The cost must be less than the wallet amount");

                _amount = newAmount;
                expense.SetCost(costToInt);
            }

            expense.SetComment(comment);
            if (create.HasValue)
                expense.SetCreateDate(create.Value);
            if (categoryId.HasValue)
                expense.SetCategory(categoryId.Value);

            _expenses.RemoveAll(exp => exp.Id == expense.Id);
            _expenses.Add(expense);
        }

        public void TransactionToWallet(decimal amount)
        {
            if (amount < 0)
                throw new HomeBankingDomainException("Amount must be more than zero");

            _amount += amount;
        }

        public void TransactionFromWallet(decimal amount)
        {
            if (_amount < amount)
                throw new HomeBankingDomainException("Insufficient funds");
            if (amount < 0)
                throw new HomeBankingDomainException("Amount must be more than zero");

            _amount -= amount;
        }

        public void SetAmount(decimal amount)
        {
            _amount += ConvertToInt(amount, Currency.From(_currencyId));
        }

        private decimal ConvertToInt(decimal value, Currency currency)
        {
            if (value > 0)
            {
                return Math.Round(value, 4, MidpointRounding.AwayFromZero);
            }
            else if (value == 0)
                return 0;
            else
                throw new HomeBankingDomainException("The cost must be less than the wallet amount");
        }

        public void SetCurrency(int currencyId)
        {
            _currencyId = currencyId;
        }

        public static Wallet NewWallet()
        {
            return new Wallet();
        }

        protected Wallet()
        {
            _expenses = new List<Expense>();
            _amount = 0;
            _currencyId = Currency.BYN.Id;
        }

        public Wallet(int userId, Currency currency, int amount) : this()
        {
            _ownerId = userId > 0 ? userId : throw new HomeBankingDomainException($"Invalid user ID");
            _currencyId = currency.Id > 0 ? currency.Id : throw new HomeBankingDomainException($"Invalid currency ID");
            _amount = amount;
        }

        public void AddExpense(decimal cost, string comment, int categoryId, int familyId, DateTime create)
        {
            var expense = default(Expense);
            var costToInt = ConvertToInt(cost, Currency.From(_currencyId));

            expense = _expenses.Where(exp => exp.Cost == costToInt
            && exp.Comment == comment
            && exp.CategoryId == categoryId
            && exp.FamilyId == familyId
            && exp.Create == create)
                .SingleOrDefault();


            if (expense != null)
            {
                var newAount = _amount + expense.Cost;
                if (cost <= newAount)
                {
                    _amount = newAount - costToInt;
                    expense.SetCost(costToInt);
                }
                else
                {
                    throw new HomeBankingDomainException("The cost must be less than the wallet amount");
                }

                expense.SetComment(comment);
                expense.SetCategory(categoryId);
            }
            else
            {
                if (_amount >= costToInt)
                {
                    _amount -= costToInt;
                    expense = new Expense(costToInt, comment, Id, categoryId, familyId);
                    _expenses.Add(expense);
                }
            }

            expense.SetCreateDate(create);

            AddDomainEvent(new AddExpenseDomainEvent(this, expense));
        }
    }
}
