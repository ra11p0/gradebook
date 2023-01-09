namespace Gradebook.Foundation.Mailservice.MailTypesModels;

public class RemindPasswordMailTypeModel
{
    public string? AuthCode { get; set; }
    public string? AccountGuid { get; set; }
}
