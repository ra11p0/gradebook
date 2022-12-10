using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycleStepSubjectInstance : BaseDomainModel
{
    public Guid AssignedTeacherGuid { get; set; }
    public Guid EducationCycleStepInstanceGuid { get; set; }

    [ForeignKey("EducationCycleStepInstanceGuid")]
    public virtual EducationCycleStepInstance? EducationCycleStepInstance { get; set; }
    [ForeignKey("AssignedTeacherGuid")]
    public virtual Teacher? AssignedTeacher { get; set; }
}
