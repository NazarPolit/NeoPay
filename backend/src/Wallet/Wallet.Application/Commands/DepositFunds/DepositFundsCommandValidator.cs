using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Application.Commands.DepositFunds
{
	public class DepositFundsCommandValidator : AbstractValidator<DepositFundsCommand>
	{
		public DepositFundsCommandValidator()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("User ID is required.");

			RuleFor(x => x.Amount)
				.GreaterThan(0).WithMessage("Deposit amount must be strictly greater than zero.");

			RuleFor(x => x.Currency)
				.NotEmpty()
				.Length(3).WithMessage("Currency must be exactly 3 characters (e.g., USD, EUR).");
		}
	}
}
