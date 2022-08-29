using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class School : BaseDomainModel
{
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public ICollection<Person> People { get; set; }
}
