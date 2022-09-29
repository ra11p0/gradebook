using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Students;

[Order(2)]
public class StudentsAndInvitations
{
    private readonly Dictionary<string, string> _storage = new();
    private IWebDriver? _driver;
    [SetUp]
    public void Setup()
    {
        _driver = WebDriverBuilder.BuildWebDriver();
        _driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        // Set up storage
        _storage["studentName"] = "Mateusz";
        _storage["studentSurname"] = "Kowalczyk";
        _storage["studentBirthday"] = "09032005";
        _storage["studentEmail"] = "mateusz.kowalczyk@szkola.pl";
        _storage["studentPassword"] = "!QAZ2wsx";
    }
    [Test]
    [Order(1)]
    public void CanCreateNewStudent()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageStudents']"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("button.addNewStudentButton"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("input[name='name']"))).SendKeys(_storage["studentName"]);
        _driver!.FindElement(By.CssSelector("input[name='surname']")).SendKeys(_storage["studentSurname"]);
        _driver!.FindElement(By.CssSelector("input[name='birthday']")).SendKeys(_storage["studentBirthday"]);
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        var studentNameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentName"]}']")));
        var studentSurnameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentSurname"]}']")));
        Assert.That(studentNameRecord.Displayed && studentSurnameRecord.Displayed);
    }
    [Test]
    [Order(2)]
    public void CanInviteStudent()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        Actions actions = new(_driver);
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageInvitations']"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("button.addInvitationButton"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector(".selectPeopleToInvite"))).Click();
        wait.Until(d => d.FindElement(By.XPath($"//li/a/div/div/div/div[text()='{_storage["studentName"]}']"))).Click();
        actions.SendKeys(Keys.Escape).Perform();
        _driver!.FindElement(By.CssSelector(".modal-footer > button[type='button']")).Click();
        var studentNameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentName"]}']")));
        var studentSurnameRecord = wait.Until(d => d.FindElement(By.XPath($"//div[text()='{_storage["studentSurname"]}']")));
        var invitationCode = _driver.FindElement(By.XPath($"//div[text()='{_storage["studentSurname"]}']/../../../../../../div")).Text;
        _storage["newStudentInvitationCode"] = invitationCode;
        Assert.That(!string.IsNullOrEmpty(invitationCode));
    }
    [Test]
    [Order(3)]
    public void CanRegisterAsStudent()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

        _driver!.Logout();
        _driver!.Register(_storage["studentEmail"], _storage["studentPassword"]);
        _driver!.Login(_storage["studentEmail"], _storage["studentPassword"]);

        wait.Until(d => d.FindElement(By.CssSelector("button.activateStudent"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("input[name='accessCode']"))).SendKeys(_storage["newStudentInvitationCode"]);
        wait.Until(d => d.FindElement(By.CssSelector($"input[value='{_storage["studentName"]}']")));
        wait.Until(d => d.FindElement(By.CssSelector($"input[value='{_storage["studentSurname"]}']")));
        _driver!.FindElement(By.CssSelector("button[type='submit']")).Click();
        var profileButton = wait.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']")));
        Assert.That(profileButton.Displayed);
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
