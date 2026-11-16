using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Commands.CreateWallet;

namespace Wallet.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
			cfg.RegisterServicesFromAssembly(typeof(CreateWalletCommand).Assembly));

		return services;
	}
}