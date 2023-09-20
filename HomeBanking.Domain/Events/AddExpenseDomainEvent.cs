using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Events
{
    public class AddExpenseDomainEvent
        : INotification
    {
        public Wallet Wallet { get; }
        public Expense Expense { get; }

        public AddExpenseDomainEvent(Wallet wallet, Expense expense) 
        {
            Wallet = wallet;
            Expense = expense;
        }
    }
}
