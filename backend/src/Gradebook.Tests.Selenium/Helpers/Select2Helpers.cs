using Gradebook.Tests.Selenium.IWebDriverExtensions;

namespace Gradebook.Tests.Selenium.Helpers;

public static class Select2Helpers
{
    public static IEnumerable<string> GetSelect2Options(this IWebDriver driver, string select2CssSelector)
    {
        driver.ClickOn(select2CssSelector);
        var options = driver.FindElements(By.CssSelector("#react-select-2-listbox > div > div")).Select(e => e.GetAttribute("innerText")).ToList();
        driver.ClickOn(select2CssSelector);
        return options;
    }
    public static void SelectSelect2Option(this IWebDriver driver, string select2CssSelector, string option)
    {
        var el = driver.ClickOn(select2CssSelector);
        driver.WaitFor("#react-select-2-listbox > div > div");
        el.FindElements(By.CssSelector("#react-select-2-listbox > div > div")).First(e => e.GetAttribute("innerText").Contains(option)).Click();
    }
    public static bool HasSelect2Option(this IWebDriver driver, string select2CssSelector, string option)
        => driver.GetSelect2Options(select2CssSelector).Any(e => e.Contains(option));

}
