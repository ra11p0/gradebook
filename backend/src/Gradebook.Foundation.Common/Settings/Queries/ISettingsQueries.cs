
namespace Gradebook.Foundation.Common.Settings.Commands;

public interface ISettingsQueries
{
    Task<Guid> GetDefaultPersonGuid(string userGuid);
}
