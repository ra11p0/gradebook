using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Group : BaseDomainModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual ICollection<Student> Students { get; set; }
    public virtual ICollection<Teacher> OwnersTeachers { get; set; }

}
