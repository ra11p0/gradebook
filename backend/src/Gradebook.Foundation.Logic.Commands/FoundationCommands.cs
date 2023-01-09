using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Identity.Models;
using Gradebook.Foundation.Logic.Commands.Repositories;

namespace Gradebook.Foundation.Logic.Commands;

public partial class FoundationCommands : BaseLogic<IFoundationCommandsRepository>, IFoundationCommands
{
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    private readonly ServiceResolver<IFoundationPermissionsLogic> _foundationPermissions;
    public FoundationCommands(IFoundationCommandsRepository repository,
        IServiceProvider serviceProvider) : base(repository)
    {
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
        _foundationPermissions = serviceProvider.GetResolver<IFoundationPermissionsLogic>();
    }

    public async Task<StatusResponse<bool>> ActivatePerson(string activationCode)
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if (!userGuid.Status) return new StatusResponse<bool>(false, userGuid.Message);

        var invitationResult = await _foundationQueries.Service.GetInvitationByActivationCode(activationCode);
        if (!invitationResult.Status) return new StatusResponse<bool>(false, invitationResult.Message);

        var invitation = invitationResult.Response!;
        if (invitation.IsUsed) return new StatusResponse<bool>("Invitation already used");
        if (invitation.ExprationDate < Time.UtcNow) return new StatusResponse<bool>("Invitation expired");

        var person = invitation.InvitedPerson;
        if (!string.IsNullOrEmpty(person!.UserId))
            return new StatusResponse<bool>(418, false, "Person already has bound account");

        var useInvitationResult = await Repository.UseInvitation(new UseInvitationCommand()
        {
            InvitationGuid = invitation.Guid,
            UsedDate = Time.UtcNow,
            UserGuid = userGuid.Response!
        });
        if (!useInvitationResult.Status) return new StatusResponse<bool>(useInvitationResult.Message);
        if (!invitation.InvitedPersonGuid.HasValue) return new StatusResponse<bool>("Invitation not bound to any person.");

        StatusResponse<bool> assigningResult;
        switch (invitation.SchoolRole)
        {
            case SchoolRoleEnum.Student:
                assigningResult = await Repository.AssignUserToStudent(userGuid.Response!, invitation.InvitedPersonGuid.Value);

                break;
            case SchoolRoleEnum.Teacher:
                assigningResult = await Repository.AssignUserToTeacher(userGuid.Response!, invitation.InvitedPersonGuid.Value);

                break;
            case SchoolRoleEnum.Admin:
                assigningResult = await Repository.AssignUserToAdministrator(userGuid.Response!, invitation.InvitedPersonGuid.Value);

                break;
            default:
                return new StatusResponse<bool>("Wrong role");
        }
        if (!assigningResult.Status) return new StatusResponse<bool>(false, assigningResult.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse<bool>(true);
    }

