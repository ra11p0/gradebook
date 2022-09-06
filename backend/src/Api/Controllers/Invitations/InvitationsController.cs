using Api.Models.Invitations;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invitations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvitationsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public InvitationsController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewInvitation([FromBody] NewInvitationModel model)
    {
        var resp = await _foundationCommands.Service.GenerateSystemInvitation(model.InvitedPersonGuid, model.Role);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
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
