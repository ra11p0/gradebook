using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Administrators;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdministratorsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IMapper> _mapper;
    public AdministratorsController(IServiceProvider serviceProvider)
    {
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _mapper = serviceProvider.GetResolver<IMapper>();
    }
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> NewAdministrator([FromBody] NewAdministratorModel model){
        var command = _mapper.Service.Map<NewAdministratorCommand>(model);
        var resp = await _foundationCommands.Service.NewAdministrator(command);
        return resp.Status ? Ok():BadRequest();
    }

    [HttpPost]
    [Route("withSchool")]
    public async Task<IActionResult> NewAdministratorWithSchool([FromBody] NewAdministratorWithSchoolModel model){
        var administratorCommand = _mapper.Service.Map<NewAdministratorCommand>(model.Administrator);
        var schoolCommand = _mapper.Service.Map<NewSchoolCommand>(model.School);
        var resp = await _foundationCommands.Service.NewAdministratorWithSchool(administratorCommand, schoolCommand);
        return resp.Status ? Ok():BadRequest();
    }
}