    public async Task<ResponseWithStatus<Guid>> AddNewClass(NewClassCommand command)
    {
        var currentPerson = await _foundationQueries.Service.GetCurrentPersonGuid(command.SchoolGuid);
        if (!await _foundationPermissions.Service.CanCreateNewClass(currentPerson.Response))
            return new ResponseWithStatus<Guid>(403);
        command.CreatedDate = Time.UtcNow;
        var resp = await Repository.AddNewClass(command);
        if (!resp.Status) return new ResponseWithStatus<Guid>(false, resp.Message);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<ResponseWithStatus<Guid>> AddNewStudent(NewStudentCommand command, Guid schoolGuid)
    {
        var person = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!person.Status) return new ResponseWithStatus<Guid>(404, "Cannot recognise person");
        if (!await _foundationPermissions.Service.CanCreateNewStudents(person.Response)) return new ResponseWithStatus<Guid>(403);
        Repository.BeginTransaction();
        command.CreatorGuid = (await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid)).Response;
        var resp = await Repository.AddNewStudent(command);
        if (resp.Status is not true) return new ResponseWithStatus<Guid>(false);
        await Repository.SaveChangesAsync();
        var addToSchoolResponse = await AddPersonToSchool(schoolGuid, resp.Response);
        if (addToSchoolResponse.Status is not true) return new ResponseWithStatus<Guid>(addToSchoolResponse.Message);
        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return resp;
    }

    public async Task<StatusResponse<bool>> AddNewTeacher(NewTeacherCommand command, Guid schoolGuid)
    {
        Repository.BeginTransaction();
        command.CreatorGuid = (await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid)).Response;
        var resp = await Repository.AddNewTeacher(command);
        if (!resp.Status) return new StatusResponse<bool>(false, resp.Message);
        await Repository.SaveChangesAsync();
        var addToSchoolResponse = await AddPersonToSchool(schoolGuid, resp.Response);
        if (!addToSchoolResponse.Status) return new StatusResponse<bool>(addToSchoolResponse.Message);
        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return resp;
    }

    public async Task<StatusResponse<bool>> AddPersonToSchool(Guid schoolGuid, Guid? personGuid = null)
    {
        if (personGuid is null)
        {
            var person = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
            if (!person.Status) return new StatusResponse<bool>(false, "Person does not exist");
            personGuid = person.Response;
        }
        var resp = await Repository.AddPersonToSchool(schoolGuid, personGuid.Value);
        if (resp.Status) await Repository.SaveChangesAsync();
        return new StatusResponse<bool>(resp.Status, resp.Message);
    }

    public async Task<StatusResponse> AddStudentsToClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
    {
        if (!studentsGuids.Any()) return new StatusResponse(true, "No change");
        foreach (var student in studentsGuids)
        {
            var isStudentAlreadyInAnyClass = await _foundationQueries.Service.IsStudentInAnyClass(student);
            if (!isStudentAlreadyInAnyClass.Status) return new StatusResponse("Cant check if students are able to assign to class");
            if (isStudentAlreadyInAnyClass.Response) return new StatusResponse(false, "At least one student is already assigned to class");
        }
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(studentsGuids.First());
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new StatusResponse(statusCode: 403, message: "Forbidden");
        var resp = await Repository.AddStudentsToClass(classGuid, studentsGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);

        if (!(await SetStudentsActiveClass(classGuid, studentsGuids.ToList())).Status)
            return new StatusResponse(false, "Could not set default class");
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        if (!teachersGuids.Any()) return new StatusResponse(true, "No change");
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(teachersGuids.First());
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new StatusResponse(403);
        var resp = await Repository.AddTeachersToClass(classGuid, teachersGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeletePerson(Guid personGuid)
    {
        var personToDelete = await _foundationQueries.Service.GetPersonByGuid(personGuid);
        if (!personToDelete.Status) return new StatusResponse(personToDelete.Message);

        var currentPerson = await _foundationQueries.Service.GetCurrentPersonGuid(personToDelete.Response!.SchoolGuid!.Value);
        if (!currentPerson.Status) return new StatusResponse(currentPerson.Message);

        if (personToDelete.Response!.SchoolRole.HasFlag(SchoolRoleEnum.Student))
        {
            if (!await _foundationPermissions.Service.CanDeleteStudents(currentPerson.Response))
                return new StatusResponse(403);
        }

        var resp = await Repository.DeletePerson(personGuid);
        if (!resp.Status) return new StatusResponse(false);

        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteSchool(Guid schoolGuid)
    {
        var resp = await Repository.DeleteSchool(schoolGuid);
        if (!resp.Status) return new StatusResponse(false, resp.Message);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<StatusResponse> DeleteStudentsFromClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
    {
        if (!studentsGuids.Any()) return new StatusResponse(true, "No change");
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(studentsGuids.First());
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new StatusResponse(403);
        var resp = await Repository.DeleteStudentsFromClass(classGuid, studentsGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        if (!(await RemoveStudentsActiveClass(studentsGuids.ToList())).Status)
            return new StatusResponse(false, "Could not remove default class");
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        if (!teachersGuids.Any()) return new StatusResponse(true, "No change");
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByRelatedPerson(teachersGuids.First());
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new StatusResponse(403);
        var resp = await Repository.DeleteTeachersFromClass(classGuid, teachersGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<ResponseWithStatus<IPagedList<StudentDto>>> EditStudentsInClass(Guid classGuid, IEnumerable<Guid> studentsGuids)
    {
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByClassGuid(classGuid);
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new ResponseWithStatus<IPagedList<StudentDto>>(403);
        Repository.BeginTransaction();
        var currentStudentsInClass = await _foundationQueries.Service.GetAllStudentsInClass(classGuid);
        if (!currentStudentsInClass.Status) return new ResponseWithStatus<IPagedList<StudentDto>>(currentStudentsInClass.Message);
        var currentStudentsInClassGuids = currentStudentsInClass.Response!.Select(s => s.Guid);
        var studentsToRemove = currentStudentsInClassGuids.Where(s => !studentsGuids.Contains(s));
        var studentsToAdd = studentsGuids.Where(s => !currentStudentsInClassGuids.Contains(s));
        var addResp = await AddStudentsToClass(classGuid, studentsToAdd);
        var removeResp = await DeleteStudentsFromClass(classGuid, studentsToRemove);
        if (addResp.Status && removeResp.Status)
        {
            await Repository.SaveChangesAsync();
            Repository.CommitTransaction();
            var resp = await _foundationQueries.Service.GetStudentsInClass(classGuid, 0);
            if (!resp.Status) return new ResponseWithStatus<IPagedList<StudentDto>>(new PagedList<StudentDto>(), true, "Could not load students");
            return new ResponseWithStatus<IPagedList<StudentDto>>(resp.Response);
        }
        else
        {
            Repository.RollbackTransaction();
            return new ResponseWithStatus<IPagedList<StudentDto>>($"{addResp.Message}; {removeResp.Message}");
        }
    }

    public async Task<ResponseWithStatus<IPagedList<TeacherDto>>> EditTeachersInClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        var currentPerson = await _foundationQueries.Service.RecogniseCurrentPersonByClassGuid(classGuid);
        if (!await _foundationPermissions.Service.CanManageClass(classGuid, currentPerson.Response))
            return new ResponseWithStatus<IPagedList<TeacherDto>>(message: "Forbidden", statusCode: 403);
        Repository.BeginTransaction();
        var currentTeachersInClass = await _foundationQueries.Service.GetAllTeachersInClass(classGuid);
        if (!currentTeachersInClass.Status) return new ResponseWithStatus<IPagedList<TeacherDto>>(currentTeachersInClass.Message);
        var currentTeachersInClassGuids = currentTeachersInClass.Response!.Select(s => s.Guid);
        var teachersToRemove = currentTeachersInClassGuids.Where(s => !teachersGuids.Contains(s));
        var teachersToAdd = teachersGuids.Where(s => !currentTeachersInClassGuids.Contains(s));
        var addResp = await AddTeachersToClass(classGuid, teachersToAdd);
        var removeResp = await DeleteTeachersFromClass(classGuid, teachersToRemove);
        if (addResp.Status && removeResp.Status)
        {
            await Repository.SaveChangesAsync();
            Repository.CommitTransaction();
            var teachers = await _foundationQueries.Service.GetTeachersInClass(classGuid, 0);
            if (!teachers.Status) return new ResponseWithStatus<IPagedList<TeacherDto>>(new PagedList<TeacherDto>(), true, "Could not load teachers");
            return new ResponseWithStatus<IPagedList<TeacherDto>>(teachers.Response);
        }
        else
        {
            Repository.RollbackTransaction();
            return new ResponseWithStatus<IPagedList<TeacherDto>>($"{addResp.Message}; {removeResp.Message}");
        }
    }

    public async Task<ResponseWithStatus<string[], bool>> GenerateMultipleSystemInvitation(Guid[] peopleGuid, Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return new ResponseWithStatus<string[], bool>(currentPersonGuid.Message);
        if (!await _foundationPermissions.Service.CanInviteToSchool(currentPersonGuid.Response!)) return new ResponseWithStatus<string[], bool>(403);
        var response = await Task.WhenAll(peopleGuid.Select(async personGuid => await Repository.GenerateSystemInvitation(personGuid, currentPersonGuid.Response, schoolGuid)));
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<string[], bool>(response!, response is not null);
    }

    public async Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return new ResponseWithStatus<string, bool>(currentPersonGuid.Message);
        if (!await _foundationPermissions.Service.CanInviteToSchool(currentPersonGuid.Response!)) return new ResponseWithStatus<string, bool>(403);
        var response = await Repository.GenerateSystemInvitation(personGuid, currentPersonGuid.Response, schoolGuid);
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<string, bool>(response, response is not null);
    }

    public async Task<StatusResponse<bool>> NewAdministrator(NewAdministratorCommand command)
    {
        if (command.UserGuid is null)
        {
            var currentUserId = await _identityLogic.Service.CurrentUserId();
            if (!currentUserId.Status) return new StatusResponse<bool>(false);
            command.UserGuid = currentUserId.Response;
        }

        await _identityLogic.Service.AddUserRole(UserRoles.SuperAdmin);

        var resp = await Repository.AddNewAdministrator(command);
        if (resp.Status)
        {
            await Repository.SaveChangesAsync();
            return new StatusResponse<bool>(true);
        }
        return new StatusResponse<bool>(false);
    }

    public async Task<StatusResponse<bool>> NewAdministratorWithSchool(NewAdministratorCommand administratorCommand, NewSchoolCommand schoolCommand)
    {
        Repository.BeginTransaction();
        if (administratorCommand.UserGuid is null)
        {
            var currentUserId = await _identityLogic.Service.CurrentUserId();
            if (!currentUserId.Status) return new StatusResponse<bool>(false);
            administratorCommand.UserGuid = currentUserId.Response;
        }

        await _identityLogic.Service.AddUserRole(UserRoles.SuperAdmin);

        var respAdmin = await Repository.AddNewAdministrator(administratorCommand);
        var respSchool = await Repository.AddNewSchool(schoolCommand);
        if (respAdmin.Status && respSchool.Status)
            await Repository.SaveChangesAsync();
        var respAddAdminToSchool = await Repository.AddAdministratorToSchool(respAdmin.Response, respSchool.Response);

        if (respAddAdminToSchool.Status)
        {
            await Repository.SaveChangesAsync();
            Repository.CommitTransaction();
            return new StatusResponse<bool>(true);
        }
        return new StatusResponse<bool>(false);
    }

    public async Task<ResponseWithStatus<Guid>> AddSubject(Guid schoolGuid, NewSubjectCommand command)
    {
        if (!command.IsValid) return new ResponseWithStatus<Guid>("Validation error");
        var person = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!person.Status) return new ResponseWithStatus<Guid>(person.Message);
        if (!await _foundationPermissions.Service.CanCreateNewSubject(person.Response))
            return new ResponseWithStatus<Guid>(403);
        var resp = await Repository.AddSubject(schoolGuid, command);
        if (!resp.Status) return new ResponseWithStatus<Guid>(resp.Message);
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<Guid>(resp.Response);
    }

    public async Task<StatusResponse> EditTeachersInSubject(Guid subjectGuid, List<Guid> teachersGuids)
    {
        var currentPerson = await _foundationQueries.Service.GetCurrentPersonGuidBySubjectGuid(subjectGuid);
        if (!currentPerson.Status) return new StatusResponse(currentPerson.Message);
        if (!await _foundationPermissions.Service.CanManageSubject(subjectGuid, currentPerson.Response))
            return new StatusResponse(403);
        Repository.BeginTransaction();
        var currentTeachersInSubject = await _foundationQueries.Service.GetTeachersForSubject(subjectGuid, 0);
        if (!currentTeachersInSubject.Status) return new StatusResponse(currentTeachersInSubject.Message);
        var currentTeachersInSubjectGuids = currentTeachersInSubject.Response!.Select(s => s.Guid);
        var teachersToRemove = currentTeachersInSubjectGuids.Where(s => !teachersGuids.Contains(s));
        var teachersToAdd = teachersGuids.Where(s => !currentTeachersInSubjectGuids.Contains(s));
        var addResp = await Repository.AddTeachersToSubject(subjectGuid, teachersToAdd.ToList());
        var removeResp = await Repository.RemoveTeachersFromSubject(subjectGuid, teachersToRemove.ToList());
        if (addResp.Status && removeResp.Status)
        {
            await Repository.SaveChangesAsync();
            Repository.CommitTransaction();
            return new StatusResponse(true);
        }
        else
        {
            Repository.RollbackTransaction();
            return new StatusResponse($"{addResp.Message}; {removeResp.Message}");
        }
    }

    public Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role, Guid schoolGuid)
    {
        throw new NotImplementedException();
    }
}
