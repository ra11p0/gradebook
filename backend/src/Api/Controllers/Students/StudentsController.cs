using Api.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Students;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    [HttpPost]
    [Route("")]
    [Authorize("Teacher")]
    public async Task<IActionResult> AddNewStudent([FromBody] NewStudentModel model){
        return Ok();
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
