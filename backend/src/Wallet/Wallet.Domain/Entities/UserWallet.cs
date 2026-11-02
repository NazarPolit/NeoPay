using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.Entities;

public class UserWallet
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }

	// Поточний реальний баланс
	public Money Balance { get; private set; }

	// Гроші, які тимчасово заблоковані для транзакції (Saga Pattern)
	public Money HeldAmount { get; private set; }

	// Порожній конструктор вимагається для Entity Framework Core
	private UserWallet() { }

	public UserWallet(Guid id, Guid userId, string currency)
	{
		Id = id;
		UserId = userId;
		Balance = new Money(0, currency);
		HeldAmount = new Money(0, currency);
	}

	// Поповнення гаманця
	public void Deposit(Money amount)
	{
		Balance += amount;
	}

	// КРОК 1 САГИ: Заморожування коштів (коли Payment API тільки ініціював платіж)
	public void HoldFunds(Money amount)
	{
		var availableBalance = Balance.Amount - HeldAmount.Amount;
		if (availableBalance < amount.Amount)
			throw new InvalidOperationException("Insufficient funds to hold");

		HeldAmount += amount;
	}

	// КРОК 2 (Успіх Саги): Платіж пройшов у Stripe, остаточно знімаємо гроші
	public void CommitHeldFunds(Money amount)
	{
		if (HeldAmount.Amount < amount.Amount)
			throw new InvalidOperationException("Cannot commit more than held");

		HeldAmount -= amount;
		Balance -= amount;
	}

	// КРОК 2 (Провал Саги): Stripe відхилив платіж, розморожуємо кошти (Compensating Action)
	public void RollbackHeldFunds(Money amount)
	{
		if (HeldAmount.Amount < amount.Amount)
			throw new InvalidOperationException("Cannot rollback more than held");

		HeldAmount -= amount;
	}
}