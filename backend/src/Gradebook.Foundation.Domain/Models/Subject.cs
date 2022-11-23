using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Subject : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    public Guid SchoolGuid { get; set; }

    [ForeignKey("SchoolGuid")]
    public School? School { get; set; }
    public virtual ICollection<Teacher>? Teachers { get; set; }
}
