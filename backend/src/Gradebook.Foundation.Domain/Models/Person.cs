using System.ComponentModel.DataAnnotations.Schema;
using Gradebook.Foundation.Common;

namespace Gradebook.Foundation.Domain.Models;

public class Person : BaseDomainModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserGuid { get; set; }
}
