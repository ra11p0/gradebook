using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Students;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly ServiceResolver<IMapper> _mapper;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    public StudentsController(IServiceProvider serviceProvider)
    {
        _mapper = new ServiceResolver<IMapper>(serviceProvider);
        _foundationCommands = new ServiceResolver<IFoundationCommands>(serviceProvider);
    }
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model){
        var command = _mapper.Service.Map<NewStudentCommand>(model);
        var response = await _foundationCommands.Service.AddNewStudent(command);
        return response.Status ? Ok(): BadRequest();
    }
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetStudents(){
        return Ok();
    }
    [HttpGet]
    [Route("{guid}")]
    public async Task<IActionResult> GetStudent([FromRoute] string guid){
        return Ok();
    }
}
