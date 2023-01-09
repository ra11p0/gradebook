using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers.Schools;

public partial class SchoolsController
{
    [HttpGet]
    [Route("{schoolGuid}/Students")]
    [ProducesResponseType(typeof(IPagedList<StudentDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetStudentsInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetStudentsInSchool(schoolGuid, page);
        return resp.ObjectResult;
    }

    [HttpPost]
    [Route("{schoolGuid}/Students")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model, [FromRoute] Guid schoolGuid)
    {
        var command = _mapper.Service.Map<NewStudentCommand>(model);
        var response = await _foundationCommands.Service.AddNewStudent(command, schoolGuid);
        return response.ObjectResult;
    }

    [HttpGet]
    [Route("{schoolGuid}/Students/Inactive")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetInactiveStudentsInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetInactiveStudents(schoolGuid);
        return resp.ObjectResult;
    }

}
