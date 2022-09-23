using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Api.Models.Account;

public class SchoolResponseModel
{
    public SchoolDto School { get; set; } = new();
    public PersonDto Person { get; set; } = new();
}
