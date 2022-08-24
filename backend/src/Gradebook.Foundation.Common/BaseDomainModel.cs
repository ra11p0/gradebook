using System.ComponentModel.DataAnnotations;

namespace Gradebook.Foundation.Common;

public abstract class BaseDomainModel
{
    protected BaseDomainModel()
    {
        Guid = Guid.NewGuid();
    }
    [Key]
    public Guid Guid { get; set; }
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}
