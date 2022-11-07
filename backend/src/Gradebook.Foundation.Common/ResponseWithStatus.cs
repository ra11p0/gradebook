namespace Gradebook.Foundation.Common;

public class ResponseWithStatus<R, S> : StatusResponse<S> where S : struct
{

    public readonly R? Response;
    public ResponseWithStatus(string? message = null) : this(400, message)
    {
    }
    public ResponseWithStatus(int statusCode, string? message = null) : this(default, statusCode, message)
    {
    }
    public ResponseWithStatus(S status, int statusCode, string? message = null) : this(default, statusCode, status, message)
    {
    }
    public ResponseWithStatus(R? response, S status, string? message = null) : this(response, 0, status, message)
    {
    }
    public ResponseWithStatus(R? response, int statusCode, S status, string? message = null) : base(statusCode, status, message)
    {
        Response = response;
    }
}

public class ResponseWithStatus<R> : ResponseWithStatus<R, bool>
{
    public ResponseWithStatus(R? response) : this(response, true, default)
    {
    }
    public ResponseWithStatus(string? message = null) : this(400, false, message)
    {
    }
    public ResponseWithStatus(int statusCode, string? message = null) : this(statusCode, false, message)
    {
    }
    public ResponseWithStatus(bool status, string? message = null) : this(400, status, message)
    {
    }
    public ResponseWithStatus(int statusCode, bool status, string? message = null) : this(default, statusCode, status, message)
    {
    }
    public ResponseWithStatus(R? response, bool status, string? message = null) : this(response, 0, status, message)
    {
    }
    public ResponseWithStatus(R? response, int statusCode, bool status, string? message = null) : base(response, statusCode, status, message)
    {
    }
}
public class StatusResponse : StatusResponse<bool>
{


    public StatusResponse(string? message = null) : this(400, message)
    {
    }
    public StatusResponse(bool status, string? message = null) : this(0, status, message)
    {
    }
    public StatusResponse(int statusCode, string? message = null) : this(statusCode, false, message)
    {
    }
    public StatusResponse(int statusCode, bool status, string? message = null) : base(statusCode, status, message)
    {
    }
}

public class StatusResponse<S> where S : struct
{
    public readonly S Status;
    public readonly int StatusCode;
    public readonly string? Message;

    public StatusResponse(int statusCode, S status, string? message = null)
    {
        Status = status;
        Message = message;
        StatusCode = statusCode;
    }
    public StatusResponse(S status, string? message = null) : this(0, status, message)
    {
    }
    public StatusResponse(string? message = null) : this(default, message)
    {
    }
}
