using System.Resources;

namespace Gradebook.Tests.Selenium;

public static class CommonResources
{
    private readonly static ResourceManager _resources;
    public static ResourceManager Resources => _resources;
    static CommonResources()
    {
        _resources = new ResourceManager(typeof(CommonResources));
    }
    public static string? GetValue(string key) => _resources.GetString(key);
}
