using HomeBanking.API.Application.Models;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WalletDomain = HomeBanking.Domain.AggregatesModel.WalletAggregate;

namespace HomeBanking.API.Application.Commands.Wallet
{
    public class TransactionCommandHandler
        : IRequestHandler<TransactionCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;

        public TransactionCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<bool> Handle(TransactionCommand command, CancellationToken cancellationToken)
        {
            var walletFrom = await _walletRepository.GetAsync(command.FromId);
            var walletTo = await _walletRepository.GetAsync(command.ToId);

            if (walletFrom == null)
                throw new ArgumentNullException(nameof(walletFrom));
            else if (walletFrom.OwnerId != command.FromId)
                throw new HomeBankingDomainException($"Wrong {nameof(walletFrom.Amount)} owner");
            if (walletTo == null)
                throw new ArgumentNullException(nameof(walletTo));
            else if (walletTo.OwnerId != command.ToId)
                throw new HomeBankingDomainException("Wrong walletTo owner");

            walletFrom.TransactionFromWallet(command.Amount);
            walletTo.TransactionToWallet(command.Amount);

            _walletRepository.Update(walletFrom);
            _walletRepository.Update(walletTo);


            return await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
