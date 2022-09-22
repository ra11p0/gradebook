using AutoMapper;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Models;

namespace Gradebook.Foundation.Common.Foundation;

public class FoundationMappings : Profile
{
    public FoundationMappings()
    {
        CreateMap<NewClassModel, NewClassCommand>();
        CreateMap<NewStudentModel, NewStudentCommand>();
        CreateMap<NewTeacherModel, NewTeacherCommand>();
        CreateMap<NewSchoolModel, NewSchoolCommand>();
        CreateMap<NewAdministratorModel, NewAdministratorCommand>();
    }
}
