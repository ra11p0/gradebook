using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Domain.Models;

public class Student : Person
{
    public Guid? GroupGuid { get; set; }
    [ForeignKey("GroupGuid")]
    public Group Group { get; set; }
    public Guid? ClassGuid { get; set; }
    [ForeignKey("ClassGuid")]
    public virtual Class Class { get; set; }
}
