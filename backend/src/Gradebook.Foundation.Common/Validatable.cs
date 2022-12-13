namespace Gradebook.Foundation.Common;

public abstract class Validatable
{
    public bool IsValid => Validate().Status;
    protected abstract StatusResponse Validate();
}
