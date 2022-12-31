using Gradebook.Tests.Selenium.Constraints.Views;
using Gradebook.Tests.Selenium.Helpers;
using Gradebook.Tests.Selenium.IWebDriverExtensions;
using Gradebook.Tests.Selenium.QuickActionsExtensions;

namespace Gradebook.Tests.Selenium.Tests.Account;

[Category("Selenium")]
[Order(1)]
public class RegisterAndLogin
{
    [Test]
    [Order(1)]
    public void ShouldRegisterNewUser()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.GoTo(ConfigurationManager.GetValue("Urls:ApplicationUrl"));
        driver.ClickOn(Login.RegisterButton);
        driver.WaitFor(Register.EmailField).SendKeys(CommonResources.GetValue("email"));
        driver.WaitFor(Register.PasswordField).SendKeys(CommonResources.GetValue("password"));
        driver.WaitFor(Register.Password2Field).SendKeys(CommonResources.GetValue("password"));
        driver.WaitFor(Register.TermsAndConditionsSwitch).Click();
        driver.ClickOn(Register.SubmitButton);
        var link = DatabaseHelper.GetActivationLinkForEmail(CommonResources.GetValue("email")!);
        driver.GoTo(link);
        Assert.That(driver.Contains(Swal.SuccessRing));
        driver.ClickOn(Swal.ConfirmButton);
        driver.WaitFor(Login.EmailField).SendKeys(CommonResources.GetValue("email"));
        driver.WaitFor(Login.PasswordField).SendKeys(CommonResources.GetValue("password"));
        driver.ClickOn(Login.SubmitButton);
        Assert.That(driver.Contains(Header.LogOutButton));
    }
    [Test]
    [Order(2)]
    public void CanRegisterNewSchool()
    {
        using var driver = WebDriverBuilder.BuildWebDriver();
        driver.Login(CommonResources.GetValue("email")!, CommonResources.GetValue("password")!);
        driver.ClickOn(RegisterPerson.RegisterAdministratorButton);
        driver.WaitFor(RegisterPerson.NameField).SendKeys(CommonResources.GetValue("name")!);
        driver.WaitFor(RegisterPerson.SurnameField).SendKeys(CommonResources.GetValue("surname")!);
        driver.WaitFor(RegisterPerson.BirthdayField).SelectAll().SendKeys(CommonResources.GetValue("birthday")!);
        driver.ClickOn(RegisterPerson.SubmitButton);
        driver.WaitFor(RegisterPerson.SchoolNameField).SendKeys(CommonResources.GetValue("schoolName")!);
        driver.WaitFor(RegisterPerson.SchoolAddressLine1Field).SendKeys(CommonResources.GetValue("schoolAddress")!);
        driver.WaitFor(RegisterPerson.SchoolAddressPostalCode).SendKeys(CommonResources.GetValue("postalCode")!);
        driver.WaitFor(RegisterPerson.SchoolAddressCity).SendKeys(CommonResources.GetValue("city")!);
        driver.ClickOn(RegisterPerson.SubmitButton);
        Assert.That(driver.Contains(Header.AccountButton));
        Assert.That(
            driver.WaitFor(Header.AccountButton)
            .ContainsText(CommonResources.GetValue("name")! + " " + CommonResources.GetValue("surname")!));
    }
}
