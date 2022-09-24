using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Api.Models.Account;

public class MeResponseModel
{
    public string UserId { get; set; } = string.Empty;
    public IEnumerable<SchoolWithRelatedPersonDto> Schools { get; set; } = Enumerable.Empty<SchoolWithRelatedPersonDto>();
}
