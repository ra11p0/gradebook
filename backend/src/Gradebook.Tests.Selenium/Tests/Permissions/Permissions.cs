using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Permissions;

[Category("Selenium")]
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

        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _actions = new(_driver);
    }
    [Test]
    [Order(1)]
    public void CanSetOwnPermission()
    {
        _driver!.AddNewStudent("Mikołaj", "Lubuszczyk", "02.02.2002");
        _wait!.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("a.permissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']")).Text.Length > 0);
        var permissionText = _driver!.FindElement(By.CssSelector("li[data-value='1']")).Text; _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")).Displayed);
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select")).Text == permissionText);
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='2']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        Assert.That(_wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select")).Text != permissionText));

    }
    [Test]
    [Order(2)]
    public void CanSetOthersPermission()
    {
        _driver!.AddNewStudent("Mikołaj", "Lubuszczyk", "02.02.2002");
        _wait!.Until(d => d.FindElement(By.CssSelector("a[href='/manageStudents']"))).Click();
        _wait!.Until(d => d.FindElement(By.CssSelector("button.showProfileButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("a.permissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']")).Text.Length > 0);
        var permissionText = _driver!.FindElement(By.CssSelector("li[data-value='1']")).Text;
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='1']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")).Displayed);
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select")).Text == permissionText);
        _wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("li[data-value='2']"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".savePermissionsButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".rnc__notification-item--success")));
        _driver!.Navigate().Refresh();
        _wait.Until(d => d.FindElement(By.CssSelector("button.administrationPermissions"))).Click();
        Assert.That(_wait.Until(d => d.FindElement(By.CssSelector("div.permission_1.row .MuiSelect-select")).Text != permissionText));
    }
    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
