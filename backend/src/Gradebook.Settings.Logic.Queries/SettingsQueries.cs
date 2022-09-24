using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Commands;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueries : BaseLogic<ISettingsQueriesRepository>, ISettingsQueries
{
    public SettingsQueries(ISettingsQueriesRepository repository) : base(repository)
    {
    }
}
