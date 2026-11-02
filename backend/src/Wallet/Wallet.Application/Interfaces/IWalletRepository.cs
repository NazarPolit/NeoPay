using Wallet.Domain.Entities;

namespace Wallet.Application.Interfaces;

public interface IWalletRepository
{
	Task<UserWallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<UserWallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	Task AddAsync(UserWallet wallet, CancellationToken cancellationToken);
	Task UpdateAsync(UserWallet wallet, CancellationToken cancellationToken);
}