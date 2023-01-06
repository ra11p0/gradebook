using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Permissions;

[Category("Selenium")]
[Order(3)]
public class Permissions
{
    [Test]
    public void CanSetOwnPermission()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();

        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);

        driver.ClickOn("a[href='/account/profile']");
        driver.ClickOn("a.permissions");
        driver.ClickOn("button.administrationPermissions");
        driver.ClickOn("div.permission_1.row .MuiSelect-select");

        Assert.That(driver.WaitFor("li[data-value='1']", e => e.Text.Length > 0));

        var permissionText = driver.WaitFor("li[data-value='1']").Text;
        driver.ClickOn("li[data-value='1']");
        driver.ClickOn(".savePermissionsButton");

        Assert.That(driver.Contains(".rnc__notification-item--success"));

        driver.Refresh();
        driver.ClickOn("button.administrationPermissions");

        Assert.That(driver.WaitFor("div.permission_1.row .MuiSelect-select", e => e.Text == permissionText));

        driver.ClickOn("div.permission_1.row .MuiSelect-select");
        driver.ClickOn("li[data-value='2']");
        driver.ClickOn(".savePermissionsButton");

        Assert.That(driver.Contains(".rnc__notification-item--success"));

        driver.Refresh();

        driver.ClickOn("button.administrationPermissions");

        Assert.That(driver.WaitFor("div.permission_1.row .MuiSelect-select", d => d.Text != permissionText));
    }
    [Test]
    public void CanSetOthersPermission()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();

        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.AddNewStudent("Mikołaj", "Lubuszczyk", "02.02.2002");

        driver.ClickOn("a[href='/dashboard/manageStudents']");
        driver.ClickOn("tbody tr");
        driver.ClickOn("a.permissions");
        driver.ClickOn("button.administrationPermissions");
        driver.ClickOn("div.permission_1.row .MuiSelect-select");

        Assert.That(driver.WaitFor("li[data-value='1']", e => e.Text.Length > 0));

        var permissionText = driver.WaitFor("li[data-value='1']").Text;

        driver.ClickOn("li[data-value='1']");
        driver.ClickOn(".savePermissionsButton");

        Assert.That(driver.Contains(".rnc__notification-item--success"));

        driver.Refresh();
        driver.ClickOn("button.administrationPermissions");

        Assert.That(driver.WaitFor("div.permission_1.row .MuiSelect-select", e => e.Text == permissionText));

        driver.ClickOn("div.permission_1.row .MuiSelect-select");
        driver.ClickOn("li[data-value='2']");
        driver.ClickOn(".savePermissionsButton");

        Assert.That(driver.Contains(".rnc__notification-item--success"));

        driver.Refresh();
        driver.ClickOn("button.administrationPermissions");

        Assert.That(driver.WaitFor("div.permission_1.row .MuiSelect-select", e => e.Text != permissionText));
    }
}
