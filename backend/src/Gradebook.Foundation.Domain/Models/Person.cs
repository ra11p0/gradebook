using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Domain.Enums;

namespace Gradebook.Foundation.Domain.Models;

public class Person : BaseDomainModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? UserGuid { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }
    public DateTime? Birthday { get; set; }
}
