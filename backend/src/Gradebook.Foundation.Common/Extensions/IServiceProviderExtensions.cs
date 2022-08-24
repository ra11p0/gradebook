using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Foundation.Common.Extensions;

public static class IServiceProviderExtensions
{
    public static T Resolve<T>(this IServiceProvider provider) where T : notnull{
        return provider
                .CreateScope().ServiceProvider.GetRequiredService<T>();
    }
}
