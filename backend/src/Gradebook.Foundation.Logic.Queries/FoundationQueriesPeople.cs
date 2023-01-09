using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Models;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries : IFoundationPeopleQueries
{

    public async Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleByGuids(IEnumerable<Guid> guids, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetPeopleByGuids(guids, pager);
        return new ResponseWithStatus<IPagedList<PersonDto>>(resp);
    }

    public async Task<ResponseWithStatus<IPagedList<PersonDto>>> SearchPeople(PeoplePickerData pickerData, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.SearchPeople(pickerData, pager);
        return new ResponseWithStatus<IPagedList<PersonDto>>(resp);
    }
    public async Task<ResponseWithStatus<TeacherDto, bool>> GetTeacherByGuid(Guid guid)
    {
        var resp = await Repository.GetTeacherByGuid(guid);
        if (resp is null) return new ResponseWithStatus<TeacherDto, bool>(404);
        return new ResponseWithStatus<TeacherDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<StudentDto, bool>> GetStudentByGuid(Guid guid)
    {
        var resp = await Repository.GetStudentByGuid(guid);
        if (resp is null) return new ResponseWithStatus<StudentDto, bool>(404);
        return new ResponseWithStatus<StudentDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<AdminDto>> GetAdminByGuid(Guid guid)
    {
        var resp = await Repository.GetAdminByGuid(guid);
        if (resp is null) return new ResponseWithStatus<AdminDto>(404);
        return new ResponseWithStatus<AdminDto>(resp, true);
    }
}
