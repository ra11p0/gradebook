using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Subjects;

public class SubjectsController : ControllerBase
{
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationCommands> _foundationCommands;
    public SubjectsController(IServiceProvider serviceProvider)
    {
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationCommands = serviceProvider.GetResolver<IFoundationCommands>();
    }
    [HttpGet]
    [Route("{subjectGuid}")]
    [ProducesResponseType(typeof(SubjectDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetSubject([FromRoute] Guid subjectGuid)
    {
        var resp = await _foundationQueries.Service.GetSubject(subjectGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
}
