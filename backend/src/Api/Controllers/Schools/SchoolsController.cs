using Api.Models.Invitations;
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

    #region invitations
    [HttpPost]
    [Route("{schoolGuid}/Invitations")]
    [ProducesResponseType(typeof(string[]), 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> AddMultipleNewInvitations([FromBody] NewMultipleInvitationModel model, [FromRoute] Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return BadRequest(currentPersonGuid.Message);
        if (!await _foundationPermissions.Service.CanInviteToSchool(currentPersonGuid.Response!)) return Forbid();
        var resp = await _foundationCommands.Service.GenerateMultipleSystemInvitation(model.InvitedPersonGuidArray, model.Role, schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpGet]
    [Route("{schoolGuid}/Invitations")]
    [ProducesResponseType(typeof(IPagedList<InvitationDto>), 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetMyInvitations([FromRoute] Guid schoolGuid, [FromQuery] int page = 1)
    {
        var resp = await _foundationQueries.Service.GetInvitationsToSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    #endregion

    #region students
    [HttpGet]
    [Route("{schoolGuid}/Students")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetStudentsInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetStudentsInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpPost]
    [Route("{schoolGuid}/Students")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model, [FromRoute] Guid schoolGuid)
    {
        var command = _mapper.Service.Map<NewStudentCommand>(model);
        var response = await _foundationCommands.Service.AddNewStudent(command, schoolGuid);
        return response.Status ? Ok(response.Response) : BadRequest(response.Message);
    }

    [HttpGet]
    [Route("{schoolGuid}/Students/Inactive")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetInactiveStudentsInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetInactiveStudents(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest();
    }
    #endregion

    #region teachers
    [HttpGet]
    [Route("{schoolGuid}/Teachers")]
    [ProducesResponseType(typeof(IPagedList<TeacherDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetTeachersInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetTeachersInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpPost]
    [Route("{schoolGuid}/Teachers")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewTeacher([FromBody] NewTeacherModel model, [FromRoute] Guid schoolGuid)
    {
        var command = _mapper.Service.Map<NewTeacherCommand>(model);
        var response = await _foundationCommands.Service.AddNewTeacher(command, schoolGuid);
        return response.Status ? Ok() : BadRequest(response.Message);
    }

    [HttpGet]
    [Route("{schoolGuid}/Teachers/Inactive")]
    [ProducesResponseType(typeof(IEnumerable<TeacherDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetInactiveTeachersInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetInactiveTeachers(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest();
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

    #region education cycles
    [HttpPost]
    [Route("{schoolGuid}/EducationCycles")]
    [ProducesResponseType(typeof(Guid), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [ProducesResponseType(typeof(string), statusCode: 403)]
    [ProducesResponseType(typeof(string), statusCode: 404)]
    public async Task<IActionResult> AddNewEducationCycle([FromBody] EducationCycleCommand model, [FromRoute] Guid schoolGuid)
    {
        model.SchoolGuid = schoolGuid;
        var res = await _foundationCommands.Service.AddNewEducationCycle(model);
        return res.ObjectResult;
    }
    [HttpGet]
    [Route("{schoolGuid}/EducationCycles")]
    [ProducesResponseType(typeof(Guid), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [ProducesResponseType(typeof(string), statusCode: 403)]
    [ProducesResponseType(typeof(string), statusCode: 404)]
    public async Task<IActionResult> GetEducationCycles([FromRoute] Guid schoolGuid, [FromQuery] int page = 0)
    {
        var res = await _foundationQueries.Service.GetEducationCyclesInSchool(schoolGuid, page);
        return res.ObjectResult;
    }
    #endregion
}
