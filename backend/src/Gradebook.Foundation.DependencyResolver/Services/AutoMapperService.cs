using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Logic.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Gradebook.Foundation.DependencyResolver.Services;

public class AutoMapperService
{
    public static void Inject(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddAutoMapper(o=>{
            o.AddProfile<FoundationMappings>();
            o.AddProfile<FoundationCommandsMappings>();
        });
    }
}
