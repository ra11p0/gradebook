using Gradebook.Foundation.Common;
using Gradebook.Settings.Database;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommandsRepository : BaseRepository<SettingsDatabaseContext>, ISettingsCommandsRepository
{
    public SettingsCommandsRepository(SettingsDatabaseContext context) : base(context)
    {
    }
}
