using Microsoft.Extensions.Configuration;

namespace Gradebook.Tests.Selenium;

public static class ConfigurationManager
{
    private readonly static IConfigurationRoot _configuration;
    public static IConfigurationRoot Configuration => _configuration;
    static ConfigurationManager()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json");
        _configuration = builder.Build();
    }
    public static string GetValue(string key)
    {
        return _configuration[key];
    }
}
