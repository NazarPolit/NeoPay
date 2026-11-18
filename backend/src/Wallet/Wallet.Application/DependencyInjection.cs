using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Behaviors;
using Wallet.Application.Commands.CreateWallet;

namespace Wallet.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

		services.AddMediatR(cfg =>
		{
			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); 
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		});

		return services;
	}
}