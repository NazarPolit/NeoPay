using MediatR;

namespace Wallet.Application.Commands.CreateWallet;

// Команда, яка передає дані для створення гаманця
public record CreateWalletCommand(Guid UserId, string Currency) : IRequest<Guid>;