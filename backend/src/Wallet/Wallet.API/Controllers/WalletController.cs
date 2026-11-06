using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Commands.CreateWallet;
using Wallet.Application.Queries.GetBalance;

namespace Wallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
	private readonly IMediator _mediator;

	public WalletController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand command)
	{
		var walletId = await _mediator.Send(command);
		return Ok(new { WalletId = walletId });
	}

	[HttpGet("{userId}")]
	public async Task<IActionResult> GetBalance(Guid userId)
	{
		var balance = await _mediator.Send(new GetWalletBalanceQuery(userId));
		return Ok(balance);
	}
}