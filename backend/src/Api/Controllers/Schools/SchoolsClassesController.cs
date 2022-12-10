using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

public partial class SchoolsController
{
    [HttpPost]
    [Route("{schoolGuid}/Classes")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> AddNewClass([FromRoute] Guid schoolGuid, [FromBody] NewClassModel model)
    {
        var command = _mapper.Service.Map<NewClassCommand>(model);
        command.SchoolGuid = schoolGuid;
        var resp = await _foundationCommands.Service.AddNewClass(command);
        return resp.ObjectResult;
    }
    [HttpGet]
    [Route("{schoolGuid}/Classes")]
    [ProducesResponseType(typeof(IPagedList<ClassDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetClassesInSchool([FromRoute] Guid schoolGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.GetClassesInSchool(schoolGuid, page ?? 0, query);
        return resp.ObjectResult;
    }
}
