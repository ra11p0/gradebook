using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class StudentsAbsence : BaseDomainModel
{
    public DateTime SinceDate { get; set; }
    public DateTime UntilDate { get; set; }

    public Guid StudentGuid { get; set; }
    [ForeignKey("StudentGuid")]
    public virtual Student Student { get; set; }
}
