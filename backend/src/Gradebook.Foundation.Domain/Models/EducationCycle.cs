using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class EducationCycle : BaseDomainModel
{
    public string? Name { get; set; }
    public Guid SchoolGuid { get; set; }
    public Guid CreatorGuid { get; set; }
    public DateTime CreatedDate { get; set; }

    [ForeignKey("SchoolGuid")]
    public virtual School School { get; set; } = new School();
    [ForeignKey("CreatorGuid")]
    public virtual Person Creator { get; set; } = new Person();
}
