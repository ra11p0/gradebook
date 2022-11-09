using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Domain.Models;

public class Student : Person
{
    public virtual ICollection<Class>? Classes { get; set; }
    public virtual ICollection<Group>? Groups { get; set; }
    public Guid? CurrentClassGuid { get; set; }
    [ForeignKey("CurrentClassGuid")]
    public Class? CurrentClass { get; set; }
}
