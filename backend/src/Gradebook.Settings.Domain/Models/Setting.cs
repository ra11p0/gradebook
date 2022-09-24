using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;
using Gradebook.Foundation.Domain.Models;

namespace Gradebook.Settings.Domain.Models;

public class Setting : BaseDomainModel
{
    public Guid PersonGuid { get; set; }
    public SettingEnum SettingType { get; set; }
    public string JsonValue { get; set; } = string.Empty;

    [ForeignKey("PersonGuid")]
    public virtual Person? Person { get; set; }
}
