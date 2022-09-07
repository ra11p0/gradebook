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

namespace Api.Controllers.Students;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly ServiceResolver<IMapper> _mapper;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public StudentsController(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetResolver<IMapper>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model)
    {
        var command = _mapper.Service.Map<NewStudentCommand>(model);
        var response = await _foundationCommands.Service.AddNewStudent(command);
        return response.Status ? Ok() : BadRequest(response.Message);
    }
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetAccessibleStudents()
    {
        var students = await _foundationQueries.Service.GetAllAccessibleStudents();
        return students.Status ? Ok(students.Response) : BadRequest(students.Message);
    }
    [HttpGet]
    [Route("{guid}")]
    public async Task<IActionResult> GetStudent([FromRoute] string guid)
    {
        return Ok();
    }
}
