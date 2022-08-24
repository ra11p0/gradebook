using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Grade : BaseDomainModel
{
    public int Mark { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }


    public Guid TeacherGuid { get; set; }
    [ForeignKey("TeacherGuid")]
    public virtual Teacher Teacher { get; set; }

    public Guid StudentGuid { get; set; }
    [ForeignKey("StudentGuid")]
    public virtual Student Student { get; set; }

    public Guid LessonGuid { get; set; }
    [ForeignKey("LessonGuid")]
    public Lesson Lesson { get; set; }

    public Guid RelatedGradeGuid { get; set; }
    [ForeignKey("RelatedGradeGuid")]
    public virtual Grade RelatedGrade { get; set; }
}
