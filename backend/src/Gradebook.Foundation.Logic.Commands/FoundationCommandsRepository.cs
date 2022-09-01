using System.Linq;
using AutoMapper;
using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Database;
using Gradebook.Foundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommandsRepository : BaseRepository<FoundationDatabaseContext>, IFoundationCommandsRepository
{
    private readonly IMapper _mapper;
    public FoundationCommandsRepository(FoundationDatabaseContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<ResponseWithStatus<bool>> AddAdministratorToSchool(Guid administratorGuid, Guid schoolGuid)
    {
        var admin = Context.Administrators.FirstOrDefault(e=>e.Guid == administratorGuid);
        var school = Context.Schools.Include(e=>e.People).FirstOrDefault(e=>e.Guid == schoolGuid);

        if(admin is null || school is null) return new ResponseWithStatus<bool>(false, "Admin or school not found!");

        school.People.Add(admin);

        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> AddNewAdministrator(NewAdministratorCommand command)
    {
        var administrator = _mapper.Map<Administrator>(command);
        administrator.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Admin;

        await Context.Administrators.AddAsync(administrator);

        return new ResponseWithStatus<Guid, bool>(administrator.Guid, true);
    }

    public async Task<ResponseWithStatus<Guid, bool>> AddNewSchool(NewSchoolCommand command)
    {
        var school = _mapper.Map<School>(command);
        
        await Context.Schools.AddAsync(school);

        return new ResponseWithStatus<Guid, bool>(school.Guid, true);
    }

    public async Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand newStudentDto)
    {
        var student = _mapper.Map<Student>(newStudentDto);
        student.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Student;
        await Context.Students.AddAsync(student);

        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<bool>> AddNewTeacher(NewTeacherCommand newTeacherDto)
    {
        var teacher = _mapper.Map<Teacher>(newTeacherDto);
        teacher.SchoolRole = Common.Foundation.Enums.SchoolRoleEnum.Teacher;
        await Context.Teachers.AddAsync(teacher);

        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<bool>> AddPersonToSchool(Guid schoolGuid, Guid personGuid)
    {
        Person? person = await GetPersonByGuid(personGuid);
        if(person is null) return new ResponseWithStatus<bool>(false, "Person does not exist");
        School? school = await Context.Schools.Include(e=>e.People).FirstOrDefaultAsync(e=>e.Guid == schoolGuid);
        if(school is null) return new ResponseWithStatus<bool>(false, "School does not exist");
        school.People.Add(person);
        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<bool>> AssignUserToStudent(string userId, Guid personGuid)
    {
        var student = await Context.Students.FirstOrDefaultAsync(e=>e.Guid == personGuid);
        if(student is null) return new ResponseWithStatus<bool>(false, "Person does not exist");
        student.UserGuid = userId;
        return new ResponseWithStatus<bool>(true);
    }
    public async Task<ResponseWithStatus<bool>> AssignUserToTeacher(string userId, Guid personGuid)
    {
        var teacher = await Context.Teachers.FirstOrDefaultAsync(e=>e.Guid == personGuid);
        if(teacher is null) return new ResponseWithStatus<bool>(false, "Person does not exist");
        teacher.UserGuid = userId;
        return new ResponseWithStatus<bool>(true);
    }
    public async Task<ResponseWithStatus<bool>> AssignUserToAdministrator(string userId, Guid personGuid)
    {
        var administrator = await Context.Administrators.FirstOrDefaultAsync(e=>e.Guid == personGuid);
        if(administrator is null) return new ResponseWithStatus<bool>(false, "Person does not exist");
        administrator.UserGuid = userId;
        return new ResponseWithStatus<bool>(true);
    }

    public async Task<string?> GenerateSystemInvitation(Guid invitedPersonGuid, Guid invitingPersonGuid, SchoolRoleEnum role)
    {
        SystemInvitation systemInvitation = new(){
            SchoolRole = role,
            CreatorGuid = invitingPersonGuid,
            InvitedPersonGuid = invitedPersonGuid
        };
        Context.SystemInvitations.Add(systemInvitation);
        return systemInvitation.InvitationCode;
    }

    public async Task<ResponseWithStatus<bool>> UseInvitation(UseInvitationCommand command)
    {
        var invitation = await Context.SystemInvitations.FirstOrDefaultAsync(e=>e.Guid == command.InvitationGuid);
        if(invitation is null) return new ResponseWithStatus<bool>(false, "Invitation does not exist");
        invitation.IsUsed = true;
        invitation.UsedDate = command.UsedDate;
        return new ResponseWithStatus<bool>(true);
    }

    private async Task<Person?> GetPersonByGuid(Guid guid){
        Person? person = (Person?) await Context.Students.FirstOrDefaultAsync(e=>e.Guid == guid) ??
            (Person?) await Context.Teachers.FirstOrDefaultAsync(e=>e.Guid == guid) ??
            (Person?) await Context.Administrators.FirstOrDefaultAsync(e=>e.Guid == guid);
        return person;
    }
}
