using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces;
using Wallet.Domain.Entities;
using Wallet.Infrastructure.Persistence;

namespace Wallet.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
	private readonly WalletDbContext _context;

	public WalletRepository(WalletDbContext context)
	{
		_context = context;
	}

	public async Task<UserWallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Wallets
			.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
	}

	public async Task<UserWallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		return await _context.Wallets
			.FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
	}

	public async Task AddAsync(UserWallet wallet, CancellationToken cancellationToken)
	{
		await _context.Wallets.AddAsync(wallet, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(UserWallet wallet, CancellationToken cancellationToken)
	{
		_context.Wallets.Update(wallet);
		await _context.SaveChangesAsync(cancellationToken);
	}
}