using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Classes;

public partial class ClassesController
{
    [HttpGet]
    [Route("{classGuid}/EducationCycles")]
    [ProducesResponseType(typeof(EducationCyclesForClassDto), statusCode: 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ObjectResult> GetEducationCyclesInClass([FromRoute] Guid classGuid)
    {
        var resp = await _foundationQueries.Service.GetEducationCyclesByClassGuid(classGuid);
        return resp.ObjectResult;
    }
}
