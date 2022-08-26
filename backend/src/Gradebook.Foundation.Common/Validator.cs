namespace Gradebook.Foundation.Common;

public class Validator<Req, Res, Stat> where Res : Stat
{
    private Func<Req, Stat> _validationFunction;
    public Validator(Func<Req, Stat> validationFunction)
        => _validationFunction = validationFunction;
    public Stat Validate(Req req) 
        => _validationFunction.Invoke(req);
}
