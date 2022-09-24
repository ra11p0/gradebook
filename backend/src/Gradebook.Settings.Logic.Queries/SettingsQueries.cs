using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueries : BaseLogic<ISettingsQueriesRepository>, ISettingsQueries
{
    public SettingsQueries(ISettingsQueriesRepository repository) : base(repository)
    {
    }

    public Task<Guid> GetDefaultPersonGuid(string userGuid)
        => Repository.GetSettingForUserAsync<Guid>(userGuid, SettingEnum.DefaultPersonGuid);
}
