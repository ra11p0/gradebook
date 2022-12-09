using Gradebook.Foundation.Database;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");
var config = builder.Build();
var _ = config.GetConnectionString("DefaultAppDatabase");
