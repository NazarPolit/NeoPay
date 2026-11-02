using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence;

public class WalletDbContext : DbContext
{
	public DbSet<UserWallet> Wallets { get; set; }

	public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserWallet>(builder =>
		{
			builder.ToTable("Wallets");

			builder.HasKey(w => w.Id);
			builder.Property(w => w.UserId).IsRequired();

			// Мапінг Value Object: Balance
			builder.ComplexProperty(w => w.Balance, b =>
			{
				b.Property(m => m.Amount).HasColumnName("BalanceAmount").HasPrecision(18, 4);
				b.Property(m => m.Currency).HasColumnName("BalanceCurrency").HasMaxLength(3);
			});

			// Мапінг Value Object: HeldAmount
			builder.ComplexProperty(w => w.HeldAmount, h =>
			{
				h.Property(m => m.Amount).HasColumnName("HeldAmount").HasPrecision(18, 4);
				h.Property(m => m.Currency).HasColumnName("HeldCurrency").HasMaxLength(3);
			});
		});

		base.OnModelCreating(modelBuilder);
	}
}