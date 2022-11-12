using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycleStep : BaseDomainModel
{
    public string? Name { get; set; }
    public int Order { get; set; }
    public Guid EducationCycleGuid { get; set; }
    public virtual ICollection<Subject>? Subjects { get; set; }

    [ForeignKey("EducationCycleGuid")]
    public virtual EducationCycle EducationCycle { get; set; } = new EducationCycle();
}
