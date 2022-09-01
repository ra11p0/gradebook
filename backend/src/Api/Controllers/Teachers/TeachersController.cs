using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
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
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public TeachersController(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetResolver<IMapper>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewTeacher([FromBody] NewTeacherModel model){
        var command = _mapper.Service.Map<NewTeacherCommand>(model);
        var response = await _foundationCommands.Service.AddNewTeacher(command);
        return response.Status ? Ok(): BadRequest();
    }
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllAccessibleTeachers(){
        var response = await _foundationQueries.Service.GetAllAccessibleTeachers();
        return response.Status ? Ok(response.Response): BadRequest(response.Message);
    }
}
