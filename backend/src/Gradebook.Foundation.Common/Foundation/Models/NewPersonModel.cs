using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Common.Foundation.Models;

public class NewPersonModel
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? UserGuid { get; set; }
    public DateTime? Birthday { get; set; }
}
