using Receipts.Foundation.DependencyResolver;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DependencyInjector.Inject(builder.Services, builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();

app.Run();
