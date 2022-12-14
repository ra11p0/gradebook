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
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.GoToGradebookHomepage();
        driver.FindElement(By.CssSelector("input[name='email']")).SendKeys(email);
        driver.FindElement(By.CssSelector("input[name='password']")).SendKeys(password);
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        wait.Until(d => d.FindElement(By.CssSelector("a.logoutButton")));
        return driver;
    }
    public static IWebDriver Logout(this IWebDriver driver)
    {
        driver.GoToGradebookHomepage();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(drv => drv.FindElement(By.CssSelector("a.logoutButton"))).Click();
        Thread.Sleep(1000);
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
    {
        driver.Navigate().GoToUrl(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        return driver;
    }
    public static IWebDriver GoToInvitationsTab(this IWebDriver driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.GoToGradebookHomepage();
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageInvitations']"))).Click();
        return driver;
    }
    public static IWebDriver GoToSchoolsTab(this IWebDriver driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        driver.GoToGradebookHomepage();
        wait.Until(d => d.FindElement(By.CssSelector("a[href='/manageSchool']"))).Click();
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
