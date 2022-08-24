using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Group : BaseDomainModel
{
    public string Name { get; set; }
    
    [ForeignKey("Students")]
    public virtual List<Student> Students { get; set; }
    
    [ForeignKey("Classs")]
    public Guid ClassGuid { get; set; }
    public virtual Class Class { get; set; }
}
