using Receipts.Foundation.DependencyResolver;
using Gradebook.Foundation.SignalR;

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

HubsMapper.MapHubs(app);

app.Run();
