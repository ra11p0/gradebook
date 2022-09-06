using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Class : BaseDomainModel
{
    public string Name { get; set; }
    public virtual ICollection<Student> Students { get; set; }
}
