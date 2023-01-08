using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Schools;

public partial class SchoolsController
{
    [HttpGet]
    [Route("{schoolGuid}/People")]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetPeopleInSchool([FromRoute] Guid schoolGuid)
    {
        var resp = await _foundationQueries.Service.GetPeopleInSchool(schoolGuid);
        return resp.Status ? Ok(resp.Response) : BadRequest(resp.Message);
    }
    [HttpGet]
    [Route("{schoolGuid}/People/Search")]
    [ProducesResponseType(typeof(IPagedList<PersonDto>), statusCode: 200)]
    [ProducesResponseType(typeof(string), statusCode: 400)]
    public async Task<IActionResult> GetPeopleInSchoolSearch([FromRoute] Guid schoolGuid, [FromQuery] int page = 1, [FromQuery] string? discriminator = "", [FromQuery] string? query = "")
    {
        var resp = await _foundationQueries.Service.SearchPeople(new PeoplePickerData()
        {
            SchoolGuid = schoolGuid,
            SchoolRole = (discriminator ?? string.Empty) switch { "Teacher" => SchoolRoleEnum.Teacher, "Student" => SchoolRoleEnum.Student, "Administrator" => SchoolRoleEnum.Admin, _ => 0 },
            Query = query
        }, page);
        return resp.ObjectResult;
    }

    /* [HttpPost]
     [Route("{schoolGuid}/People")]
     public async Task<IActionResult> AddPersonToSchool([FromRoute] Guid schoolGuid, [FromBody] Guid personGuid)
     {
         var resp = await _foundationCommands.Service.AddPersonToSchool(schoolGuid, personGuid);
         return resp.ObjectResult;
     }*/
}
