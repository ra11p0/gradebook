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
    [HttpDelete]
    [Route("{classGuid}/EducationCycles")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> DeleteActiveEducationCycleFromClass([FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.DeleteActiveEducationCycleFromClass(classGuid);
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
    [HttpPatch]
    [Route("{classGuid}/EducationCycles/Instances/Steps/Start")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> StartEducationCycleStepInstance([FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.StartEducationCycleStepInstance(classGuid);
        return resp.ObjectResult;
    }
    [HttpPatch]
    [Route("{classGuid}/EducationCycles/Instances/Steps/Forward")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> ForwardEducationCycleStepInstance([FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.ForwardEducationCycleStepInstance(classGuid);
        return resp.ObjectResult;
    }
    [HttpPatch]
    [Route("{classGuid}/EducationCycles/Instances/Steps/Stop")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> StopEducationCycleStepInstance([FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.StopEducationCycleStepInstance(classGuid);
        return resp.ObjectResult;
    }
}
