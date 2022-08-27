using Gradebook.Foundation.Common.Foundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invitations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdministratorsController : ControllerBase
{
    [HttpPost]
    [Route("{userGuid}")]
    public async Task<IActionResult> ActivateAdministrator([FromRoute] string userGuid, [FromBody] ActivateAdministratorModel model){
        return Ok();
    }
}
