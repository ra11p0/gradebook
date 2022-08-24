using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class TeachersAbsence : BaseDomainModel
{
    public DateTime SinceDate { get; set; }
    public DateTime UntilDate { get; set; }

    [ForeignKey("Teachers")]
    public Guid TeacherGuid { get; set; }
    public virtual Teacher Teacher { get; set; }
}
