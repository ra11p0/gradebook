using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Lesson : BaseDomainModel
{
    public DateTime DateSince { get; set; }
    public DateTime? DateUntil { get; set; }
    public Guid StartingPersonGuid { get; set; }
    public Guid EducationCycleStepSubjectInstanceGuid { get; set; }


    [ForeignKey("EducationCycleStepSubjectInstanceGuid")]
    public virtual EducationCycleStepSubjectInstance? EducationCycleStepSubjectInstance { get; set; }
    [ForeignKey("StartingPersonGuid")]
    public virtual Person? StartingPerson { get; set; }
}
