using Gradebook.Foundation.Common;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueriesRepositoryCached : BaseRepositoryCached<SettingsQueriesRepository, object>, ISettingsQueriesRepository
{
    public SettingsQueriesRepositoryCached(SettingsQueriesRepository _base, object cacheMachine) : base(_base, cacheMachine)
    {
    }
}
