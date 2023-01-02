using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationPeopleQueries
{
    Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleByGuids(IEnumerable<Guid> guids, int page);
    Task<ResponseWithStatus<IPagedList<PersonDto>>> SearchPeople(PeoplePickerData pickerData, int page);
}
