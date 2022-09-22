using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invitations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvitationsController : ControllerBase
{
    //private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public InvitationsController(IServiceProvider serviceProvider)
    {
        //_foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(IEnumerable<InvitationDto>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> GetMyInvitations()
    {
        var resp = await _foundationQueries.Service.GetInvitations();
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("Activation/Code")]
    public async Task<IActionResult> GetActivationCodeInfo([FromQuery] string activationCode, [FromQuery] string method)
    {
        var resp = await _foundationQueries.Service.GetActivationCodeInfo(activationCode, method);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
}
