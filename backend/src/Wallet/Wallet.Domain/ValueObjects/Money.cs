namespace Wallet.Domain.ValueObjects;

public record Money
{
	public decimal Amount { get; init; }
	public string Currency { get; init; }

	public Money(decimal amount, string currency)
	{
		if (amount < 0)
			throw new ArgumentException("Amount cannot be negative", nameof(amount));

		if (string.IsNullOrWhiteSpace(currency))
			throw new ArgumentException("Currency must be specified", nameof(currency));

		Amount = amount;
		Currency = currency.ToUpperInvariant();
	}

	// Перевантаження оператора додавання, щоб не можна було додати USD до EUR
	public static Money operator +(Money a, Money b)
	{
		if (a.Currency != b.Currency)
			throw new InvalidOperationException($"Cannot add {b.Currency} to {a.Currency}");

		return new Money(a.Amount + b.Amount, a.Currency);
	}

	// Перевантаження оператора віднімання
	public static Money operator -(Money a, Money b)
	{
		if (a.Currency != b.Currency)
			throw new InvalidOperationException($"Cannot subtract {b.Currency} from {a.Currency}");

		return new Money(a.Amount - b.Amount, a.Currency);
	}
}