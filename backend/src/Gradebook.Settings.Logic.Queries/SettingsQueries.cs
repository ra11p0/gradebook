using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Foundation.Common.Settings.Queries.Definitions;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueries : BaseLogic<ISettingsQueriesRepository>, ISettingsQueries
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public SettingsQueries(ISettingsQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    public async Task<Guid> GetDefaultPersonGuid(string userGuid)
    {
        var resp = await Repository.GetSettingForUserAsync<Guid>(userGuid, SettingEnum.DefaultPersonGuid);
        if (resp == default) return (await _foundationQueries.Service.GetPeopleByUserGuid(userGuid)).Response!.Select(e => e.Guid).FirstOrDefault();
        return resp;
    }

    public async Task<ResponseWithStatus<SettingsDto>> GetAccountSettings(string userGuid)
    {
        var settings = new SettingsDto()
        {
            DefaultPersonGuid = await GetDefaultPersonGuid(userGuid)
        };
        return new ResponseWithStatus<SettingsDto>(settings, true);
    }
}
