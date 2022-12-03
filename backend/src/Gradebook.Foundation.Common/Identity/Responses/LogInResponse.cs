namespace Gradebook.Foundation.Common.Identity.Responses;

public class LogInResponse
{
    public string access_token { get; set; } = string.Empty;
    public string refresh_token { get; set; } = string.Empty;
    public int expires_in { get; set; }
}