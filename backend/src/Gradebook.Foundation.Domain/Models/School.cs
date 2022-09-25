using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class School : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public virtual ICollection<Person>? People { get; set; }
    public virtual ICollection<Class>? Classes { get; set; }
}
