using System.ComponentModel.DataAnnotations;

namespace Api.Models.Account;

public class SetNewPasswordAuthorizedModel : SetNewPasswordModel
{
    [Required]
    public string? OldPassword { get; set; }
}
