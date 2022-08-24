using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class StudentsAbsence : BaseDomainModel
{
    public DateTime SinceDate { get; set; }
    public DateTime UntilDate { get; set; }

    [ForeignKey("Students")]
    public Guid StudentGuid { get; set; }
    public virtual Student Student { get; set; }
}
