using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Wallet.Application.Commands.CreateWallet;
using Wallet.Application.Interfaces;
using Wallet.Infrastructure.Persistence;
using Wallet.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<WalletDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();

builder.Services.AddMediatR(cfg =>
	cfg.RegisterServicesFromAssembly(typeof(CreateWalletCommand).Assembly));

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngular", policy =>
	{
		policy.WithOrigins("http://localhost:4200")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseAuthorization();
app.UseCors("AllowAngular");
app.MapControllers();
app.Run();