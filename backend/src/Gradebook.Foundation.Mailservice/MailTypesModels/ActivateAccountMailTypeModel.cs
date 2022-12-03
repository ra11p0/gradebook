namespace Gradebook.Foundation.Mailservice.MailTypesModels;

public class ActivateAccountMailTypeModel
{
    public string? AuthCode { get; set; }
    public string? AccountGuid { get; set; }
}
