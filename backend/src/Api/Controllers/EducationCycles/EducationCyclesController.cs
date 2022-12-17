using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Api.Controllers.EducationCycles;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EducationCyclesController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public EducationCyclesController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpGet]
    [Route("{educationCycleGuid}")]
    [ProducesResponseType(typeof(EducationCycleExtendedDto), statusCode: 200)]
    public async Task<ObjectResult> GetActivationCodeInfo([FromRoute] Guid educationCycleGuid)
    {
        var resp = await _foundationQueries.Service.GetEducationCycle(educationCycleGuid);
        return resp.ObjectResult;
    }
    [HttpGet]
    [Route("{educationCycleGuid}/Classes")]
    [ProducesResponseType(typeof(IPagedList<ClassDto>), statusCode: 200)]
    public async Task<ObjectResult> GetClassesInEducationCycle([FromRoute] Guid educationCycleGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.GetClassesForEducationCycle(educationCycleGuid, page ?? 0, query);
        return resp.ObjectResult;
    }
    [HttpGet]
    [Route("{educationCycleGuid}/Classes/Available")]
    [ProducesResponseType(typeof(IPagedList<ClassDto>), statusCode: 200)]
    public async Task<ObjectResult> GetAvalibleClassesInEducationCycle([FromRoute] Guid educationCycleGuid, [FromQuery] int? page = 0, [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.GetAvalibleClassesInForEducationCycle(educationCycleGuid, page ?? 0, query);
        return resp.ObjectResult;
    }
    [HttpPost]
    [Route("{educationCycleGuid}/Classes")]
    [ProducesResponseType(typeof(StatusResponse), statusCode: 200)]
    public async Task<ObjectResult> EditClassesInEducationCycle([FromRoute] Guid educationCycleGuid, [FromBody] Guid[] classesGuids)
    {
        var resp = await _foundationCommands.Service.EditClassesAssignedToEducationCycle(classesGuids, educationCycleGuid);
        return resp.ObjectResult;
    }
    [HttpPut]
    [Route("{educationCycleGuid}/Classes/{classGuid}")]
    [ProducesResponseType(typeof(StatusResponse), statusCode: 200)]
    public async Task<ObjectResult> AddClassToEducationCycle([FromRoute] Guid educationCycleGuid, [FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.SetActiveEducationCycleToClass(classGuid, educationCycleGuid);
        return resp.ObjectResult;
    }
}
