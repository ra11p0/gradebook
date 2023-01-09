using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries.Repositories.Interfaces;

public interface IFoundationQueriesPeopleRepository
{
    Task<IEnumerable<StudentDto>> GetAllAccessibleStudents(Guid schoolGuid);
    Task<IEnumerable<StudentDto>> GetAllInactiveAccessibleStudents(Guid schoolGuid);
    Task<IEnumerable<TeacherDto>> GetAllAccessibleTeachers(Guid relatedPersonGuid);
    Task<PersonDto> GetPersonByGuid(Guid guid);
    Task<StudentDto> GetStudentByGuid(Guid guid);
    Task<TeacherDto> GetTeacherByGuid(Guid guid);
    Task<AdminDto> GetAdminByGuid(Guid guid);
    Task<IPagedList<PersonDto>> GetPeopleByGuids(IEnumerable<Guid> guids, Pager pager);
    Task<IPagedList<PersonDto>> SearchPeople(PeoplePickerData pickerData, Pager pager);
}
