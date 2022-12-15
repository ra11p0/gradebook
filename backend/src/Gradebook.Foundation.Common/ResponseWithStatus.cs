using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    public ResponseWithStatus(R? response, S status, string? message = null) : this(response, default(S).Equals(status) ? 400 : 200, status, message)
    {
    }
    public ResponseWithStatus(R? response, int statusCode, S status, string? message = null) : base(statusCode, status, message)
    {
        Response = response;
    }
    public override sealed ObjectResult ObjectResult => new ObjectResult(StatusCode is 200 ? Response : Message) { StatusCode = StatusCode };
}

public class ResponseWithStatus<R> : ResponseWithStatus<R, bool>
{
    public ResponseWithStatus(R? response) : this(response, true, default)
    {
    }
    public ResponseWithStatus(string? message = null) : this(400, false, message)
    {
    }
    public ResponseWithStatus(int statusCode, string? message = null) : this(statusCode, statusCode is 200 ? true : false, message)
    {
    }
    public ResponseWithStatus(bool status, string? message = null) : this(status ? 200 : 400, status, message)
    {
    }
    public ResponseWithStatus(int statusCode, bool status, string? message = null) : this(default, statusCode, status, message)
    {
    }
    public ResponseWithStatus(R? response, bool status, string? message = null) : this(response, status ? 200 : 400, status, message)
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
    public StatusResponse(bool status, string? message = null) : this(status ? 200 : 400, status, message)
    {
    }
    public StatusResponse(int statusCode, string? message = null) : this(statusCode, statusCode is 200 ? true : false, message)
    {
    }
    public StatusResponse(int statusCode, bool status, string? message = null) : base(statusCode, status, message)
    {
    }
}

public class StatusResponse<S> where S : struct
{
    private readonly string? _statusMessage;
    public readonly S Status;
    public readonly HttpStatusCode HttpStatusCode;
    public int StatusCode => (int)HttpStatusCode;
    public string Message => _statusMessage ?? ((HttpStatusCode)StatusCode).ToString();

    public StatusResponse(int statusCode, S status, string? message = null)
    {
        Status = status;
        HttpStatusCode = (HttpStatusCode)statusCode;
        _statusMessage = message;
    }
    public StatusResponse(HttpStatusCode statusCode, S status, string? message = null) : this((int)statusCode, status, message)
    {
    }
    public StatusResponse(S status, string? message = null) : this(default(S).Equals(status) ? 400 : 200, status, message)
    {
    }
    public StatusResponse(string? message = null) : this(default, message)
    {
    }

    public virtual ObjectResult ObjectResult => new ObjectResult(Message) { StatusCode = StatusCode };
}
