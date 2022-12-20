using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Schools;

[Category("Selenium")]
[Order(2)]
public class Schools
{
    [Test]
    [Order(1)]
    public void CanAddNewSchool()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.GoToSchoolsTab();
        driver.ClickOn(".addSchoolButton");
        driver.ClickOn(".activateAdministrator");
        driver.WaitFor("input[name='city']").SendKeys(CommonResources.GetValue("secondSchoolCity"));
        driver.WaitFor("input[name='postalCode']").SendKeys(CommonResources.GetValue("secondSchoolPostalCode"));
        driver.WaitFor("input[name='addressLine1']").SendKeys(CommonResources.GetValue("secondSchoolAddress"));
        driver.WaitFor("input[name='name']").SendKeys(CommonResources.GetValue("secondSchoolName"));
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.HasSelect2Option("#schoolSelect", CommonResources.GetValue("secondSchoolName")!));
        driver.SelectSelect2Option("#schoolSelect", CommonResources.GetValue("secondSchoolName")!);
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolName")!));
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolAddress")!));
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolPostalCode")!));
        Assert.That(driver.WaitFor("#schoolSelect").ContainsText(CommonResources.GetValue("secondSchoolName")!));
    }
}
