using Api.Models.Invitations;
using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.People;

[Route("api/[controller]")]
[ApiController]
[Authorize()]
public class PeopleController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    private readonly ServiceResolver<IMapper> _mapper;
    public PeopleController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _mapper = serviceProvider.GetResolver<IMapper>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
    }
    [HttpGet]
    [Route("{userGuid}/schools")]
    public async Task<IActionResult> GetSchools([FromRoute] string userGuid)
    {
        var userId = await _foundationQueries.Service.GetPersonGuidForUser(userGuid);
        if(!userId.Status)
            return BadRequest(userId.Message);
        var resp = await _foundationQueries.Service.GetSchoolsForPerson(userId.Response);
        return resp.Status ? Ok(resp.Response) : BadRequest();
    }
}
