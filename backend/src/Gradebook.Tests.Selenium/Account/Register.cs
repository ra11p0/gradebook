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
    public void LogoExists()
    {
        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        var logos = _driver.FindElements(By.CssSelector(".text-dark.display-6.text-decoration-none.my-auto"));
        Assert.That(logos.Any());
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
