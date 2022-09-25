using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Gradebook.Tests.Selenium;

public static class WebDriverBuilder
{
    public static IWebDriver BuildWebDriver()
    {
        ChromeOptions options = new();
        if (bool.Parse(ConfigurationManager.GetValue("Browser:RunHeadless"))) options.AddArgument("--headless");
        string? path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        return new ChromeDriver(path + @"/Drivers/", options);
    }
}
