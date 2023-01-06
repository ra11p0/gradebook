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
    [Test]
    public void ShouldAddClass()
    {
        const string className = "2016-2020";
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);

        driver.ClickOn(Common.ClassesButton);
        driver.ClickOn(ClassesView.AddClassButton);

        driver.WaitFor(ClassesView.AddClass_ClassNameField).SendKeys(className);
        driver.ClickOn("button[type='submit']");
        driver.WaitForSuccessNotification();
        driver.Refresh();

        Assert.That(driver.WaitFor("tbody").ContainsText(className));
    }
    [Test]
    public void ShouldAddClassOwner()
    {
        const string className = "2017-2021";
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver
            .Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!)
            .AddNewTeacher("Mateusz", "Grzegorzewski", new DateTime(1990, 3, 14))
            .AddNewClass(className);
        driver.ClickOn(Common.ClassesButton);
        driver.WaitForElementContaining(className, 10).Parent("tr").Click();
        driver.ClickOn(ClassView.ManageClassOwners);
        driver.SelectPerson("Mateusz Grzegorzewski");
        driver.ClickOn("button[type='submit']");
        driver.Refresh();
        driver.WaitForElementContaining("Mateusz Grzegorzewski").Parent("a").Click();

        Assert.That(driver.WaitFor("#managedClassesList").ContainsText(className));

    }
    [Test]
    public void ShouldAddStudentToClass()
    {
        const string className = "2018-2022";
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver
            .Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!)
            .AddNewStudent("Tomasz", "Włodarczyk", new DateTime(2001, 2, 2))
            .AddNewClass(className);

        driver.ClickOn(Common.ClassesButton);
        driver.WaitForElementContaining(className, 10).Parent("tr").Click();
        driver.ClickOn(ClassView.ManageClassStudents);
        driver.SelectPerson("Tomasz Włodarczyk");
        driver.ClickOn("button[type='submit']");
        driver.Refresh();
        driver.WaitForElementContaining("Włodarczyk").Parent(".cursor-pointer").Click();
        driver.WaitForElementContaining(className).Parent("a").Click();

        Assert.That(driver.WaitFor("h2", e => e.ContainsText(className)));
    }
    [Test]
    public void ShouldAddNotShowStudentInOtherClassesWhenAlreadyInOne()
    {
        const string className = "2010-2014-a";
        const string className2 = "2009-2013-b";
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver
            .Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!)
            .AddNewStudent("Klaudiusz", "Michalski", new DateTime(2001, 1, 12))
            .AddNewClass(className)
            .AddNewClass(className2)
            .AddStudentToClass(className, "Klaudiusz", "Michalski")
            .GoToGradebookHomepage()
            .ClickOn(Common.ClassesButton);
        driver.WaitForElementContaining(className2, 10).Parent("tr").Click();
        driver.ClickOn(ClassView.ManageClassStudents);
        Assert.That(
            driver
            .WithTimeout(5)
            .FindElements(By.CssSelector("[data-person-full-name='Klaudiusz Michalski']"))
            .Any()
            , Is.False);
    }
}
