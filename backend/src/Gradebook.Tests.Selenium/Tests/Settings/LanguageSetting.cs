using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Settings;


[Category("Selenium")]
[Order(6)]
public class LanguageSetting
{
    [Test]
    public void ShouldChangeMomentJsLanguage()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.ClickOn("[test-id='languageDropdown']");
        driver.ClickOn("[test-id='languageDropdown-english']");
        driver.GoToTeachersTab();
        var dateTextBeforeChange = driver.WaitFor(".test-teacherBirthday").Text;

        driver.ClickOn("[test-id='languageDropdown']");
        driver.ClickOn("[test-id='languageDropdown-polish']");

        var dateTextAfterChange = driver.WaitFor(".test-teacherBirthday").Text;

        Assert.That(dateTextAfterChange, Is.Not.EqualTo(dateTextBeforeChange), "Moment.js language is has not changed to polish.");

        driver.Refresh();

        dateTextAfterChange = driver.WaitFor(".test-teacherBirthday").Text;

        Assert.That(dateTextAfterChange, Is.Not.EqualTo(dateTextBeforeChange), "Moment.js language is has not changed to polish after refresh.");

        driver.ClickOn("[test-id='languageDropdown']");
        driver.ClickOn("[test-id='languageDropdown-english']");

        dateTextAfterChange = driver.WaitFor(".test-teacherBirthday").Text;

        Assert.That(dateTextAfterChange, Is.EqualTo(dateTextBeforeChange), "Moment.js language is has not changed to english.");
    }

    [Test]
    public void ShouldChangeLanguage()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue(key: "password")!);
        driver.ClickOn(Header.LanguageSelect);
        driver.ClickOn(Header.LanguageSelectEnglish);
        driver.GoToTeachersTab();
        var textBeforeChange = driver.WaitFor(Header.LogOutButton).Text;

        driver.ClickOn(Header.LanguageSelect);
        driver.ClickOn(Header.LanguageSelectPolish);

        var textAfterChange = driver.WaitFor(Header.LogOutButton).Text;

        Assert.That(textAfterChange, Is.Not.EqualTo(textBeforeChange), "Language is has not changed to polish.");

        driver.Refresh();

        textAfterChange = driver.WaitFor(Header.LogOutButton).Text;

        Assert.That(textAfterChange, Is.Not.EqualTo(textBeforeChange), "Language is has not changed to polish after refresh.");

        driver.ClickOn(Header.LanguageSelect);
        driver.ClickOn(Header.LanguageSelectEnglish);

        textAfterChange = driver.WaitFor(Header.LogOutButton).Text;

        Assert.That(textAfterChange, Is.EqualTo(textBeforeChange), "Language is has not changed to english.");
    }
}
