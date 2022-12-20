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
        return el.GetAttribute("innerText").Contains(innerText);
    }
}
