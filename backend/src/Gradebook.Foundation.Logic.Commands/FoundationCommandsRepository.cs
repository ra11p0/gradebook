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
}
