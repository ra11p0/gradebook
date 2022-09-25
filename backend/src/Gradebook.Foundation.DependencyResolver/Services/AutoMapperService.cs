using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Logic.Commands;
using Gradebook.Foundation.Logic.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class AutoMapperService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot _)
    {
        services.AddAutoMapper(o =>
        {
            o.AddProfile<FoundationMappings>();
            o.AddProfile<FoundationCommandsMappings>();
            o.AddProfile<FoundationQueriesMappings>();
        });
    }
}
