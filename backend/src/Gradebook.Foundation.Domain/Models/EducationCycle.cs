using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycle : BaseDomainModel
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public Guid SchoolGuid { get; set; }
    [Required]
    public Guid CreatorGuid { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }

    public ICollection<EducationCycleStep>? EducationCycleSteps { get; set; }

    [ForeignKey("SchoolGuid")]
    public virtual School? School { get; set; }
    [ForeignKey("CreatorGuid")]
    public virtual Person? Creator { get; set; }
}
