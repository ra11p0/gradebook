using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class TeachersAbsence : BaseDomainModel
{
    public DateTime SinceDate { get; set; }
    public DateTime UntilDate { get; set; }

    public Guid TeacherGuid { get; set; }
    [ForeignKey("TeacherGuid")]
    public virtual Teacher Teacher { get; set; }
}
