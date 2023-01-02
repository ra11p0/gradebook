using OpenQA.Selenium.Chrome;

namespace Gradebook.Tests.Selenium;

public class DriverImplementation : ChromeDriver, IDisposable
{
    public DriverImplementation(string chromeDriverDirectory, ChromeOptions options) : base(chromeDriverDirectory, options)
    { }
    public new void Dispose()
    {
        this.Quit();
        ((ChromeDriver)this).Dispose();
    }
}
