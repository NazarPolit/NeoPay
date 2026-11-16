using Scalar.AspNetCore;
using Wallet.API;
using Wallet.Application;
using Wallet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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