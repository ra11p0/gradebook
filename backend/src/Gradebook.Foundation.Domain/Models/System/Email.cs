using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models.System;

public class Email : BaseDomainModel
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime SendDateTime { get; set; } = Time.UtcNow;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
