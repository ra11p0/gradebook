namespace Gradebook.Tests.Selenium.IWebDriverExtensions;

public static class IWebElementExtensions
{
    public static IWebElement ClearElement(this IWebElement el)
    {
        el.SelectAll().SendKeys(Keys.Delete);
        return el;
    }

    public static IWebElement SelectAll(this IWebElement el)
    {
        el.Click();
        if (ConfigurationManager.GetValue("Browser:Platform") == "macos")
            el.SendKeys(Keys.Command + "a");
        else
            el.SendKeys(Keys.Control + "a");
        return el;
    }
    public static bool ContainsText(this IWebElement el, string innerText)
    {
        const int timeoutLimit = 5;
        int timeout = 0;
        var contains = false;
        do
        {
            contains = el.GetAttribute("innerText").Contains(innerText);
            timeout++;
            if (timeout > timeoutLimit) break;
            Thread.Sleep(1000);
        } while (!contains);
        return contains;
    }
}
