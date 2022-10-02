using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;

namespace Gradebook.Foundation.Logic.Queries;

public class FoundationQueries : BaseLogic<IFoundationQueriesRepository>, IFoundationQueries
{
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IMapper> _mapper;
    public FoundationQueries(IFoundationQueriesRepository repository, IServiceProvider serviceProvider) : base(repository)
    {
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _mapper = serviceProvider.GetResolver<IMapper>();
    }

    public async Task<ResponseWithStatus<ActivationCodeInfoDto>> GetActivationCodeInfo(string activationCode, string method)
    {
        var invitationResponse = await GetInvitationByActivationCode(activationCode);
        if (!invitationResponse.Status) return new ResponseWithStatus<ActivationCodeInfoDto>(invitationResponse.Message);
        if (invitationResponse.Response!.IsUsed) return new ResponseWithStatus<ActivationCodeInfoDto>("Invitation code is used");
        if (invitationResponse.Response!.ExprationDate < DateTime.Now) return new ResponseWithStatus<ActivationCodeInfoDto>("Invitation code expired");

        var invitation = invitationResponse.Response!;
        var invitedPersonGuid = invitation.InvitedPersonGuid;
        if (invitedPersonGuid is null) return new ResponseWithStatus<ActivationCodeInfoDto>("There is no information about activation code");

        var response = new ActivationCodeInfoDto();

        switch (method)
        {
            case "student":
                var studentResponse = await GetStudentByGuid(invitedPersonGuid.Value);
                if (!studentResponse.Status) return new ResponseWithStatus<ActivationCodeInfoDto>(studentResponse.Message);
                response.Person = _mapper.Service.Map<PersonDto>(studentResponse.Response);
                break;
            case "teacher":
                var teacherResponse = await GetTeacherByGuid(invitedPersonGuid.Value);
                if (!teacherResponse.Status) return new ResponseWithStatus<ActivationCodeInfoDto>(teacherResponse.Message);
                response.Person = _mapper.Service.Map<PersonDto>(teacherResponse.Response);
                break;
            default:
                return new ResponseWithStatus<ActivationCodeInfoDto>("Method not found");
        }
        return new ResponseWithStatus<ActivationCodeInfoDto>(response, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<StudentDto>, bool>> GetAllAccessibleStudents(Guid schoolGuid)
    {
        var relatedPersonGuid = await GetCurrentPersonGuid(schoolGuid);
        if (!relatedPersonGuid.Status) return new ResponseWithStatus<IEnumerable<StudentDto>, bool>(null, false, "Cannot recognise current person");
        var students = await Repository.GetAllAccessibleStudents(relatedPersonGuid.Response);
        return new ResponseWithStatus<IEnumerable<StudentDto>, bool>(students, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<TeacherDto>, bool>> GetAllAccessibleTeachers(Guid schoolGuid)
    {
        var relatedPersonGuid = await GetCurrentPersonGuid(schoolGuid);
        if (!relatedPersonGuid.Status) return new ResponseWithStatus<IEnumerable<TeacherDto>, bool>(null, false, "Cannot recognise current person");
        var teachers = await Repository.GetAllAccessibleTeachers(relatedPersonGuid.Response);
        return new ResponseWithStatus<IEnumerable<TeacherDto>, bool>(teachers, true);
    }

    public Task<ResponseWithStatus<IEnumerable<StudentDto>>> GetAllStudentsInClass(Guid classGuid)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseWithStatus<IEnumerable<TeacherDto>>> GetAllTeachersInClass(Guid classGuid)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseWithStatus<ClassDto, bool>> GetClassByGuid(Guid guid)
    {
        var resp = await Repository.GetClassByGuid(guid);
        if (resp is null) return new ResponseWithStatus<ClassDto, bool>(null, false, "Class does not exist");
        return new ResponseWithStatus<ClassDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesInSchool(Guid schoolGuid, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesInSchool(schoolGuid, pager);
        if (resp is null) return new ResponseWithStatus<IPagedList<ClassDto>>(404);
        return new ResponseWithStatus<IPagedList<ClassDto>>(resp, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> GetCurrentPersonGuid(Guid schoolGuid)
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if (!userGuid.Status) return new ResponseWithStatus<Guid, bool>(Guid.Empty, false, "Can't get current user guid");
        var personGuid = await GetPersonGuidForUser(userGuid.Response!, schoolGuid);
        return personGuid;
    }

    public async Task<ResponseWithStatus<GroupDto, bool>> GetGroupByGuid(Guid guid)
    {
        var resp = await Repository.GetGroupByGuid(guid);
        if (resp is null) return new ResponseWithStatus<GroupDto, bool>(null, false, "Group does not exist");
        return new ResponseWithStatus<GroupDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<StudentDto>>> GetInactiveStudents(Guid schoolGuid)
    {
        var students = await Repository.GetAllInactiveAccessibleStudents(schoolGuid);
        return new ResponseWithStatus<IEnumerable<StudentDto>>(students, true);
    }

    public Task<ResponseWithStatus<IEnumerable<TeacherDto>>> GetInactiveTeachers(Guid schoolGuid)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseWithStatus<InvitationDto, bool>> GetInvitationByActivationCode(string activationCode)
    {
        var invitation = await Repository.GetInvitationByActivationCode(activationCode);
        if (invitation is null) return new ResponseWithStatus<InvitationDto, bool>(invitation, false, "Invitation does not exist");
        return new ResponseWithStatus<InvitationDto, bool>(invitation, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<InvitationDto>, bool>> GetInvitations(Guid personGuid)
    {
        var resp = await Repository.GetInvitations(personGuid);
        return new ResponseWithStatus<IEnumerable<InvitationDto>, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IPagedList<InvitationDto>, bool>> GetInvitationsToSchool(Guid schoolGuid, int page)
    {
        var pager = new Pager(page);
        var invitationsResponse = await Repository.GetInvitationsToSchool(schoolGuid, pager);


        var invitationsWithPeople = await Task.WhenAll(invitationsResponse.Select(async invitation =>
        {
            if (invitation.InvitedPersonGuid.HasValue)
            {
                var personResponse = await GetPersonByGuid(invitation.InvitedPersonGuid.Value);
                if (personResponse.Status)
                    invitation.InvitedPerson = personResponse.Response;
            }
            return invitation;
        }));
        return new ResponseWithStatus<IPagedList<InvitationDto>, bool>(invitationsWithPeople.ToPagedList(invitationsResponse.Page, invitationsResponse.Total, invitationsResponse.TotalPages), true);
    }

    public async Task<ResponseWithStatus<IEnumerable<PersonDto>>> GetPeopleByUserGuid(string userGuid)
    {
        var resp = await GetSchoolsForUser(userGuid);
        if (!resp.Status) return new ResponseWithStatus<IEnumerable<PersonDto>>(resp.Message);
        var people = resp.Response!.Select(e => e.Person);
        return new ResponseWithStatus<IEnumerable<PersonDto>>(people, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<PersonDto>, bool>> GetPeopleInSchool(Guid schoolGuid)
    {
        var resp = await Repository.GetPeopleInSchool(schoolGuid);
        return new ResponseWithStatus<IEnumerable<PersonDto>, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IPagedList<PersonDto>>> GetPeopleInSchool(Guid schoolGuid, string discriminator, string query, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetPeopleInSchool(schoolGuid, discriminator, query, pager);
        return new ResponseWithStatus<IPagedList<PersonDto>>(resp, true);
    }

    public async Task<ResponseWithStatus<PersonDto, bool>> GetPersonByGuid(Guid guid)
    {
        var resp = await Repository.GetPersonByGuid(guid);
        if (resp is null) return new ResponseWithStatus<PersonDto, bool>(null, false, "Person does not exist");
        return new ResponseWithStatus<PersonDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> GetPersonGuidForUser(string userId, Guid schoolGuid)
    {
        var resp = await Repository.GetPersonGuidForUser(userId, schoolGuid);
        return new ResponseWithStatus<Guid, bool>(resp.Value, resp != Guid.Empty);
    }

    public async Task<ResponseWithStatus<SchoolDto>> GetSchool(Guid schoolGuid)
    {
        var school = await Repository.GetSchoolByGuid(schoolGuid);
        if (school is null)
            return new ResponseWithStatus<SchoolDto>(404, "School not found");
        return new ResponseWithStatus<SchoolDto>(school, true);
    }

    public async Task<ResponseWithStatus<IEnumerable<SchoolWithRelatedPersonDto>, bool>> GetSchoolsForUser(string userGuid)
    {
        var resp = await Repository.GetSchoolsForUser(userGuid);
        var schoolsWithRelatedPeople = resp.Select(school =>
        {
            return new SchoolWithRelatedPersonDto()
            {
                School = school,
                Person = GetPersonByGuid(GetCurrentPersonGuid(school.Guid).Result.Response).Result.Response ?? new()
            };
        });

        return new ResponseWithStatus<IEnumerable<SchoolWithRelatedPersonDto>, bool>(schoolsWithRelatedPeople, true);
    }

    public async Task<ResponseWithStatus<StudentDto, bool>> GetStudentByGuid(Guid guid)
    {
        var resp = await Repository.GetStudentByGuid(guid);
        if (resp is null) return new ResponseWithStatus<StudentDto, bool>(null, false, "Student does not exist");
        return new ResponseWithStatus<StudentDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IPagedList<StudentDto>>> GetStudentsInClass(Guid classGuid, int page)
    {
        var pager = new Pager(page);
        var response = await Repository.GetStudentsInClass(classGuid, pager);
        return new ResponseWithStatus<IPagedList<StudentDto>>(response, true);
    }

    public async Task<ResponseWithStatus<IPagedList<StudentDto>>> GetStudentsInSchool(Guid schoolGuid, int page)
    {
        var pager = new Pager(page);
        var response = await Repository.GetStudentsInSchool(schoolGuid, pager);
        return new ResponseWithStatus<IPagedList<StudentDto>>(response, true);
    }

    public async Task<ResponseWithStatus<TeacherDto, bool>> GetTeacherByGuid(Guid guid)
    {
        var resp = await Repository.GetTeacherByGuid(guid);
        if (resp is null) return new ResponseWithStatus<TeacherDto, bool>(null, false, "Teacher does not exist");
        return new ResponseWithStatus<TeacherDto, bool>(resp, true);
    }

    public async Task<ResponseWithStatus<IPagedList<TeacherDto>>> GetTeachersInClass(Guid classGuid, int page)
    {
        var pager = new Pager(page);
        var response = await Repository.GetTeachersInClass(classGuid, pager);
        return new ResponseWithStatus<IPagedList<TeacherDto>>(response, true);
    }

    public async Task<ResponseWithStatus<IPagedList<TeacherDto>>> GetTeachersInSchool(Guid schoolGuid, int page)
    {
        var pager = new Pager(page);
        var response = await Repository.GetTeachersInSchool(schoolGuid, pager);
        return new ResponseWithStatus<IPagedList<TeacherDto>>(response, true);
    }

    public async Task<ResponseWithStatus<bool>> IsUserActive(string userGuid)
    {
        return new ResponseWithStatus<bool>(await Repository.IsUserActive(userGuid), true);
    }

    public async Task<ResponseWithStatus<Guid>> RecogniseCurrentPersonByRelatedPerson(Guid requestedPersonGuid)
    {
        var uid = await _identityLogic.Service.CurrentUserId();
        if (!uid.Status) return new ResponseWithStatus<Guid>(uid.Message);
        var people = await GetPeopleByUserGuid(uid.Response!);
        if (!people.Status) return new ResponseWithStatus<Guid>(people.Message);
        var requestedPerson = await GetPersonByGuid(requestedPersonGuid);
        if (!requestedPerson.Status) return new ResponseWithStatus<Guid>(requestedPerson.Message);
        var schoolGuidOfRequested = requestedPerson.Response!.SchoolGuid;
        var searchedPerson = people.Response!.FirstOrDefault(e => e.SchoolGuid == schoolGuidOfRequested);
        return searchedPerson is null ? new ResponseWithStatus<Guid>("Could not find person") : new ResponseWithStatus<Guid>(searchedPerson.Guid, true);
    }
}
