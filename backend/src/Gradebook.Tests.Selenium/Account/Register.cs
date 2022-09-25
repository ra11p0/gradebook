using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
    [Test]
    public void ThisTestShouldFail()
    {
        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        var logos = _driver.FindElements(By.CssSelector(".teooxt-dark.display-6.text-decoration-none.my-auto"));
        Assert.That(logos.Any());
    }
    [Test]
    public void RegisterNewUser()
    {
        //  Login form view
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        var registerButton = _driver.FindElement(By.CssSelector("a[href='/register']"));
        registerButton.Click();

        //  Register form view
        wait.Until(drv => drv.FindElement(By.CssSelector("form")));
        var emailField = _driver.FindElement(By.CssSelector("#email"));
        var password = _driver.FindElement(By.CssSelector("#password"));
        var password2 = _driver.FindElement(By.CssSelector("#password2"));
        var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

        emailField.SendKeys("hell0@world.pl");
        password.SendKeys("hEll0worldpl112!A");
        password2.SendKeys("hEll0worldpl112!A");
        submitButton.Click();

        //  Alert
        var successAlertOkButton = wait.Until(drv => drv.FindElement(By.CssSelector(".swal2-confirm.swal2-styled")));
        Assert.That(successAlertOkButton.Displayed);

    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
