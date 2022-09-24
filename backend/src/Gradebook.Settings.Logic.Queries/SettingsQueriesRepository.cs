using Gradebook.Foundation.Common;
using Gradebook.Settings.Database;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueriesRepository : BaseRepository<SettingsDatabaseContext>, ISettingsQueriesRepository
{
    public SettingsQueriesRepository(SettingsDatabaseContext context) : base(context)
    {
    }
}
