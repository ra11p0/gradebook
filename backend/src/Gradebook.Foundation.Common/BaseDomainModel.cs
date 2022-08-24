namespace Gradebook.Foundation.Common;

public abstract class BaseDomainModel
{
    protected BaseDomainModel()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Guid { get; set; }
    public int Id { get; set; }
}
