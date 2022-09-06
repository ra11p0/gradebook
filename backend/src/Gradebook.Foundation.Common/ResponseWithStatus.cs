namespace Gradebook.Foundation.Common;

public class ResponseWithStatus<R, S> : StatusResponse<S> where S : struct
{

    public readonly R? Response;
    public ResponseWithStatus(R? response, S status, string? message = null) : base(status, message)
    {
        Response = response;
    }
    public ResponseWithStatus(S status, string? message = null) : this(default, status, message)
    {
    }
    public ResponseWithStatus(string? message = null) : this(default, message)
    {
    }
}

public class ResponseWithStatus<R> : ResponseWithStatus<R, bool>
{
    public ResponseWithStatus(string? message = null) : this(false, message)
    {
    }
    public ResponseWithStatus(bool status, string? message = null) : this(default, status, message)
    {
    }
    public ResponseWithStatus(R? response, bool status, string? message = null) : base(response, status, message)
    {

    }
}
public class StatusResponse : StatusResponse<bool>
{
    public StatusResponse(bool status, string? message = null) : base(status, message)
    {
    }
    public StatusResponse(string? message = null) : this(false, message)
    {
    }
}

public class StatusResponse<S> where S : struct
{
    public readonly S Status;
    public readonly string? Message;
    public StatusResponse(S status, string? message = null)
    {
        Status = status;
        Message = message;
    }
}
