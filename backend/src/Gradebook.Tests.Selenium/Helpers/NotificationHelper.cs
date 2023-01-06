using Gradebook.Tests.Selenium.IWebDriverExtensions;

namespace Gradebook.Tests.Selenium.Helpers;

public static class NotificationHelper
{
    public static IWebDriver WaitForSuccessNotification(this IWebDriver driver)
    {
        driver.WaitFor(".rnc__notification-item--success");
        return driver;
    }
}
