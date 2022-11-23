using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycleStep : BaseDomainModel
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public int Order { get; set; }
    [Required]
    public Guid EducationCycleGuid { get; set; }
    public virtual ICollection<EducationCycleStepSubject>? EducationCycleStepSubjects { get; set; }

    [ForeignKey("EducationCycleGuid")]
    public virtual EducationCycle EducationCycle { get; set; } = new();
}
