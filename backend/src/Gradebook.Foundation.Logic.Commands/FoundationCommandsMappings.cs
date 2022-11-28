using AutoMapper;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Domain.Models;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsMappings : Profile
{
    public FoundationCommandsMappings()
    {
        CreateMap<NewClassCommand, Class>();
        CreateMap<NewStudentCommand, Student>();
        CreateMap<NewTeacherCommand, Teacher>();
        CreateMap<NewSubjectCommand, Subject>();
        CreateMap<NewSchoolCommand, School>();
        CreateMap<NewPersonCommand, Administrator>();
        CreateMap<NewAdministratorCommand, Administrator>();
    }
}
