namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewSchoolCommand
{
    public string Name { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
