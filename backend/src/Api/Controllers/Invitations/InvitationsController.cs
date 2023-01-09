using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
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
    [Route("Activation/Code")]
    public async Task<IActionResult> GetActivationCodeInfo([FromQuery] string activationCode)
    {
        var resp = await _foundationQueries.Service.GetActivationCodeInfo(activationCode);
        return resp.ObjectResult;
    }
}
