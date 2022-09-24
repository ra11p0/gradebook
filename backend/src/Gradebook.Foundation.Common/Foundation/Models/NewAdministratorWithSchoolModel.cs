namespace Gradebook.Foundation.Common.Foundation.Models;

public class NewAdministratorWithSchoolModel
{
    public NewAdministratorModel Administrator { get; set; } = new();
    public NewSchoolModel School { get; set; } = new();
}
