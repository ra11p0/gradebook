using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using LoginView = Gradebook.Tests.Selenium.Constraints.Views.Login;

namespace Gradebook.Tests.Selenium.QuickActionsExtensions;

public static class GradebookQuickActionsExtensions
{
    public static IWebDriver ScrollTo(this IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
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
        // nowe akcje drivera
        /*
        driver.GoToGradebookHomepage();

        //  Login form view
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var registerButton = driver.FindElement(By.CssSelector("a[href='/register']"));
        registerButton.Click();

        //  RegisterAndLogin form view
        wait.Until(drv => drv.FindElement(By.CssSelector("form")));
        var emailField = driver.FindElement(By.CssSelector("#email"));
        var passwordField = driver.FindElement(By.CssSelector("#password"));
        var password2 = driver.FindElement(By.CssSelector("#password2"));
        var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

        emailField.SendKeys(email);
        passwordField.SendKeys(password);
        password2.SendKeys(password);
        submitButton.Click();

        wait.Until(drv => drv.FindElement(By.CssSelector(".swal2-confirm.swal2-styled")));*/
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
    public static IWebDriver GoToSchoolsTab(this IWebDriver driver)
    {
        driver.GoTo(ConfigurationManager.GetValue("Urls:ApplicationUrl") + "dashboard/manageSchool");
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
        Assert.That(driver.WaitFor("tbody").ContainsText(studentName));
        Assert.That(driver.WaitFor("tbody").ContainsText(studentSurname));
        driver.GoToGradebookHomepage();
        return driver;
    }
}
