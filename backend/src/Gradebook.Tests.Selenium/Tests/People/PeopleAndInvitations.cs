using Gradebook.Tests.Selenium.Constraints.Views.Dashboard;
using Gradebook.Tests.Selenium.Constraints.Views.Shared;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;
using StudentsView = Gradebook.Tests.Selenium.Constraints.Views.Dashboard.Students;

namespace Gradebook.Tests.Selenium.Tests.People;

[Category("Selenium")]
[Order(2)]
public class PeopleAndInvitations
{
    private readonly Dictionary<string, string> _storage = new();
    public PeopleAndInvitations()
    {
        _storage["studentName"] = "Amelia";
        _storage["studentSurname"] = "Zielnickka";
        _storage["studentBirthday"] = "09/03/2005";
        _storage["studentEmail"] = "amelia.zielnicka@szkola.pl";
        _storage["studentPassword"] = "!QAZ2wsx";
    }

    [Test]
    [Order(1)]
    public void CanCreateNewStudent()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.ClickOn(Common.ManageStudentsButton);
        driver.ClickOn(StudentsView.NewStudentButton);
        driver.WaitFor("input[name='name']").SendKeys(_storage["studentName"]);
        driver.WaitFor("input[name='surname']").SendKeys(_storage["studentSurname"]);
        driver.WaitFor("input.birthday").ClearElement().SendKeys(_storage["studentBirthday"]);
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.WaitFor("tbody").ContainsText(_storage["studentName"]));
        Assert.That(driver.WaitFor("tbody").ContainsText(_storage["studentSurname"]));
    }

    [Test]
    [Order(2)]
    public void CanInviteStudent()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.ClickOn(Common.InvitationsButton);
        driver.ClickOn("button.addInvitationButton");
        driver.WaitFor(PeoplePicker.SearchQueryInput).SendKeys(_storage["studentName"]);
        driver.ClickOn($"[data-person-full-name='{_storage["studentName"] + " " + _storage["studentSurname"]}']");
        Assert.That(driver.WaitFor(".selected-people .person-element", 10).ContainsText(_storage["studentName"] + " " + _storage["studentSurname"]));
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.WaitForElementContaining(_storage["studentName"] + " " + _storage["studentSurname"]).Displayed);

        var invitationCode = driver
            .WaitForElementContaining(_storage["studentName"] + " " + _storage["studentSurname"], 10)
            .Parent("tr")
            .Children(".invitation-code")
            .First().Text.Trim();

        _storage["newStudentInvitationCode"] = invitationCode;
        Assert.That(!string.IsNullOrEmpty(invitationCode));
    }

    [Test]
    [Order(3)]
    public void CanRegisterAsStudent()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Register(_storage["studentEmail"], _storage["studentPassword"]);
        driver.ClickOn("button.activateStudent");
        driver.WaitFor("input[name='accessCode']").SendKeys(_storage["newStudentInvitationCode"]);
        Assert.That(driver.WaitFor($"input[value='{_storage["studentName"]}']").Displayed);
        Assert.That(driver.WaitFor($"input[value='{_storage["studentSurname"]}']").Displayed);
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.WaitFor("a[href='/account/profile']").Displayed);
    }

    [Test]
    public void CanAddTeacher()
    {
        const string teacherName = "Mariusz";
        const string teacherSurname = "Tracz";
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.GoToTeachersTab();
        driver.ClickOn("[test-id='addNewTeacherButton']");
        driver.WaitFor("input[name='name']").SendKeys(teacherName);
        driver.WaitFor("input[name='surname']").SendKeys(teacherSurname);
        driver.WaitFor("input[name='birthday']").ClearElement().SendKeys("02.02.1992");
        driver.ClickOn("button[type='submit']");

        Assert.That(driver.WaitFor("tbody", e => e.ContainsText(teacherName)), "Could not find teacher name in teachers list");
        Assert.That(driver.WaitFor("tbody", e => e.ContainsText(teacherSurname)), "Could not find teacher surname in teachers list");
    }

}
