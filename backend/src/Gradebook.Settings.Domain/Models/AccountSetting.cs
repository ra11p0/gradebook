using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Settings.Enums;

namespace Gradebook.Settings.Domain.Models;

public class AccountSetting : BaseDomainModel
{
    public string UserGuid { get; set; } = string.Empty;
    public SettingEnum SettingType { get; set; }
    public string JsonValue { get; set; } = string.Empty;
}
