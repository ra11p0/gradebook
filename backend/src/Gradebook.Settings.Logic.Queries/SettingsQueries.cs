using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueries : BaseLogic<ISettingsQueriesRepository>, ISettingsQueries
{
    public SettingsQueries(ISettingsQueriesRepository repository) : base(repository)
    {
    }

    public Task<Guid> GetDefaultPersonGuid(Guid personGuid)
        => Repository.GetSettingForPersonAsync<Guid>(personGuid, SettingEnum.DefaultPersonGuid);
}
