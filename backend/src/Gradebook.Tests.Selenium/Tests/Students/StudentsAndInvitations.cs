using Gradebook.Tests.Selenium.Constraints.Views.Dashboard;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;
using StudentsView = Gradebook.Tests.Selenium.Constraints.Views.Dashboard.Students;

namespace Gradebook.Tests.Selenium.Tests.Students;

[Category("Selenium")]
[Order(2)]
public class StudentsAndInvitations
{
    private readonly Dictionary<string, string> _storage = new();
    public StudentsAndInvitations()
    {
        _storage["studentName"] = "Mateusz";
        _storage["studentSurname"] = "Kowalczyk";
        _storage["studentBirthday"] = "09/03/2005";
        _storage["studentEmail"] = "mateusz.kowalczyiiik@szkola.pl";
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
        // wybieraczka do zmiany
        /* using var driver = WebDriverBuilder.BuildWebDriver();


         //var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
         //Actions actions = new(_driver);
         driver.ClickOn("a[href='/dashboard/manageInvitations']");


         wait.Until(d => d.FindElement(By.CssSelector("button.addInvitationButton"))).Click();
         wait.Until(d => d.FindElement(By.CssSelector(".selectPeopleToInvite"))).Click();
         wait.Until(d => d.FindElement(By.XPath($"//li/a/div/div/div/div[text()='{_storage["studentName"]}']"))).Click();
         actions.SendKeys(Keys.Escape).Perform();
         _driver!.FindElement(By.CssSelector(".modal-footer > button[type='button']")).Click();
         var studentNameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentName"]}']")));
         var studentSurnameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentSurname"]}']")));
         var invitationCode = _driver.FindElement(By.XPath($"//div[text()='{_storage["studentSurname"]}']/../../../../../../div")).Text;
         _storage["newStudentInvitationCode"] = invitationCode;
         Assert.That(!string.IsNullOrEmpty(invitationCode));*/
    }
    [Test]
    [Order(3)]
    public void CanRegisterAsStudent()
    {
        //  wybieraczka do zmiany
        /*
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Register(_storage["studentEmail"], _storage["studentPassword"]);
        driver.Login(_storage["studentEmail"], _storage["studentPassword"]);
        driver.ClickOn("button.activateStudent");
        driver.WaitFor("input[name='accessCode']").SendKeys(_storage["newStudentInvitationCode"]);
        Assert.That(driver.WaitFor($"input[value='{_storage["studentName"]}']").Displayed);
        Assert.That(driver.WaitFor($"input[value='{_storage["studentSurname"]}']").Displayed);
        driver.ClickOn("button[type='submit']");
        Assert.That(driver.WaitFor("a[href='/account/profile']").Displayed);*/
    }
}
