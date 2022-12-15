using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycleStepInstance : BaseDomainModel
{
    public Guid EducationCycleStepGuid { get; set; }
    public Guid EducationCycleInstanceGuid { get; set; }
    public DateTime? DateSince { get; set; }
    public DateTime? DateUntil { get; set; }

    [ForeignKey("EducationCycleInstanceGuid")]
    public virtual EducationCycleInstance? EducationCycleInstance { get; set; }
    [ForeignKey("EducationCycleStepGuid")]
    public virtual EducationCycleStep? EducationCycleStep { get; set; }
    public virtual ICollection<EducationCycleStepSubjectInstance>? EducationCycleStepSubjectInstances { get; set; }
}
