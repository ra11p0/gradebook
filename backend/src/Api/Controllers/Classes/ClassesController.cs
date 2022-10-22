using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationPermissionsLogic> _foundationPermissions;
    public ClassesController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationPermissions = serviceProvider.GetResolver<IFoundationPermissionsLogic>();
    }
    [HttpDelete]
    [Route("{classGuid}")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> DeleteClass([FromRoute] Guid classGuid)
    {
        var classResponse = await _foundationQueries.Service.GetClassByGuid(classGuid);
        if (!classResponse.Status) return BadRequest(classResponse.Message);
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonBySchoolGuid(classResponse.Response!.SchoolGuid);
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return Forbid();
        var resp = await _foundationCommands.Service.DeleteClass(classGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{classGuid}")]
    [ProducesResponseType(typeof(ClassDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetClass([FromRoute] Guid classGuid)
    {
        var resp = await _foundationQueries.Service.GetClassByGuid(classGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{classGuid}/Students")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetStudentsInClass([FromRoute] Guid classGuid, [FromQuery] int page = 1)
    {
        var resp = await _foundationQueries.Service.GetStudentsInClass(classGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{classGuid}/Students/Candidates/Search")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> SearchStudentsCandidatesToClassWithCurrent([FromRoute] Guid classGuid, [FromQuery] int page = 1, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.SearchStudentsCandidatesToClassWithCurrent(classGuid, query!, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{classGuid}/Teachers")]
    [ProducesResponseType(typeof(IPagedList<TeacherDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetTeachersInClass([FromRoute] Guid classGuid, [FromQuery] int page = 1)
    {
        var resp = await _foundationQueries.Service.GetTeachersInClass(classGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpPut]
    [Route("{classGuid}/Students")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> PutStudentToClass([FromRoute] Guid classGuid, [FromBody] Guid studentGuid)
    {
        var resp = await _foundationCommands.Service.AddStudentsToClass(classGuid, studentGuid.AsEnumerable());
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpPost]
    [Route("{classGuid}/Students")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> EditStudentsInClass([FromRoute] Guid classGuid, [FromBody] IEnumerable<Guid> studentsGuids)
    {
        var resp = await _foundationCommands.Service.EditStudentsInClass(classGuid, studentsGuids);
        //  returns all students in class
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpDelete]
    [Route("{classGuid}/Students")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> DeleteStudentsFromClass([FromRoute] Guid classGuid, [FromBody] IEnumerable<Guid> studentsGuids)
    {
        var resp = await _foundationCommands.Service.DeleteStudentsFromClass(classGuid, studentsGuids);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpPut]
    [Route("{classGuid}/Teachers")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> PutTeacherToClass([FromRoute] Guid classGuid, [FromBody] Guid teacherGuid)
    {
        var resp = await _foundationCommands.Service.AddTeachersToClass(classGuid, teacherGuid.AsEnumerable());
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpPost]
    [Route("{classGuid}/Teachers")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> EditTeachersInClass([FromRoute] Guid classGuid, [FromBody] IEnumerable<Guid> teachersGuids)
    {
        var resp = await _foundationCommands.Service.EditTeachersInClass(classGuid, teachersGuids);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpDelete]
    [Route("{classGuid}/Teachers")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> DeleteTeachersFromClass([FromRoute] Guid classGuid, [FromBody] IEnumerable<Guid> teachersGuids)
    {
        var resp = await _foundationCommands.Service.DeleteTeachersFromClass(classGuid, teachersGuids);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
}
