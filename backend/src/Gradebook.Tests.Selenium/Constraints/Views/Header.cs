namespace Gradebook.Tests.Selenium.Constraints.Views;

public static class Header
{
    public static readonly string LogOutButton = "#logOutButton";
    public static readonly string AccountButton = "a.nav-link[href='/account/profile']";
    public static readonly string SchoolSelect = "#schoolSelect";
    public static readonly string LanguageSelect = "[test-id='languageDropdown']";
    public static readonly string LanguageSelectEnglish = "[test-id='languageDropdown-english']";
    public static readonly string LanguageSelectPolish = "[test-id='languageDropdown-polish']";
}
