using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
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
    //private readonly ServiceResolver<IMapper> _mapper;
    //private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public StudentsController(IServiceProvider serviceProvider)
    {
        //_mapper = serviceProvider.GetResolver<IMapper>();
        //_foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
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
    [Route("Inactive")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetInactiveStudents(Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetInactiveStudents(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest();
    }
}
