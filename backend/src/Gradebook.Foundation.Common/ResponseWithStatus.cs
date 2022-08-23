namespace Gradebook.Foundation.Common;

public class ResponseWithStatus<R, S>
{
    public readonly S Status;
    public readonly R? Response;
    public ResponseWithStatus(R? response, S status)
    {
        Status = status;
        Response = response;
    }
}
