using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Subject : BaseDomainModel
{
    public Guid SchoolGuid { get; set; }
    public string Name { get; set; } = string.Empty;

    [ForeignKey("SchoolGuid")]
    public virtual School School { get; set; } = new School();
}
