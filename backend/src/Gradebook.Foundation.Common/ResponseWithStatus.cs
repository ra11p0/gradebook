namespace Gradebook.Foundation.Common;

public class ResponseWithStatus<R, S> : ResponseWithStatus<S> where S : struct
{

    public readonly R? Response;
    public ResponseWithStatus(R? response, S status, string? message = null): base(status, message)
    {
        Response = response;
    }
}

public class ResponseWithStatus<S> where S : struct
{
    public readonly S Status;
    public readonly string? Message;
    public ResponseWithStatus(S status, string? message = null)
    {
        Status = status;
        Message = message;
    }
}
