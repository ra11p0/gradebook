using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Domain.Models;

public class Student : Person
{
    public virtual ICollection<Class>? Classes { get; set; }
    public virtual ICollection<Group>? Groups { get; set; }
}
