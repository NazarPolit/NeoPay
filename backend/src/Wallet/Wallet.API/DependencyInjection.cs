using Microsoft.Extensions.DependencyInjection;

namespace Wallet.API;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddOpenApi();

		services.AddCors(options =>
		{
			options.AddPolicy("AllowAngular", policy =>
			{
				policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
					  .AllowAnyHeader()
					  .AllowAnyMethod();
			});
		});

		return services;
	}
}