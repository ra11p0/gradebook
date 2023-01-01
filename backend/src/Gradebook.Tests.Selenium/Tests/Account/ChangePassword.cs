using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.QuickActionsExtensions;
using Gradebook.Tests.Selenium.Constraints.Views.Dashboard;

namespace Gradebook.Tests.Selenium.Tests.Account;

[Category("Selenium")]
[Order(5)]
public class ChangePassword
{
    private const string _newPassword = "!QAZ2wsx";
    private const string _secondNewPassword = "!QAZ2wsx!!!";
    [Test]
    [Order(1)]
    public void CanRequestChangePasswordEmail()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.GoToGradebookHomepage();
        driver.ClickOn("a[test-id='forgotPassword']");
        driver.WaitForMany("input[name='email']")[1].SendKeys(CommonResources.GetValue("email"));
        driver.WaitForMany("button[type='submit']")[1].Click();

        Assert.That(driver.Contains(Swal.SuccessRing));

        var remindPasswordLink = DatabaseHelper.GetChangePasswordLinkFromEmail(CommonResources.GetValue("email")!);

        driver.GoTo(remindPasswordLink);
        driver.WaitFor("input[name='newPassword']").SendKeys(_newPassword);
        driver.WaitFor("input[name='newPasswordConfirm']").SendKeys(_newPassword);
        driver.ClickOn("button[type='submit']");

        Assert.That(driver.Contains(Swal.SuccessRing));

        driver.Login(CommonResources.GetValue("email")!, _newPassword);
        driver.Logout();
        driver.ChangePassword(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
    }
    [Test]
    [Order(2)]
    public void CanChangePasswordInSettings()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
        driver.ClickOn(Common.SettingsButton);
        driver.ClickOn("button[test-id='changeSchoolButton']");

        driver.WaitFor("input[name='oldPassword']").SendKeys(CommonResources.GetValue("password")!);
        driver.WaitFor("input[name='newPasswordConfirm']").SendKeys(_secondNewPassword);
        driver.WaitFor("input[name='newPassword']").SendKeys(_secondNewPassword);
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.Contains(Swal.SuccessRing));

        driver.Logout();
        driver.Login(CommonResources.GetValue("email")!, _secondNewPassword);
        driver.Logout();
        driver.ChangePassword(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
    }
}
