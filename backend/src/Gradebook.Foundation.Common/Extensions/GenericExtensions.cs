using System.Text;

namespace Gradebook.Foundation.Common.Extensions;

public static class GenericExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this T obj)
    {
        return new T[] { obj };
    }
}
