namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class SchoolWithRelatedPersonDto
{
    public SchoolDto School { get; set; } = new();
    public PersonDto Person { get; set; } = new();
}
