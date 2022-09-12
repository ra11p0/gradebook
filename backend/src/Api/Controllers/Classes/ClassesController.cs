using Api.Models.Invitations;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public ClassesController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpDelete]
    [Route("{classGuid}")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> DeleteClass([FromRoute] Guid classGuid)
    {
        var resp = await _foundationCommands.Service.DeleteClass(classGuid);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
}
