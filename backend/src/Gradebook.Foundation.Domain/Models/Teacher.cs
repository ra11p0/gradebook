using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Domain.Models;

public class Teacher : Person
{
    public virtual ICollection<Class>? OwnedClasses { get; set; }
    public virtual ICollection<Group>? OwnedGroups { get; set; }
}
