using MediatR;
using Wallet.Application.Interfaces;
using Wallet.Domain.ValueObjects;

namespace Wallet.Application.Commands.DepositFunds;

public class DepositFundsCommandHandler : IRequestHandler<DepositFundsCommand, bool>
{
	private readonly IWalletRepository _walletRepository;

	public DepositFundsCommandHandler(IWalletRepository walletRepository)
	{
		_walletRepository = walletRepository;
	}

	public async Task<bool> Handle(DepositFundsCommand request, CancellationToken cancellationToken)
	{
		var wallet = await _walletRepository.GetByUserIdAsync(request.UserId, cancellationToken);
		if (wallet == null)
		{
			throw new KeyNotFoundException($"Wallet for User {request.UserId} not found.");
		}

		var moneyToDeposit = new Money(request.Amount, request.Currency);

		wallet.Deposit(moneyToDeposit);

		await _walletRepository.UpdateAsync(wallet, cancellationToken);

		return true;
	}
}