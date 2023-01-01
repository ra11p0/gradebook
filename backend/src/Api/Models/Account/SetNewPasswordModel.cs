using System.ComponentModel.DataAnnotations;

namespace Api.Models.Account;

public class SetNewPasswordModel
{
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? ConfirmPassword { get; set; }
}
