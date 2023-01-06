using OpenQA.Selenium.Chrome;

namespace Gradebook.Tests.Selenium;

public static class WebDriverBuilder
{
    public static IWebDriver BuildWebDriver()
    {
        ChromeOptions options = new();
        options.AddArgument("--window-size=1366,768");
        options.AddArgument("--disable-extensions");
        options.AddArgument("--proxy-server='direct://'");
        options.AddArgument("--proxy-bypass-list=*");
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--incognito");
        options.AddArguments("no-sandbox",
            "--disable-infobars",
            "--disable-dev-shm-usage",
            "--disable-browser-side-navigation",
            "--ignore-certificate-errors",
            "--disable-blink-features=AutomationControlled");

        if (bool.Parse(ConfigurationManager.GetValue("Browser:RunHeadless")))
        {
            options.AddArgument("--headless");
        }
        string? path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        var driver = new DriverImplementation(path + @$"/Drivers/{ConfigurationManager.GetValue("Browser:Platform")}/", options);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);

        return driver;
    }
}
