namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewAdministratorCommand
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public string? UserGuid { get; set; }
}
