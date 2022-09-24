namespace Gradebook.Foundation.Common;

public abstract class Validatable<Target> where Target : Validatable<Target>
{
    private readonly Validator<Validatable<Target>, bool> _validator;
    public bool IsValid => _validator.Validate(this);
    protected Validatable()
    {
        _validator = new Validator<Validatable<Target>, bool>(e => Validate((Target)this));
    }
    protected abstract bool Validate(Target validatable);
}
