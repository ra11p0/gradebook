using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Class : BaseDomainModel
{
    [ForeignKey("Students")]
    public virtual List<Student> Students { get; set; }
}
