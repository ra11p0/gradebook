using Api.Models.Invitations;
using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
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
public class SchoolsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<UserManager<ApplicationUser>> _userManager;
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IMapper> _mapper;
    public SchoolsController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _userManager = serviceProvider.GetResolver<UserManager<ApplicationUser>>();
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _mapper = serviceProvider.GetResolver<IMapper>();
    }

    #region school
    [HttpPost]
    [Route("")]
    [ProducesErrorResponseType(typeof(string))]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewSchool([FromBody] NewSchoolModel model)
    {
        var newSchoolCommand = _mapper.Service.Map<NewSchoolCommand>(model);
        var resp = await _foundationCommands.Service.AddNewSchool(newSchoolCommand);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }

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
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> DeleteSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationCommands.Service.DeleteSchool(schoolGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    #endregion

    #region people

    [HttpGet]
    [Route("{schoolGuid}/People")]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetPeopleInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetPeopleInSchool(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{schoolGuid}/People/Search")]
    [ProducesResponseType(typeof(IPagedList<PersonDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetPeopleInSchoolSearch([FromRoute] Guid schoolGuid, [FromQuery] int page = 1, [FromQuery] string discriminator = "", [FromQuery] string query = "")
    {
        var resp = await _foundationQueries.Service.GetPeopleInSchool(schoolGuid, discriminator, query, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpPost]
    [Route("{schoolGuid}/People")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddPersonToSchool([FromRoute] Guid schoolGuid, [FromBody] Guid personGuid)
    {
        var resp = await _foundationCommands.Service.AddPersonToSchool(schoolGuid, personGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    #endregion

    #region invitations
    [HttpPost]
    [Route("{schoolGuid}/Invitations")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(string[]), 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> AddMultipleNewInvitations([FromBody] NewMultipleInvitationModel model, [FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationCommands.Service.GenerateMultipleSystemInvitation(model.InvitedPersonGuidArray, model.Role, schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpGet]
    [Route("{schoolGuid}/Invitations")]
    [Authorize(Roles = "SuperAdmin")]
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
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetStudentsInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetStudentsInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpPost]
    [Route("{schoolGuid}/Students")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model, [FromRoute] Guid schoolGuid)
    {
        var command = _mapper.Service.Map<NewStudentCommand>(model);
        var response = await _foundationCommands.Service.AddNewStudent(command, schoolGuid);
        return response.Status ? Ok() : BadRequest(response.Message);
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
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(IPagedList<TeacherDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetTeachersInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetTeachersInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpPost]
    [Route("{schoolGuid}/Teachers")]
    [Authorize(Roles = "SuperAdmin")]
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

    #region classes
    [HttpPost]
    [Route("{schoolGuid}/Classes")]
    [ProducesResponseType(typeof(string), 400)]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewClass([FromRoute] Guid schoolGuid, [FromBody] NewClassModel model)
    {
        var command = _mapper.Service.Map<NewClassCommand>(model);
        command.SchoolGuid = schoolGuid;
        var resp = await _foundationCommands.Service.AddNewClass(command);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{schoolGuid}/Classes")]
    [ProducesResponseType(typeof(IPagedList<ClassDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewClass([FromRoute] Guid schoolGuid, [FromQuery] int page = 1)
    {
        var resp = await _foundationQueries.Service.GetClassesInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    #endregion
}
