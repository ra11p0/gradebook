using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;
using Gradebook.Tests.Selenium.Constraints.Views.Dashboard;
using ClassesView = Gradebook.Tests.Selenium.Constraints.Views.Dashboard.Classes;
using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.Constraints.Views.Shared;

namespace Gradebook.Tests.Selenium.Tests.Classes;

[Category("Selenium")]
[Order(7)]
public class Classes
{
    private const string _className = "2016-2020";
    [Test]
    [Order(1)]
    public void ShouldAddClass()
    {

        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);

        driver.ClickOn(Common.ClassesButton);
        driver.ClickOn(ClassesView.AddClassButton);

        driver.WaitFor(ClassesView.AddClass_ClassNameField).SendKeys(_className);
        driver.ClickOn("button[type='submit']");
        driver.WaitForSuccessNotification();
        driver.Refresh();

        Assert.That(driver.WaitFor("tbody").ContainsText(_className));
    }
    [Test]
    [Order(1)]
    public void ShouldAddClassOwner()
    {

        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);

        driver.ClickOn(Common.ClassesButton);
        driver.WaitForElementContaining(_className).Parent("tr").Click();
        driver.ClickOn(ClassView.ManageClassOwners);
        driver.SelectPerson("Mariusz Tracz");
        driver.ClickOn("button[type='submit']");
        driver.Refresh();
        driver.WaitForElementContaining("Mariusz Tracz").Parent("a").Click();

        Assert.That(driver.WaitFor("#managedClassesList").ContainsText(_className));

    }
}
