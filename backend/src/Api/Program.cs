using Gradebook.Foundation.DependencyResolver;
using Gradebook.Foundation.SignalR;
using Gradebook.Foundation.DependencyResolver.Services;
using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Inject();

builder.Services.AddControllers();

var app = builder.Build();

app.UseEmailExceptionHandler();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    HangfireService.MapHangfireEndpoint(app);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello!");

app.MapDefaultControllerRoute();

app.MapHubs();

app.Run();
