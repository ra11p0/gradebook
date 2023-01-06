using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Settings.Commands;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Foundation.Common.Settings.Queries.Definitions;

namespace Gradebook.Settings.Logic.Queries;

public class SettingsQueries : BaseLogic<ISettingsQueriesRepository>, ISettingsQueries
{
    private readonly ServiceResolver<Context> _context;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public SettingsQueries(ISettingsQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _context = serviceProvider.GetResolver<Context>();
    }

    public async Task<Guid> GetDefaultSchoolGuid(string userGuid)
    {
        var resp = await Repository.GetSettingForUserAsync<Guid>(userGuid, SettingEnum.DefaultSchool);
        if (resp == default) return (await _foundationQueries.Service.GetSchoolsForUser(userGuid)).Response!.OrderBy(e => e.School.Name).Select(e => e.School.Guid).FirstOrDefault();
        return resp;
    }

    public async Task<ResponseWithStatus<SettingsDto>> GetAccountSettings()
    {
        string? userGuid = _context.Service.UserId;
        if (userGuid is null) return new ResponseWithStatus<SettingsDto>(401);
        var settings = new SettingsDto()
        {
            DefaultSchool = await GetDefaultSchoolGuid(userGuid),
            Language = await GetUserLanguage(userGuid)
        };
        return new ResponseWithStatus<SettingsDto>(settings);
    }

    public Task<string?> GetUserLanguage(string userGuid)
        => Repository.GetSettingForUserAsync<string>(userGuid, SettingEnum.Language);
}
