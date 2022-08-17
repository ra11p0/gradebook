var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(e=>{
    e.AddDefaultPolicy(p=>{
        p.WithOrigins("https://127.0.0.1:3005", "http://127.0.0.1:3005",
                      "https://localhost:3005", "http://localhost:3005",
                      "https://127.0.0.1:7059", "http://127.0.0.1:7059",
                      "https://localhost:7059", "http://localhost:7059")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();

app.Run();
