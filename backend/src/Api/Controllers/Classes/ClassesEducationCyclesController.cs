using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Classes;

public partial class ClassesController
{
    [HttpGet]
    [Route("{classGuid}/EducationCycles")]
    [ProducesResponseType(typeof(EducationCyclesForClassDto), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> GetEducationCyclesInClass([FromRoute] Guid classGuid)
    {
        var resp = await _foundationQueries.Service.GetEducationCyclesByClassGuid(classGuid);
        return resp.ObjectResult;
    }

    [HttpPost]
    [Route("{classGuid}/EducationCycles/Current/Configuration")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> ConfigureCurrentEducationCycle([FromRoute] Guid classGuid, [FromBody] EducationCycleConfigurationCommand configuration)
    {
        var resp = await _foundationCommands.Service.ConfigureEducationCycleForClass(classGuid, configuration);
        return resp.ObjectResult;
    }
}
