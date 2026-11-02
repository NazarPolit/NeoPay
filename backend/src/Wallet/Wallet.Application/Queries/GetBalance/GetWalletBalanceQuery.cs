using MediatR;

namespace Wallet.Application.Queries.GetBalance;

public record GetWalletBalanceQuery(Guid UserId) : IRequest<WalletBalanceDto>;