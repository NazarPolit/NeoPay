using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Application.Commands.DepositFunds
{
	public record DepositFundsCommand(Guid UserId, decimal Amount, string Currency) : IRequest<bool>;
}
