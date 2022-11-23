using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;
public class EducationCycleStepSubject : BaseDomainModel
{
    [Required]
    public Guid SubjectGuid { get; set; }
    [Required]
    public Guid EducationCycleStepGuid { get; set; }
    [Required]
    public int HoursInStep { get; set; }
    public bool IsMandatory { get; set; }
    public bool GroupsAllowed { get; set; }


    [ForeignKey("EducationCycleStepGuid")]
    public virtual EducationCycleStep? EducationCycleStep { get; set; }
    [ForeignKey("SubjectGuid")]
    public virtual Subject? Subject { get; set; }
}


