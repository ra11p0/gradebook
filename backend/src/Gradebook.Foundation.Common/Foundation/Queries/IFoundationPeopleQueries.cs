using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationPeopleQueries
{
    Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleByGuids(IEnumerable<Guid> guids, int page);
}
