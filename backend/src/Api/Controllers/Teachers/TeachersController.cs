using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Teachers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public TeachersController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllAccessibleTeachers()
    {
        var response = await _foundationQueries.Service.GetAllAccessibleTeachers();
        return response.Status ? Ok(response.Response) : BadRequest(response.Message);
    }
}
