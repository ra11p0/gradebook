using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Account;

[Category("Selenium")]
[Order(1)]
public class RegisterAndLogin
{
    [Test]
    [Order(1)]
    public void ShouldRegisterNewUser()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.GoTo(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        driver.ClickOn("a[href='/register']");
        driver.WaitFor("#email").SendKeys(CommonResources.GetValue("email"));
        driver.WaitFor("#password").SendKeys(CommonResources.GetValue("password"));
        driver.WaitFor("#password2").SendKeys(CommonResources.GetValue("password"));
        driver.WaitFor("#termsAndConditions").Click();
        driver.ClickOn("button[type='submit']");
        var link = DatabaseHelper.GetActivationLinkForEmail(CommonResources.GetValue("email")!);
        driver.GoTo(link);
        Assert.That(driver.Contains(".swal2-success-ring"));
        driver.ClickOn(".swal2-confirm");
        driver.WaitFor("#email").SendKeys(CommonResources.GetValue("email"));
        driver.WaitFor("#password").SendKeys(CommonResources.GetValue("password"));
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.Contains("#logOutButton"));
    }
    [Test]
    [Order(2)]
    public void CanRegisterNewSchool()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
        driver.ClickOn("button.activateAdministrator");
        driver.WaitFor("#name").SendKeys(CommonResources.GetValue("name")!);
        driver.WaitFor("#surname").SendKeys(CommonResources.GetValue("surname")!);
        driver.WaitFor("input.birthday").SelectAll().SendKeys(CommonResources.GetValue("birthday")!);
        driver.ClickOn("button[type='submit']");
        driver.WaitFor("#name").SendKeys(CommonResources.GetValue("schoolName")!);
        driver.WaitFor("#addressLine1").SendKeys(CommonResources.GetValue("schoolAddress")!);
        driver.WaitFor("#postalCode").SendKeys(CommonResources.GetValue("postalCode")!);
        driver.WaitFor("#city").SendKeys(CommonResources.GetValue("city")!);
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.Contains("a.nav-link[href='/account/profile']"));
        Assert.That(
            driver.WaitFor("a.nav-link[href='/account/profile']")
            .ContainsText(CommonResources.GetValue("name")! + " " + CommonResources.GetValue("surname")!));
    }
}
