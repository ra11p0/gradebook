using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;
using SchoolsView = Gradebook.Tests.Selenium.Constraints.Views.Dashboard.Schools;

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
        driver.ClickOn(SchoolsView.AddSchoolButton);
        driver.ClickOn(RegisterPerson.RegisterAdministratorButton);
        driver.WaitFor(RegisterPerson.SchoolAddressCity).SendKeys(CommonResources.GetValue("secondSchoolCity"));
        driver.WaitFor(RegisterPerson.SchoolAddressPostalCode).SendKeys(CommonResources.GetValue("secondSchoolPostalCode"));
        driver.WaitFor(RegisterPerson.SchoolAddressLine1Field).SendKeys(CommonResources.GetValue("secondSchoolAddress"));
        driver.WaitFor(RegisterPerson.SchoolNameField).SendKeys(CommonResources.GetValue("secondSchoolName"));
        driver.ClickOn(RegisterPerson.SubmitButton);
        Assert.That(driver.HasSelect2Option(Header.SchoolSelect, CommonResources.GetValue("secondSchoolName")!));
        driver.SelectSelect2Option(Header.SchoolSelect, CommonResources.GetValue("secondSchoolName")!);
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolName")!));
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolAddress")!));
        Assert.That(driver.WaitFor("tbody").ContainsText(CommonResources.GetValue("secondSchoolPostalCode")!));
        Assert.That(driver.WaitFor(Header.SchoolSelect).ContainsText(CommonResources.GetValue("secondSchoolName")!));
    }
}
