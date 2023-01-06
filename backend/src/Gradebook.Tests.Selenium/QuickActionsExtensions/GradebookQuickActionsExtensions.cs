using Gradebook.Foundation.Common;
using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.Constraints.Views.Dashboard;
using Gradebook.Tests.Selenium.Constraints.Views.Shared;
using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using LoginView = Gradebook.Tests.Selenium.Constraints.Views.Login;
using RegisterView = Gradebook.Tests.Selenium.Constraints.Views.Register;

namespace Gradebook.Tests.Selenium.QuickActionsExtensions;

public static class GradebookQuickActionsExtensions
{
    public static IWebDriver ScrollTo(this IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'instant', block: 'center', inline: 'center'});", element);

        Thread.Sleep(500);
        return driver;
    }
    public static IWebDriver Login(this IWebDriver driver, string email, string password)
    {
        driver.GoToGradebookHomepage();
        driver.WaitFor(LoginView.EmailField).SendKeys(email);
        driver.WaitFor(LoginView.PasswordField).SendKeys(password);
        driver.ClickOn(LoginView.SubmitButton);
        driver.WaitFor(Header.LogOutButton);
        return driver;
    }
    public static IWebDriver Logout(this IWebDriver driver)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn(Header.LogOutButton);
        return driver;
    }
    public static IWebDriver Register(this IWebDriver driver, string email, string password)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn(LoginView.RegisterButton);
        driver.WaitFor(RegisterView.EmailField).SendKeys(email);
        driver.WaitFor(RegisterView.PasswordField).SendKeys(password);
        driver.WaitFor(RegisterView.Password2Field).SendKeys(password);
        driver.WaitFor(RegisterView.TermsAndConditionsSwitch).Click();
        driver.ClickOn(RegisterView.SubmitButton);
        var link = DatabaseHelper.GetActivationLinkFromEmail(email);
        driver.GoTo(link);
        Assert.That(driver.Contains(Swal.SuccessRing));
        driver.ClickOn(Swal.ConfirmButton);
        driver.WaitFor(LoginView.EmailField).SendKeys(email);
        driver.WaitFor(LoginView.PasswordField).SendKeys(password);
        driver.ClickOn(LoginView.SubmitButton);
        Assert.That(driver.Contains(Header.LogOutButton));
        return driver;
    }
    public static IWebDriver GoToGradebookHomepage(this IWebDriver driver)
        => driver.GoTo(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
    public static IWebDriver GoToInvitationsTab(this IWebDriver driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.GoToGradebookHomepage();
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageInvitations']"))).Click();
        return driver;
    }
    public static IWebDriver GoToTeachersTab(this IWebDriver driver)
    {
        driver.GoTo($"{ConfigurationManager.GetValue("Urls:ApplicationUrl")}dashboard/manageTeachers");
        return driver;
    }
    public static IWebDriver GoToSchoolsTab(this IWebDriver driver)
    {
        driver.GoTo(ConfigurationManager.GetValue("Urls:ApplicationUrl") + "dashboard/manageSchool");
        return driver;
    }
    public static IWebDriver ChangePassword(this IWebDriver driver, string email, string newPassword)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn("a[test-id='forgotPassword']");
        driver.WaitForMany("input[name='email']")[1].SendKeys(email);
        var timestamp = Time.UtcNow;
        driver.WaitForMany("button[type='submit']")[1].Click();

        Assert.That(driver.Contains(Swal.SuccessRing));

        var remindPasswordLink = DatabaseHelper.GetChangePasswordLinkFromEmail(email, timestamp);

        driver.GoTo(remindPasswordLink);
        driver.WaitFor("input[name='newPassword']").SendKeys(newPassword);
        driver.WaitFor("input[name='newPasswordConfirm']").SendKeys(newPassword);
        driver.ClickOn("button[type='submit']");

        Assert.That(driver.Contains(Swal.SuccessRing));

        return driver;
    }
    public static IWebDriver AddNewStudent(this IWebDriver driver, string studentName, string studentSurname, string studentBirthday)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn("a[href='/dashboard/manageStudents']");
        driver.ClickOn("button.addNewStudentButton");
        driver.WaitFor("input[name='name']").SendKeys(studentName);
        driver.WaitFor("input[name='surname']").SendKeys(studentSurname);
        driver.WaitFor("input.birthday").SelectAll().SendKeys(studentBirthday);
        driver.ClickOn("button[type='submit']");
        driver.WaitForSuccessNotification();
        driver.Refresh();
        Assert.That(driver.WaitFor("tbody").ContainsText(studentName));
        Assert.That(driver.WaitFor("tbody").ContainsText(studentSurname));
        driver.GoToGradebookHomepage();
        return driver;
    }
    public static IWebDriver AddNewStudent(this IWebDriver driver, string studentName, string studentSurname, DateTime studentBirthday)
        => driver.AddNewStudent(studentName, studentSurname, studentBirthday.ToString("dd.MM.yyyy"));
    public static IWebDriver AddNewClass(this IWebDriver driver, string className)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn(Common.ClassesButton);
        driver.ClickOn(Classes.AddClassButton);

        driver.WaitFor(Classes.AddClass_ClassNameField).SendKeys(className);
        driver.ClickOn("button[type='submit']");
        driver.WaitForSuccessNotification();
        driver.Refresh();

        Assert.That(driver.WaitFor("tbody").ContainsText(className));

        return driver;
    }
    public static IWebDriver AddNewTeacher(this IWebDriver driver, string teacherName, string teacherSurname, DateTime birthday)
    {
        driver.GoToGradebookHomepage();
        driver.GoToTeachersTab();
        driver.ClickOn("[test-id='addNewTeacherButton']");
        driver.WaitFor("input[name='name']").SendKeys(teacherName);
        driver.WaitFor("input[name='surname']").SendKeys(teacherSurname);
        driver.WaitFor("input[name='birthday']").ClearElement().SendKeys(birthday.ToString("dd.MM.yyyy"));
        driver.ClickOn("button[type='submit']");
        driver.WaitForSuccessNotification();
        driver.Refresh();
        Assert.That(driver.WaitFor("tbody", e => e.ContainsText(teacherName)), "Could not find teacher name in teachers list");
        Assert.That(driver.WaitFor("tbody", e => e.ContainsText(teacherSurname)), "Could not find teacher surname in teachers list");
        return driver;
    }

    public static IWebDriver AddStudentToClass(this IWebDriver driver, string className, string studentName, string studentSurname)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn(Common.ClassesButton);
        driver.WaitForElementContaining(className).Parent("tr").Click();
        driver.ClickOn(ClassView.ManageClassStudents);
        driver.SelectPerson($"{studentName} {studentSurname}");
        driver.ClickOn("button[type='submit']");
        driver.Refresh();
        driver.WaitForElementContaining(studentSurname).Parent(".cursor-pointer").Click();
        driver.WaitForElementContaining(className).Parent("a").Click();

        Assert.That(driver.WaitFor("h2", e => e.ContainsText(className)));
        return driver;
    }
}
