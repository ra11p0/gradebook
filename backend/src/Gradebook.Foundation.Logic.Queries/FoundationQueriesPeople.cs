using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries : IFoundationPeopleQueries
{

    public async Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleByGuids(IEnumerable<Guid> guids, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetPeopleByGuids(guids, pager);
        return new ResponseWithStatus<IPagedList<PersonDto>>(resp);
    }

    public async Task<ResponseWithStatus<IPagedList<PersonDto>>> SearchPeople(PeoplePickerData pickerData, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.SearchPeople(pickerData, pager);
        return new ResponseWithStatus<IPagedList<PersonDto>>(resp);
    }
}
