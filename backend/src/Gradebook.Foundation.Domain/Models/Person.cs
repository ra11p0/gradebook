using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Enums;

namespace Gradebook.Foundation.Domain.Models;

public class Person : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? UserGuid { get; set; }
    public SchoolRoleEnum SchoolRole { get; set; }
    public DateTime? Birthday { get; set; }
    public Guid? CreatorGuid { get; set; }
    public Guid? SchoolGuid { get; set; }

    //**********

    [ForeignKey("CreatorGuid")]
    public Administrator? Creator { get; set; }
    [ForeignKey("SchoolGuid")]
    public School? School { get; set; }
}
