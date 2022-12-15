using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

public partial class SchoolsController
{
    [HttpPost]
    [Route("{schoolGuid}/EducationCycles")]
    [ProducesResponseType(typeof(Guid), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    [ProducesResponseType(typeof(string), statusCode: 403)]
    [ProducesResponseType(typeof(string), statusCode: 404)]
    public async Task<ObjectResult> AddNewEducationCycle([FromBody] EducationCycleCommand model, [FromRoute] Guid schoolGuid)
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
    public async Task<ObjectResult> GetEducationCycles([FromRoute] Guid schoolGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var res = await _foundationQueries.Service.GetEducationCyclesInSchool(schoolGuid, page ?? 0, query ?? "");
        return res.ObjectResult;
    }
}
