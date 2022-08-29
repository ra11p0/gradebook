using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Group : BaseDomainModel
{
    public string Name { get; set; }
    
    public virtual List<Student> Students { get; set; }
    
    public Guid ClassGuid { get; set; }
    [ForeignKey("ClassGuid")]
    public virtual Class Class { get; set; }
}
