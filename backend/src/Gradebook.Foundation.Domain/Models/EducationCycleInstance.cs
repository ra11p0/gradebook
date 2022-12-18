using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycleInstance : BaseDomainModel
{
    public Guid ClassGuid { get; set; }
    public Guid EducationCycleGuid { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public Guid CreatorGuid { get; set; }


    [ForeignKey("ClassGuid")]
    public virtual Class? Class { get; set; }
    [ForeignKey("EducationCycleGuid")]
    public virtual EducationCycle? EducationCycle { get; set; }
    [ForeignKey("CreatorGuid")]
    public virtual Person? Creator { get; set; }
    public virtual ICollection<EducationCycleStepInstance>? EducationCycleStepInstances { get; set; }

}
