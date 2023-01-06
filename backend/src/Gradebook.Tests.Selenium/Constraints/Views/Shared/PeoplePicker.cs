using Gradebook.Tests.Selenium.IWebDriverExtensions;

namespace Gradebook.Tests.Selenium.Constraints.Views.Shared;

public static class PeoplePicker
{
    public static readonly string SearchQueryInput = "#searchQueryInput";
    public static IWebElement SelectPerson(this IWebDriver driver, string personFullName)
    {
        driver.WaitFor(PeoplePicker.SearchQueryInput).ClearElement().SendKeys(personFullName);
        var el = driver.ClickOn($"[data-person-full-name='{personFullName}']");
        return el;
    }
}
