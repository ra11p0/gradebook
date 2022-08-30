using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SchoolsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public SchoolsController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }
    [HttpGet]
    [Route("{schoolGuid}/People")]
    public async Task<IActionResult> GetPeopleInSchool([FromRoute] Guid schoolGuid){
        var resp = await _foundationQueries.Service.GetPeopleInSchool(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
}
