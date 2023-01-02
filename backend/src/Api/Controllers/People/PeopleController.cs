using Api.Models.People;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Permissions;
using Gradebook.Foundation.Common.Permissions.Commands;
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
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationPermissionsLogic> _foundationPermissionsLogic;
    private readonly ServiceResolver<IPermissionsPermissionsLogic> _permissionsPermissionsLogic;
    private readonly ServiceResolver<Context> _context;
    private IFoundationPeopleQueries FoundationPeopleQueries => _foundationQueries.Service;
    public PeopleController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _permissionsCommands = serviceProvider.GetResolver<IPermissionsCommands>();
        _permissionsQueries = serviceProvider.GetResolver<IPermissionsQueries>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationPermissionsLogic = serviceProvider.GetResolver<IFoundationPermissionsLogic>();
        _permissionsPermissionsLogic = serviceProvider.GetResolver<IPermissionsPermissionsLogic>();
        _context = serviceProvider.GetResolver<Context>();
    }

    [HttpPost, Route("Details")]
    public async Task<ObjectResult> GetPeopleDetails([FromBody] Guid[] peopleGuids, [FromQuery] int? page = 0)
    {
        return (await FoundationPeopleQueries.GetPeopleByGuids(peopleGuids, page!.Value)).ObjectResult;
    }

    [HttpGet, Route("{teacherGuid}/subjects")]
    [ProducesResponseType(typeof(PersonDto), statusCode: 200)]
    public async Task<IActionResult> GetSubjectsForTeacher([FromRoute] Guid teacherGuid, [FromQuery] int page = 0)
    {
        var personResponse = await _foundationQueries.Service.GetSubjectsForTeacher(teacherGuid, page);
        return personResponse.Status ? Ok(personResponse.Response) : BadRequest(personResponse.Message);
    }

    [HttpGet, Route("{personGuid}")]
    [ProducesResponseType(typeof(PersonDto), statusCode: 200)]
    public async Task<IActionResult> GetPerson([FromRoute] Guid personGuid)
    {
        var personResponse = await _foundationQueries.Service.GetPersonByGuid(personGuid);
        return personResponse.Status ? Ok(personResponse.Response) : BadRequest(personResponse.Message);
    }

    [HttpGet, Route("{personGuid}/Classes")]
    [ProducesResponseType(typeof(PagedList<ClassDto>), statusCode: 200)]
    public async Task<IActionResult> GetManagedClasses([FromRoute] Guid personGuid, [FromQuery] int page = 0)
    {
        var classesResponse = await _foundationQueries.Service.GetClassesForPerson(personGuid, page);
        return classesResponse.Status ? Ok(classesResponse.Response) : BadRequest(classesResponse.Message);
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

    [ProducesResponseType(typeof(GetPermissionsResponseModel[]), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> Permissions([FromRoute] Guid personGuid)
    {
        var resp = await _permissionsQueries.Service.GetPermissionsForPerson(personGuid);
        return resp is not null ? Ok(resp.Select(e => new GetPermissionsResponseModel()
        {
            PermissionId = e.Key,
            PermissionLevel = e.Value,
            PermissionGroup = _permissionsQueries.Service.GetPermissionGroupForPermission(e.Key),
            PermissionLevels = _permissionsQueries.Service.GetPermissionLevelsForPermission(e.Key)
        })) : BadRequest();
    }
    [HttpPost]
    [Route("{personGuid}/Permissions")]
    [ProducesResponseType(typeof(GetPermissionsResponseModel[]), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> Permissions([FromRoute] Guid personGuid, [FromBody] SetPermissionsRequestModel[] permissions)
    {
        var currentPersonGuid = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(personGuid);
        if (!currentPersonGuid.Status) return BadRequest(currentPersonGuid.Message);
        if (!await _permissionsPermissionsLogic.Service.CanManagePermissions(currentPersonGuid.Response)) return Forbid();
        await _permissionsCommands.Service.SetPermissionsForPerson(personGuid, permissions.ToDictionary(e => e.PermissionId, e => e.PermissionLevel));
        var resp = await _permissionsQueries.Service.GetPermissionsForPerson(personGuid);
        return resp is not null ? Ok(resp.Select(e => new GetPermissionsResponseModel()
        {
            PermissionId = e.Key,
            PermissionLevel = e.Value,
            PermissionGroup = _permissionsQueries.Service.GetPermissionGroupForPermission(e.Key),
            PermissionLevels = _permissionsQueries.Service.GetPermissionLevelsForPermission(e.Key)
        })) : BadRequest();
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
