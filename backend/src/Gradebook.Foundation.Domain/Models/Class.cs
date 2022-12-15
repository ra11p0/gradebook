using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Class : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid SchoolGuid { get; set; }
    public Guid? ActiveEducationCycleGuid { get; set; }

    //**********
    [ForeignKey("SchoolGuid")]
    public School? School { get; set; }
    [ForeignKey("ActiveEducationCycleGuid")]
    public EducationCycle? ActiveEducationCycle { get; set; }
    public virtual ICollection<Student>? Students { get; set; }
    public virtual ICollection<Student>? ActiveStudents { get; set; }
    public virtual ICollection<Teacher>? OwnersTeachers { get; set; }
}
