namespace Gradebook.Foundation.Common;

public interface IValidatable<Target, Status>
{
    public Validator<Target, Status> Validator { get; } 
    public Status IsValid =>  Validator.Validate((Target) this);
}
