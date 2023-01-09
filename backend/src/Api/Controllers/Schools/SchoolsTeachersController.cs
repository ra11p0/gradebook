using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers.Schools;

public partial class SchoolsController
{


    [HttpGet]
    [Route("{schoolGuid}/Teachers")]
    [ProducesResponseType(typeof(IPagedList<TeacherDto>), statusCode: 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetTeachersInSchool([FromRoute] Guid schoolGuid, int page = 1)
    {
        var resp = await _foundationQueries.Service.GetTeachersInSchool(schoolGuid, page);
        return resp.ObjectResult;
    }

    [HttpPost]
    [Route("{schoolGuid}/Teachers")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewTeacher([FromBody] NewTeacherModel model, [FromRoute] Guid schoolGuid)
    {
        var command = _mapper.Service.Map<NewTeacherCommand>(model);
        var response = await _foundationCommands.Service.AddNewTeacher(command, schoolGuid);
        return response.ObjectResult;
    }

    [HttpGet]
    [Route("{schoolGuid}/Teachers/Inactive")]
    [ProducesResponseType(typeof(IEnumerable<TeacherDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetInactiveTeachersInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetInactiveTeachers(schoolGuid);
        return resp.ObjectResult;
    }


}
