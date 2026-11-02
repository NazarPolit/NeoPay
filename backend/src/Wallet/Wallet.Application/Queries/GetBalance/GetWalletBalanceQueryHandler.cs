using MediatR;
using Wallet.Application.Interfaces;

namespace Wallet.Application.Queries.GetBalance;

public class GetWalletBalanceQueryHandler : IRequestHandler<GetWalletBalanceQuery, WalletBalanceDto>
{
	private readonly IWalletRepository _walletRepository;

	public GetWalletBalanceQueryHandler(IWalletRepository walletRepository)
	{
		_walletRepository = walletRepository;
	}

	public async Task<WalletBalanceDto> Handle(GetWalletBalanceQuery request, CancellationToken cancellationToken)
	{
		var wallet = await _walletRepository.GetByUserIdAsync(request.UserId, cancellationToken);

		if (wallet == null)
		{
			throw new KeyNotFoundException("Wallet not found for the specified user.");
		}

		// Мапимо сутність у DTO, обчислюючи доступний залишок
		return new WalletBalanceDto(
			wallet.Id,
			wallet.Balance.Amount - wallet.HeldAmount.Amount,
			wallet.HeldAmount.Amount,
			wallet.Balance.Currency
		);
	}
}