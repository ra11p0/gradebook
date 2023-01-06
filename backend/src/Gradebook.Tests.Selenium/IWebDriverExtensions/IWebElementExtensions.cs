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
        const int timeoutLimit = 10;
        int timeout = 0;
        var contains = false;
        do
        {
            contains = el.GetAttribute("innerText").Contains(innerText);
            timeout++;
            if (timeout > timeoutLimit) break;

        } while (!contains);
        return contains;
    }

    public static IWebElement Parent(this IWebElement el)
    {
        var parent = el.FindElement(By.XPath("./.."));
        return parent;
    }

    public static IWebElement Parent(this IWebElement el, string cssSelector)
    {
        const int maxDepth = 100;
        int i = 0;
        IWebElement parent = el;
        do
        {
            parent = parent.Parent();
            var targetParent = parent.Children(cssSelector).FirstOrDefault();
            if (targetParent is not null) return targetParent;
            i++;
        }
        while (maxDepth > i);
        throw new Exception("Parent " + cssSelector + " not found.");
    }

    public static IEnumerable<IWebElement> Children(this IWebElement el)
    {
        var children = el.FindElements(By.XPath(".//*"));
        return children;
    }

    public static IEnumerable<IWebElement> Children(this IWebElement el, string cssSelector)
    {
        return el.FindElements(By.CssSelector(cssSelector));
    }
}
