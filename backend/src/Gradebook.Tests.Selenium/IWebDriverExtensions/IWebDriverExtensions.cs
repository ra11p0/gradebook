using System.Collections.ObjectModel;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.IWebDriverExtensions;

public static class IWebDriverExtensions
{
    public static WebDriverWait GetWait(this IWebDriver driver, int timeoutSeconds = -1)
        => new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds <= 0 ? 15 : timeoutSeconds));

    public static IWebElement ClickOn(this IWebDriver driver, string cssSelector, int timeoutSeconds = -1)
    {
        var el = driver.WaitFor(cssSelector);
        driver.ScrollTo(el);
        el.Click();
        return el;
    }
    public static IWebElement ClickOnElementContaining(this IWebDriver driver, string text, int timeoutSeconds = -1)
    {
        var el = driver.WaitForXpath($"//*[text()='{text}']");
        driver.ScrollTo(el);
        el.Click();
        return el;
    }
    public static IWebElement WaitForElementContaining(this IWebDriver driver, string text, int timeoutSeconds = -1)
    {
        var el = driver.WaitForXpath($"//*[text()='{text}']");
        return el;
    }

    public static IWebElement WaitFor(this IWebDriver driver, string cssSelector, int timeoutSeconds = -1)
    {
        var el = driver.GetWait(timeoutSeconds).Until(d => d.FindElement(By.CssSelector(cssSelector)));
        return el;
    }

    public static IWebElement WaitForXpath(this IWebDriver driver, string xpath, int timeoutSeconds = -1)
    {
        var el = driver.GetWait(timeoutSeconds).Until(d => d.FindElement(By.XPath(xpath)));
        return el;
    }

    public static T WaitFor<T>(this IWebDriver driver, string cssSelector, Func<IWebElement, T> waitFunc, int timeoutSeconds = -1)
    {
        var res = driver.GetWait(timeoutSeconds).Until(e => waitFunc(e.FindElement(By.CssSelector(cssSelector))));
        return res;
    }

    public static ReadOnlyCollection<IWebElement> WaitForMany(this IWebDriver driver, string cssSelector, int timeoutSeconds = -1)
    {
        var el = driver.GetWait(timeoutSeconds).Until(d => d.FindElements(By.CssSelector(cssSelector)));
        return el;
    }

    public static bool Contains(this IWebDriver driver, string cssSelector, int timeoutSeconds = -1)
    {
        var el = driver.GetWait(timeoutSeconds).Until(d => d.FindElement(By.CssSelector(cssSelector)));
        return el is not null;
    }

    public static IWebDriver GoTo(this IWebDriver driver, string url)
    {
        driver!.Navigate().GoToUrl(url);
        return driver;
    }

    public static IWebDriver Refresh(this IWebDriver driver)
    {
        driver.Navigate().Refresh();
        return driver;
    }
    public static IWebDriver WithTimeout(this IWebDriver driver, int timeoutInSeconds)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        return driver;
    }
}
