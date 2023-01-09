using Api.Models.Invitations;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

public partial class SchoolsController
{

    [HttpPost]
    [Route("{schoolGuid}/Invitations")]
    [ProducesResponseType(typeof(string[]), 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> AddMultipleNewInvitations([FromBody] NewMultipleInvitationModel model, [FromRoute] Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return BadRequest(currentPersonGuid.Message);
        if (!await _foundationPermissions.Service.CanInviteToSchool(currentPersonGuid.Response!)) return Forbid();
        var resp = await _foundationCommands.Service.GenerateMultipleSystemInvitation(model.InvitedPersonGuidArray, schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }

    [HttpGet]
    [Route("{schoolGuid}/Invitations")]
    [ProducesResponseType(typeof(IPagedList<InvitationDto>), 200)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<IActionResult> GetMyInvitations([FromRoute] Guid schoolGuid, [FromQuery] int page = 1)
    {
        var resp = await _foundationQueries.Service.GetInvitationsToSchool(schoolGuid, page);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
}
