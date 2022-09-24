namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsCommands
{
    Task SetDefaultPersonGuid(Guid personGuid, Guid defaultPersonGuid);
}
