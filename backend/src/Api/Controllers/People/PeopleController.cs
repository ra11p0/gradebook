using Api.Models.People;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Permissions.Commands;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Common.Permissions.Queries;
using Gradebook.Foundation.Common.Settings.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.People;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PeopleController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IPermissionsCommands> _permissionsCommands;
    private readonly ServiceResolver<IPermissionsQueries> _permissionsQueries;
    /*     private readonly ServiceResolver<ISettingsQueries> _settingsQueries;
        private readonly ServiceResolver<ISettingsCommands> _settingsCommands; */
    public PeopleController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _permissionsCommands = serviceProvider.GetResolver<IPermissionsCommands>();
        _permissionsQueries = serviceProvider.GetResolver<IPermissionsQueries>();
        /*         _settingsQueries = serviceProvider.GetResolver<ISettingsQueries>();
                _settingsCommands = serviceProvider.GetResolver<ISettingsCommands>(); */
    }
    [HttpDelete]
    [Route("{personGuid}")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> DeletePerson([FromRoute] Guid personGuid)
    {
        var resp = await _foundationCommands.Service.DeletePerson(personGuid);
        return resp.Status ? Ok() : BadRequest();
    }
    [HttpGet]
    [Route("{personGuid}/Permissions")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> Permissions([FromRoute] Guid personGuid)
    {
        var resp = await _permissionsQueries.Service.GetPermissionsForPerson(personGuid);
        return resp is not null ? Ok(resp.Select(e => new GetPermissionsResponseModel()
        {
            PermissionId = e.Key,
            PermissionLevel = e.Value
        })) : BadRequest();
    }
    [HttpPost]
    [Route("{personGuid}/Permissions")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> Permissions([FromRoute] Guid personGuid, [FromBody] SetPermissionsRequestModel[] permissions)
    {
        await _permissionsCommands.Service.SetPermissionsForPerson(personGuid, permissions.ToDictionary(e => e.PermissionId, e => e.PermissionLevel));
        return Ok();
    }
    [HttpGet]
    [Route("{personGuid}/Settings/{settingEnum}")]
    public Task<IActionResult> GetSetting([FromRoute] Guid personGuid, [FromRoute] SettingEnum settingEnum)
    {
        /*object? setting;
        switch (settingEnum)
        {
            default:
                return BadRequest();
        }

        return setting is null ? NoContent() : Ok(setting);*/
        throw new NotImplementedException();
    }
    [HttpPost]
    [Route("{personGuid}/Settings/{settingEnum}")]
    public Task<IActionResult> SetSetting([FromRoute] Guid personGuid, [FromRoute] SettingEnum settingEnum, [FromBody] object value)
    {/*
        switch (settingEnum)
        {
            case SettingEnum.DefaultPersonGuid:
                await _settingsCommands.Service.SetDefaultPersonGuid(personGuid, (Guid)value);
                break;
            default:
                return BadRequest();
        }
        return Ok();*/

        throw new NotImplementedException();
    }
    [HttpPost]
    [Route("Activation/Code/{activationCode}")]
    public async Task<IActionResult> ActivatePerson([FromRoute] string activationCode)
    {
        var resp = await _foundationCommands.Service.ActivatePerson(activationCode);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
}
