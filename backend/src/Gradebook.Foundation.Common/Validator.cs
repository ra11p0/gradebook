namespace Gradebook.Foundation.Common;

public class Validator<Req, Res, Stat> where Res : Stat
{
    private Func<Req, Stat> _validationInstance;
    public Validator(Func<Req, Stat> validationInstance)
        => _validationInstance = validationInstance;
    public Stat Validate(Req req) 
        => _validationInstance.Invoke(req);
}
