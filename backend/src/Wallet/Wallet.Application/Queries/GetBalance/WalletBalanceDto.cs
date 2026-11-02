namespace Wallet.Application.Queries.GetBalance;

public record WalletBalanceDto(Guid WalletId, decimal AvailableBalance, decimal HeldAmount, string Currency);