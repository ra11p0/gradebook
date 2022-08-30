using System.ComponentModel.DataAnnotations.Schema;

namespace Gradebook.Foundation.Domain.Models;

public class Teacher : Person
{
    public Guid CreatorGuid { get; set; }
    [ForeignKey("CreatorGuid")]
    public Administrator Creator { get; set; }
}
