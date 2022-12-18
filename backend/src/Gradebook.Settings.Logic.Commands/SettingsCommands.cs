using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Commands.Definitions;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Logic.Commands;

public class SettingsCommands : BaseLogic<ISettingsCommandsRepository>, ISettingsCommands
{
    private readonly ServiceResolver<Context> _context;
    public SettingsCommands(ISettingsCommandsRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _context = serviceProvider.GetResolver<Context>();
    }

    public async Task SetDefaultSchoolGuid(string userGuid, Guid schoolGuid)
    {
        await Repository.SetSettingForUserAsync(userGuid, SettingEnum.DefaultSchool, schoolGuid);
        await Repository.SaveChangesAsync();
    }

    public async Task<StatusResponse> SetAccountSettings(SettingsCommand settings)
    {
        string? userGuid = _context.Service.UserId;
        if (userGuid is null) return new StatusResponse(401);
        if (settings.DefaultSchool.HasValue) await SetDefaultSchoolGuid(userGuid, settings.DefaultSchool.Value);
        if (settings.Language is not null) await SetLanguage(userGuid, settings.Language);
        return new StatusResponse(true);
    }

    public async Task SetLanguage(string userGuid, string language)
    {
        await Repository.SetSettingForUserAsync(userGuid, SettingEnum.Language, language);
        await Repository.SaveChangesAsync();
    }
}
