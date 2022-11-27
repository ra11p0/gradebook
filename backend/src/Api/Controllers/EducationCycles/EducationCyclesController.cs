using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Extensions;

namespace Api.Controllers.EducationCycles;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EducationCyclesController : ControllerBase
{
    //private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public EducationCyclesController(IServiceProvider serviceProvider)
    {
        //_foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpGet]
    [Route("{educationCycleGuid}")]
    public async Task<ObjectResult> GetActivationCodeInfo([FromRoute] Guid educationCycleGuid)
    {
        var resp = await _foundationQueries.Service.GetEducationCycle(educationCycleGuid);
        return resp.ObjectResult;
    }
}
