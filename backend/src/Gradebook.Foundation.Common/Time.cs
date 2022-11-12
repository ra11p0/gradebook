namespace Gradebook.Foundation.Common;

public class Time
{
    private static DateTime? _fakeUtcNow = null;
    public static DateTime UtcNow => _fakeUtcNow ?? DateTime.UtcNow;
    [Obsolete("Should be used only for testing purposes!")]
    public static void Reset() => _fakeUtcNow = null;
    [Obsolete("Should be used only for testing purposes!")]
    public static void SetFakeUtcNow(DateTime utcNow) => _fakeUtcNow = utcNow;
}
