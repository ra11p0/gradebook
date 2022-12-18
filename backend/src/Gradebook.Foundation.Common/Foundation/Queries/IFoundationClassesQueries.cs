using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Common.Foundation.Queries;

public interface IFoundationClassesQueries
{
    Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesInSchool(Guid schoolGuid, int page, string? query = "");
    Task<ResponseWithStatus<ClassDto, bool>> GetClassByGuid(Guid guid);
    Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesForPerson(Guid personGuid, int page);
    Task<ResponseWithStatus<bool>> IsClassOwner(Guid classGuid, Guid personGuid);
    Task<ResponseWithStatus<IPagedList<StudentDto>>> GetStudentsInClass(Guid classGuid, int page);
    Task<ResponseWithStatus<IPagedList<TeacherDto>>> GetTeachersInClass(Guid classGuid, int page);
    Task<ResponseWithStatus<IEnumerable<StudentDto>>> GetAllStudentsInClass(Guid classGuid);
    Task<ResponseWithStatus<IEnumerable<TeacherDto>>> GetAllTeachersInClass(Guid classGuid);
    Task<ResponseWithStatus<Guid?>> GetPreviousEducationCycleStepInstanceGuid(Guid classGuid);
    Task<ResponseWithStatus<Guid?>> GetCurrentEducationCycleStepInstanceGuid(Guid classGuid);
    Task<ResponseWithStatus<Guid?>> GetNextEducationCycleStepInstanceGuid(Guid classGuid);
    Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetPreviousEducationCycleStepInstance(Guid classGuid);
    Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetCurrentEducationCycleStepInstance(Guid classGuid);
    Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetNextEducationCycleStepInstance(Guid classGuid);
}
