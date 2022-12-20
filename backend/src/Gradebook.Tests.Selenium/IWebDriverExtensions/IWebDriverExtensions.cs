using System.Collections.ObjectModel;

namespace Gradebook.Tests.Selenium.IWebDriverExtensions;

public static class IWebDriverExtensions
{
    public static IWebDriver ScrollTo(this IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        Thread.Sleep(500);
        return driver;
    }

    public static IWebElement ClickOn(this IWebDriver driver, string cssSelector, int timeoutSeconds = 5)
    {
        var el = driver.WaitFor(cssSelector);
        driver.ScrollTo(el);
        el.Click();
        return el;
    }

    public static IWebElement WaitFor(this IWebDriver driver, string cssSelector, int timeoutSeconds = 5)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var el = wait.Until(d => d.FindElement(By.CssSelector(cssSelector)));
        return el;
    }
    public static ReadOnlyCollection<IWebElement> WaitForMany(this IWebDriver driver, string cssSelector, int timeoutSeconds = 5)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var el = wait.Until(d => d.FindElements(By.CssSelector(cssSelector)));
        return el;
    }

    public static bool Contains(this IWebDriver driver, string cssSelector, int timeoutSeconds = 5)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var el = wait.Until(d => d.FindElement(By.CssSelector(cssSelector)));
        return el is not null;
    }

    public static IWebDriver GoTo(this IWebDriver driver, string url)
    {
        driver!.Navigate().GoToUrl(url);
        return driver;
    }
}
