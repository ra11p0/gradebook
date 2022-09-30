using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Permissions;

[Order(3)]
public class Permissions
{
    private IWebDriver? _driver;
    private WebDriverWait? _wait;
    private Actions? _actions;
    [SetUp]
    public void Setup()
    {
        _driver = WebDriverBuilder.BuildWebDriver();
        _driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);

        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        _actions = new(_driver);
    }
    [Test]
    [Order(1)]
    public void CanSetOwnPermission()
    {
        _driver!.AddNewStudent("Mikołaj", "Lubuszczyk", "10222004");
        _wait!.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("a.permissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        var permissionText = _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Text;
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        var changedPermissionText = _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Text;
        if (permissionText != changedPermissionText) Assert.Fail("Did not change permission");
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='2']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        Assert.That(_wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Text, Is.Not.EqualTo(permissionText));

    }
    [Test]
    [Order(2)]
    public void CanSetOthersPermission()
    {
        _driver!.AddNewStudent("Mikołaj", "Lubuszczyk", "10222004");
        _wait!.Until(d => d.FindElement(By.CssSelector("a[href='/manageStudents']"))).Click();
        _wait!.Until(d => d.FindElement(By.CssSelector("button.showProfileButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("a.permissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        var permissionText = _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Text;
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        var changedPermissionText = _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Text;
        if (permissionText != changedPermissionText) Assert.Fail("Did not change permission");
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='2']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        Assert.That(_wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Text, Is.Not.EqualTo(permissionText));
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
