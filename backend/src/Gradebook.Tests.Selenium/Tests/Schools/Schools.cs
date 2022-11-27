using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Schools;

[Category("Selenium")]
[Order(2)]
public class Schools
{
    private readonly Dictionary<string, string> _storage = new();
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
        // Set up storage
        _storage["studentName"] = "Mateusz";
    }
    [Test]
    [Order(1)]
    public void CanAddNewSchool()
    {
        _driver!.GoToSchoolsTab();
        _wait!.Until(d => d.FindElement(By.CssSelector(".addSchoolButton"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector(".activateAdministrator"))).Click();
        _wait.Until(d => d.FindElement(By.CssSelector("input[name='city']"))).SendKeys(CommonResources.GetValue("secondSchoolCity"));
        _driver!.FindElement(By.CssSelector("input[name='postalCode']")).SendKeys(CommonResources.GetValue("secondSchoolPostalCode"));
        _driver.FindElement(By.CssSelector("input[name='addressLine1']")).SendKeys(CommonResources.GetValue("secondSchoolAddress"));
        _driver.FindElement(By.CssSelector("input[name='name']")).SendKeys(CommonResources.GetValue("secondSchoolName"));
        var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
        _actions!.MoveToElement(submitButton).Perform();
        submitButton.Click();
        _wait.Until(d => d.FindElement(By.CssSelector("a[href='/account/profile']")).Displayed);
        var schoolNameRecord = _wait.Until(d => d.FindElement(By.XPath($"//div[text()='{CommonResources.GetValue("secondSchoolName")}']")));
        var schoolAddressRecord = _wait.Until(d => d.FindElement(By.XPath($"//div[text()='{CommonResources.GetValue("secondSchoolAddress")}']")));
        var postalCodeRecord = _wait.Until(d => d.FindElement(By.XPath($"//div[text()='{CommonResources.GetValue("secondSchoolPostalCode")}']")));
        Assert.That(schoolNameRecord.Displayed && schoolAddressRecord.Displayed && postalCodeRecord.Displayed);
    }

    [TearDown]
    public void End()
    {
        _driver?.Dispose();
    }
}
