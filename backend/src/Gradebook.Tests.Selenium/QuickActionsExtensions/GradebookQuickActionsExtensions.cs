using Gradebook.Tests.Selenium.IWebDriverExtensions;

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
        driver.WaitFor("#email").SendKeys(email);
        driver.WaitFor("#password").SendKeys(password);
        driver.ClickOn("button[type='submit']");
        driver.WaitFor("#logOutButton");
        return driver;
    }
    public static IWebDriver Logout(this IWebDriver driver)
    {
        driver.GoToGradebookHomepage();
        driver.ClickOn("#logOutButton");
        return driver;
    }
    public static IWebDriver Register(this IWebDriver driver, string email, string password)
    {
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

        wait.Until(drv => drv.FindElement(By.CssSelector(".swal2-confirm.swal2-styled")));
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
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        driver.GoToGradebookHomepage();
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageStudents']"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("button.addNewStudentButton"))).Click();
        wait.Until(d => d.FindElement(By.CssSelector("input[name='name']"))).SendKeys(studentName);
        driver!.FindElement(By.CssSelector("input[name='surname']")).SendKeys(studentSurname);
        driver!.FindElement(By.CssSelector("input.birthday")).SendKeys(Keys.Control + 'a' + Keys.Delete);
        driver!.FindElement(By.CssSelector("input.birthday")).SendKeys(studentBirthday);
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        wait.Until(d => d.FindElement(By.XPath($"//div[text()='{studentName}']")));
        wait.Until(d => d.FindElement(By.XPath($"//div[text()='{studentSurname}']")));
        driver.GoToGradebookHomepage();
        return driver;
    }
}
