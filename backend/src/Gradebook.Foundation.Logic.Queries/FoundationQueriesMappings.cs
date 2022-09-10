using AutoMapper;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueriesMappings : Profile
{
    public FoundationQueriesMappings()
    {
        CreateMap<StudentDto, PersonDto>();
        CreateMap<TeacherDto, PersonDto>();
    }
}
