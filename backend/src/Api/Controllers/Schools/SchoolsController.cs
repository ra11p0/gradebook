using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SchoolsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IMapper> _mapper;
    public SchoolsController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _mapper = serviceProvider.GetResolver<IMapper>();
    }
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
    [HttpPost]
    [Route("{schoolGuid}/People")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddPersonToSchool([FromRoute] Guid schoolGuid, [FromBody] Guid personGuid)
    {
        var resp = await _foundationCommands.Service.AddPersonToSchool(schoolGuid, personGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{schoolGuid}/Students")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetStudentsInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetStudentsInSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewSchool([FromBody] NewSchoolModel model)
    {
        var newSchoolCommand = _mapper.Service.Map<NewSchoolCommand>(model);
        var resp = await _foundationCommands.Service.AddNewSchool(newSchoolCommand);
        return resp.Status ? Ok() : BadRequest(resp.Message);
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
}
