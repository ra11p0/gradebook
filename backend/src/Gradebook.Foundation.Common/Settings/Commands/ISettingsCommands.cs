namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsCommands
{
    Task SetDefaultPersonGuid(string userGuid, Guid defaultPersonGuid);
}
