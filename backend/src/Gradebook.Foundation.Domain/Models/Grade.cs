using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Grade : BaseDomainModel
{
    public int Mark { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }


    [ForeignKey("Teachers")]
    public Guid TeacherGuid { get; set; }
    public virtual Teacher Teacher { get; set; }

    [ForeignKey("Students")]
    public Guid StudentGuid { get; set; }
    public virtual Student Student { get; set; }

    [ForeignKey("Lessons")]
    public Guid LessonGuid { get; set; }
    public Lesson Lesson { get; set; }

    [ForeignKey("Grades")]
    public Guid RelatedGradeGuid { get; set; }
    public virtual Grade RelatedGrade { get; set; }
}
