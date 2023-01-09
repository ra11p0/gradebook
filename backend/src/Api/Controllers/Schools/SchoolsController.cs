using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public partial class SchoolsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationPermissionsLogic> _foundationPermissions;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IMapper> _mapper;
    public SchoolsController(IServiceProvider serviceProvider)
    {
        _foundationPermissions = serviceProvider.GetResolver<IFoundationPermissionsLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _mapper = serviceProvider.GetResolver<IMapper>();
    }

    #region school
    [HttpGet]
    [Route("{schoolGuid}")]
    [ProducesResponseType(typeof(SchoolDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetSchool(schoolGuid);
        IActionResult value = resp.StatusCode == 404 ? NotFound(resp.Message) : BadRequest(resp.Message);
        return resp.Status ? base.Ok(resp.Response) : value;
    }
    [HttpDelete]
    [Route("{schoolGuid}")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> DeleteSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationCommands.Service.DeleteSchool(schoolGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    #endregion


    #region subjects
    [HttpGet, Route("{schoolGuid}/subjects")]
    [ProducesResponseType(typeof(PagedList<SubjectDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSubjects([FromRoute] Guid schoolGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.GetSubjectsForSchool(schoolGuid, page ?? 0, query ?? "");
        return resp.ObjectResult;
    }
    [HttpPost, Route("{schoolGuid}/subjects")]
    [ProducesResponseType(typeof(Guid), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> AddSubject([FromBody] NewSubjectCommand command, [FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationCommands.Service.AddSubject(schoolGuid, command);
        return resp.ObjectResult;
    }
    #endregion
}
