using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Teachers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ServiceResolver<IMapper> _mapper;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    public TeachersController(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetResolver<IMapper>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
    }
        [HttpPost]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewStudent([FromBody] NewTeacherModel model){
        var command = _mapper.Service.Map<NewTeacherCommand>(model);
        var response = await _foundationCommands.Service.AddNewTeacher(command);
        return response.Status ? Ok(): BadRequest();
    }
}
