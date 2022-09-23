using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
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
    //private readonly ServiceResolver<IMapper> _mapper;
    public PeopleController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        //_mapper = serviceProvider.GetResolver<IMapper>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
    }
    [HttpDelete]
    [Route("{personGuid}")]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> DeletePerson([FromRoute] Guid personGuid)
    {
        var resp = await _foundationCommands.Service.DeletePerson(personGuid);
        return resp.Status ? Ok() : BadRequest();
    }
    [HttpPost]
    [Route("Activation/Code/{activationCode}")]
    public async Task<IActionResult> ActivatePerson([FromRoute] string activationCode)
    {
        var resp = await _foundationCommands.Service.ActivatePerson(activationCode);
        return resp.Status ? Ok() : BadRequest(resp.Message);
    }
}
