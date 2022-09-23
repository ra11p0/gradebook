using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.Common.Extensions;

public static class IServiceProviderExtensions
{
    public static T Resolve<T>(this IServiceProvider provider) where T : notnull
        => provider.CreateScope().ServiceProvider.GetRequiredService<T>();

    public static ServiceResolver<T> GetResolver<T>(this IServiceProvider provider) where T : class
        => new(provider);
}
