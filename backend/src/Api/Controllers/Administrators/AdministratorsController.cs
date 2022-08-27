using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invitations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdministratorsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IMapper> _mapper;
    public AdministratorsController(IServiceProvider serviceProvider)
    {
        _foundationCommands = new ServiceResolver<IFoundationCommands>(serviceProvider);
        _mapper = new ServiceResolver<IMapper>(serviceProvider);
    }
    [HttpPost]
    [Route("{userGuid}")]
    public async Task<IActionResult> ActivateAdministrator([FromRoute] string userGuid, [FromBody] ActivateAdministratorModel model){
        var command = _mapper.Service.Map<ActivateAdministratorCommand>(model);
        var resp = await _foundationCommands.Service.ActivateAdministrator(command);
        return resp.Status ? Ok():BadRequest();
    }
}
