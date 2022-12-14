using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Subjects;

[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    public SubjectsController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
    }
    [HttpGet]
    [Route("{subjectGuid}")]
    [ProducesResponseType(typeof(SubjectDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSubject([FromRoute] Guid subjectGuid)
    {
        var resp = await _foundationQueries.Service.GetSubject(subjectGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{subjectGuid}/teachers")]
    [ProducesResponseType(typeof(IPagedList<TeacherDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetTeachersForSubject([FromRoute] Guid subjectGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.GetTeachersForSubject(subjectGuid, page ?? 0, query);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpPost]
    [Route("{subjectGuid}/teachers")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> AddTeachersForSubject([FromRoute] Guid subjectGuid, [FromBody] List<Guid> teachersGuids)
    {
        var resp = await _foundationCommands.Service.EditTeachersInSubject(subjectGuid, teachersGuids);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
}
