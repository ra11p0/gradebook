namespace Gradebook.Foundation.Common.Extensions;

public static class ICollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
    {
        foreach (T item in itemsToAdd) collection.Add(item);
    }
    public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
    {
        foreach (T item in itemsToAdd) collection.Remove(item);
    }
}
