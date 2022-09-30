using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Permissions.Enums;
using Gradebook.Foundation.Domain.Models;

namespace Gradebook.Permissions.Domain.Models;

public class Permission : BaseDomainModel
{
    public Guid PersonGuid { get; set; }
    public PermissionEnum PermissionId { get; set; }
    public PermissionLevelEnum PermissionLevel { get; set; }

    [ForeignKey("PersonGuid")]
    public virtual Person? Person { get; set; }
}
