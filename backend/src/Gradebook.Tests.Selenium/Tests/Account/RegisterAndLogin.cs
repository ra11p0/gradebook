using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Account;

[Order(1)]
public class RegisterAndLogin
{
    private IWebDriver? _driver;
    [SetUp]
    public void Setup()
    {
        _driver = WebDriverBuilder.BuildWebDriver();
    }
    [Test]
    [Order(1)]
    public void CanRegisterNewUser()
    {
        //  Login form view
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        var registerButton = _driver.FindElement(By.CssSelector("a[href='/register']"));
        registerButton.Click();

        //  RegisterAndLogin form view
        wait.Until(drv => drv.FindElement(By.CssSelector("form")));
        var emailField = _driver.FindElement(By.CssSelector("#email"));
        var password = _driver.FindElement(By.CssSelector("#password"));
        var password2 = _driver.FindElement(By.CssSelector("#password2"));
        var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

        emailField.SendKeys(CommonResources.GetValue("email"));
        password.SendKeys(CommonResources.GetValue("password"));
        password2.SendKeys(CommonResources.GetValue("password"));
        submitButton.Click();

        //  Alert
        var successAlertOkButton = wait.Until(drv => drv.FindElement(By.CssSelector(".swal2-confirm.swal2-styled")));
        Assert.That(successAlertOkButton.Displayed);

    }
    [Test]
    [Order(2)]
    public void CanLoginOnFreshAccount()
    {
        //  Login form view
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        Actions actions = new(_driver);

        _driver!.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        _driver.FindElement(By.CssSelector("input[name='email']")).SendKeys(CommonResources.GetValue("email"));
        _driver.FindElement(By.CssSelector("input[name='password']")).SendKeys(CommonResources.GetValue("password"));
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        wait.Until(d => d.FindElement(By.CssSelector("button.activateAdministrator"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("input[name='name']"))).SendKeys(CommonResources.GetValue("name"));
        _driver.FindElement(By.CssSelector("input[name='surname']")).SendKeys(CommonResources.GetValue("surname"));
        _driver.FindElement(By.CssSelector("input[name='birthday']")).SendKeys(CommonResources.GetValue("birthday"));
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        wait.Until(d => d.FindElement(By.CssSelector("input[name='city']"))).SendKeys(CommonResources.GetValue("city"));
        _driver.FindElement(By.CssSelector("input[name='postalCode']")).SendKeys(CommonResources.GetValue("postalCode"));
        _driver.FindElement(By.CssSelector("input[name='addressLine1']")).SendKeys(CommonResources.GetValue("schoolAddress"));
        _driver.FindElement(By.CssSelector("input[name='name']")).SendKeys(CommonResources.GetValue("schoolName"));
        var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        actions.MoveToElement(submitButton).Perform();
        submitButton.Click();
        var profileButton = wait.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']")));
        Assert.That(profileButton.Displayed);
    }
    [Test]
    [Order(3)]
    public void ShouldShowNameAndSurnameOnProfileButton()
    {
        //  Login form view
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

        _driver!.Login(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
        var profileButton = wait.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']")));

        Assert.That($"{CommonResources.GetValue(key: "name")} {CommonResources.GetValue("surname")}", Is.EqualTo(profileButton.Text));
    }
    [Test]
    [Order(3)]
    public void ShouldShowNameAndSurnameOnProfileButtonAfterReload()
    {
        //  Login form view
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

        _driver!.Login(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
        _driver!.Navigate().Refresh();
        var profileButton = wait.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']")));

        Assert.That($"{CommonResources.GetValue(key: "name")} {CommonResources.GetValue("surname")}", Is.EqualTo(profileButton.Text));
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
