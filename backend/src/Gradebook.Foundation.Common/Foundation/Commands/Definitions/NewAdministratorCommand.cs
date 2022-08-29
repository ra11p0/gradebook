namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class NewAdministratorCommand
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthday { get; set; }
    public string? UserGuid { get; set; }
}
