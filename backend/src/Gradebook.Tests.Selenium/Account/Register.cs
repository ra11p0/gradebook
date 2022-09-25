using OpenQA.Selenium;

namespace Gradebook.Tests.Selenium.Account;

public class Register
{
    private IWebDriver? _driver;
    [SetUp]
    public void Setup()
    {
        _driver = WebDriverBuilder.BuildWebDriver();
    }

    [Test]
    public void Test1()
    {
        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
