using Api.Models.Invitations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invitations;

[Route("api/[controller]")]
[ApiController]
[Authorize("Teacher")]
public class InvitationsController : ControllerBase
{
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddNewInvitation([FromBody] NewInvitationModel model){
        return Ok();
    }
}
