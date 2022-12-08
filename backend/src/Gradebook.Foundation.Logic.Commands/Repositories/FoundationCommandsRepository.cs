using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public partial class FoundationCommandsRepository : BaseRepository<FoundationDatabaseContext>, IFoundationCommandsRepository
{
    private readonly IMapper _mapper;
    public FoundationCommandsRepository(FoundationDatabaseContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<StatusResponse<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid)
    {
        var admin = await Context.Administrators!.FirstOrDefaultAsync(e => e.Guid == administratorGuid);
        var school = await Context.Schools!.Include(e => e.People).FirstOrDefaultAsync(e => e.Guid == schoolGuid);

        if (admin is null || school is null) return new StatusResponse<bool>(false, "Admin or school not found!");

        school.People!.Add(admin);

        return new StatusResponse<bool>(true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command)
    {
        var administrator = _mapper.Map<Administrator>(command);
        administrator.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Admin;

        await Context.Administrators!.AddAsync(administrator);

        return new ResponseWithStatus<Guid, bool>(administrator.Guid, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command)
    {
        var school = _mapper.Map<School>(command);

        await Context.Schools!.AddAsync(school);

        return new ResponseWithStatus<Guid, bool>(school.Guid, true);
    }

    public async Task<ResponseWithStatus<Guid>> AddNewStudent(NewStudentCommand newStudentDto)
    {
        var student = _mapper.Map<Student>(newStudentDto);
        student.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Student;
        await Context.Students!.AddAsync(student);

        return new ResponseWithStatus<Guid>(student.Guid, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> AddNewTeacher(NewTeacherCommand newTeacherDto)
    {
        var teacher = _mapper.Map<Teacher>(newTeacherDto);
        teacher.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Teacher;
        await Context.Teachers!.AddAsync(teacher);
        return new ResponseWithStatus<Guid, bool>(teacher.Guid, true);
    }

    public async Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid)
    {
        Person? person = await GetPersonByGuid(personGuid);
        if (person is null) return new StatusResponse<bool>(false, "Person does not exist");
        School? school = await Context.Schools!.Include(e => e.People).FirstOrDefaultAsync(e => e.Guid == schoolGuid);
        if (school is null) return new StatusResponse<bool>(false, "School does not exist");
        school.People!.Add(person);
        return new StatusResponse<bool>(true);
    }

    public async Task<StatusResponse<bool>> AssignUserToStudent(string userId, Guid personGuid)
    {
        var student = await Context.Students!.FirstOrDefaultAsync(e => e.Guid == personGuid);
        if (student is null) return new StatusResponse<bool>(false, "Person does not exist");
        student.UserGuid = userId;
        return new StatusResponse<bool>(true);
    }

    public async Task<StatusResponse<bool>> AssignUserToTeacher(string userId, Guid personGuid)
    {
        var teacher = await Context.Teachers!.FirstOrDefaultAsync(e => e.Guid == personGuid);
        if (teacher is null) return new StatusResponse<bool>(false, "Person does not exist");
        teacher.UserGuid = userId;
        return new StatusResponse<bool>(true);
    }

    public async Task<StatusResponse<bool>> AssignUserToAdministrator(string userId, Guid personGuid)
    {
        var administrator = await Context.Administrators!.FirstOrDefaultAsync(e => e.Guid == personGuid);
        if (administrator is null) return new StatusResponse<bool>(false, "Person does not exist");
        administrator.UserGuid = userId;
        return new StatusResponse<bool>(true);
    }

    public async Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role, Guid schoolGuid)
    {
        SystemInvitation systemInvitation = new()
        {
            SchoolRole = role,
            CreatorGuid = invitingPersonGuid,
            InvitedPersonGuid = invitedPersonGuid,
            SchoolGuid = schoolGuid
        };
        await Context.SystemInvitations!.AddAsync(systemInvitation);
        return systemInvitation.InvitationCode;
    }

    public async Task<StatusResponse<bool>> UseInvitation(UseInvitationCommand command)
    {
        var invitation = await Context.SystemInvitations!.FirstOrDefaultAsync(e => e.Guid == command.InvitationGuid);
        if (invitation is null) return new StatusResponse<bool>(false, "Invitation does not exist");
        invitation.IsUsed = true;
        invitation.UsedDate = command.UsedDate;
        return new StatusResponse<bool>(true);
    }

    private async Task<Person?> GetPersonByGuid(Guid guid)
    {
        Person? person = (Person?)await Context.Students!.FirstOrDefaultAsync(e => e.Guid == guid) ??
            (Person?)await Context.Teachers!.FirstOrDefaultAsync(e => e.Guid == guid) ??
            (Person?)await Context.Administrators!.FirstOrDefaultAsync(e => e.Guid == guid);
        return person;
    }

    private async Task<School?> GetSchoolByGuid(Guid guid)
    {
        return await Context.Schools!.FirstOrDefaultAsync(school => school.Guid == guid);
    }

    public async Task<StatusResponse> DeleteSchool(Guid schoolGuid)
    {
        School? school = await GetSchoolByGuid(schoolGuid);
        if (school is null) return new StatusResponse(true);
        school.IsDeleted = true;
        return new StatusResponse(true);
    }

    public async Task<ResponseWithStatus<Guid>> AddNewClass(NewClassCommand command)
    {
        var dbModel = _mapper.Map<Class>(command);
        await Context.AddAsync(dbModel);
        return new ResponseWithStatus<Guid>(dbModel.Guid);
    }

    public async Task<StatusResponse> DeleteClass(Guid classGuid)
    {
        var _class = await Context.Classes!.FirstOrDefaultAsync(c => c.Guid == classGuid);
        if (_class is null) return new StatusResponse(true);
        _class.IsDeleted = true;
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeletePerson(Guid personGuid)
    {
        var person = await GetPersonByGuid(personGuid);
        if (person is null) return new StatusResponse(true);
        person.IsDeleted = true;
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> AddStudentsToClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
    {
        var students = Context.Students!.Where(student => studentsGuids.Contains(student.Guid));
        var _class = await Context.Classes!.Include(entity => entity.Students).FirstOrDefaultAsync(_class => _class.Guid == classGuid);
        if (_class is null) return new StatusResponse("Class does not exist");
        _class.Students!.AddRange(students);
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        var teachers = Context.Teachers!.Where(teacher => teachersGuids.Contains(teacher.Guid));
        var _class = await Context.Classes!.Include(entity => entity.OwnersTeachers).FirstOrDefaultAsync(_class => _class.Guid == classGuid);
        if (_class is null) return new StatusResponse("Class does not exist");
        _class.OwnersTeachers!.AddRange(teachers);
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteStudentsFromClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
    {
        var students = Context.Students!.Where(student => studentsGuids.Contains(student.Guid));
        var _class = await Context.Classes!.Include(entity => entity.Students).FirstOrDefaultAsync(_class => _class.Guid == classGuid);
        if (_class is null) return new StatusResponse("Class does not exist");
        _class.Students!.RemoveRange(students);
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        var teachers = Context.Teachers!.Where(teacher => teachersGuids.Contains(teacher.Guid));
        var _class = await Context.Classes!.Include(entity => entity.OwnersTeachers).FirstOrDefaultAsync(_class => _class.Guid == classGuid);
        if (_class is null) return new StatusResponse("Class does not exist");
        _class.OwnersTeachers!.RemoveRange(teachers);
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> SetStudentActiveClass(Guid classGuid, Guid studentGuid)
    {
        var _class = await Context.Classes!.FirstOrDefaultAsync(e => e.Guid == classGuid);
        var student = await Context.Students!.FirstOrDefaultAsync(e => e.Guid == studentGuid);
        if (_class is null || student is null) return new StatusResponse(false, _class is null ? "Class not found" : "Student not found");
        student.CurrentClassGuid = _class.Guid;
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> RemoveStudentActiveClass(Guid studentGuid)
    {
        var student = await Context.Students!.FirstOrDefaultAsync(e => e.Guid == studentGuid);
        if (student is null) return new StatusResponse(false, "Class not found");
        student.CurrentClassGuid = null;
        return new StatusResponse(true);
    }

    public async Task<ResponseWithStatus<Guid>> AddSubject(Guid schoolGuid, NewSubjectCommand command)
    {
        var school = await Context.Schools!.FirstOrDefaultAsync(s => s.Guid == schoolGuid);
        if (school is null) return new ResponseWithStatus<Guid>("School does not exist");
        var subject = _mapper.Map<Subject>(command);
        subject.School = school;
        await Context.Subjects!.AddAsync(subject);
        return new ResponseWithStatus<Guid>(subject.Guid);
    }

    public async Task<StatusResponse> AddTeachersToSubject(Guid subjectGuid, List<Guid> teachersGuids)
    {
        var subject = await Context.Subjects!.Include(sub => sub.Teachers).FirstOrDefaultAsync(sub => sub.Guid == subjectGuid);
        if (subject is null) return new StatusResponse("Subject not found");
        var teachers = Context.Teachers!.Where(tea => teachersGuids.Contains(tea.Guid));
        subject.Teachers!.AddRange(teachers);
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> RemoveTeachersFromSubject(Guid subjectGuid, List<Guid> teachersGuids)
    {
        var subject = await Context.Subjects!.Include(sub => sub.Teachers).FirstOrDefaultAsync(sub => sub.Guid == subjectGuid);
        if (subject is null) return new StatusResponse("Subject not found");
        var teachers = Context.Teachers!.Where(tea => teachersGuids.Contains(tea.Guid));
        subject.Teachers!.RemoveRange(teachers);
        return new StatusResponse(true);
    }

    public async Task<ResponseWithStatus<Guid>> AddNewEducationCycle(EducationCycleCommand command)
    {
        EducationCycle educationCycle = new()
        {
            Name = command.Name,
            SchoolGuid = command.SchoolGuid,
            CreatedDate = command.CreatedDate,
            CreatorGuid = command.CreatorGuid,
            EducationCycleSteps = command.Stages.Select((step, index) => new EducationCycleStep()
            {
                Name = step.Name,
                Order = index,
                EducationCycleStepSubjects = step.Subjects.Select(subject => new EducationCycleStepSubject()
                {
                    SubjectGuid = subject.SubjectGuid,
                    HoursInStep = subject.HoursNo,
                    IsMandatory = subject.IsMandatory,
                    GroupsAllowed = subject.CanUseGroups,
                }).ToList()
            }).ToList()
        };
        await Context.AddAsync(educationCycle);
        return new ResponseWithStatus<Guid>(educationCycle.Guid);
    }
}

