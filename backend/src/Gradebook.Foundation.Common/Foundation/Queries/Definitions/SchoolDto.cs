namespace Gradebook.Foundation.Common.Foundation.Queries.Definitions;

public class SchoolDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
