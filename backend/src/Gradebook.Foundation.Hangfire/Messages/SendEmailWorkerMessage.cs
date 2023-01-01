using Gradebook.Foundation.Common.Hangfire;

namespace Gradebook.Foundation.Hangfire.Messages;

public class SendEmailWorkerMessage : BaseHangfireWorkerMessage
{
    public string? To { get; set; }
    public string? From { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public string? PayloadJson { get; set; }
    public string? MessageType { get; set; }
}
