using MediatR;
using Wallet.Application.Interfaces;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.CreateWallet;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Guid>
{
	private readonly IWalletRepository _walletRepository;

	public CreateWalletCommandHandler(IWalletRepository walletRepository)
	{
		_walletRepository = walletRepository;
	}

	public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
	{
		// Перевіряємо, чи немає вже гаманця у цього користувача (спрощена логіка)
		var existingWallet = await _walletRepository.GetByUserIdAsync(request.UserId, cancellationToken);
		if (existingWallet != null)
		{
			throw new InvalidOperationException("User already has a wallet.");
		}

		var wallet = new UserWallet(Guid.NewGuid(), request.UserId, request.Currency);

		await _walletRepository.AddAsync(wallet, cancellationToken);

		return wallet.Id;
	}
}