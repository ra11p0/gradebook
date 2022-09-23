using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Common.Foundation.Enums;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Identity.Logic.Interfaces;
using Gradebook.Foundation.Identity.Models;

namespace Gradebook.Foundation.Logic.Commands;

public class FoundationCommands : BaseLogic<IFoundationCommandsRepository>, IFoundationCommands
{
    private readonly ServiceResolver<IIdentityLogic> _identityLogic;
    private readonly ServiceResolver<IFoundationQueries> _foundationQueries;
    public FoundationCommands(IFoundationCommandsRepository repository,
        IServiceProvider serviceProvider) : base(repository)
    {
        _identityLogic = serviceProvider.GetResolver<IIdentityLogic>();
        _foundationQueries = serviceProvider.GetResolver<IFoundationQueries>();
    }

    public async Task<StatusResponse<bool>> ActivatePerson(string activationCode)
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if (!userGuid.Status) return new StatusResponse<bool>(false, userGuid.Message);

        var invitationResult = await _foundationQueries.Service.GetInvitationByActivationCode(activationCode);
        if (!invitationResult.Status) return new StatusResponse<bool>(false, invitationResult.Message);

        var invitation = invitationResult.Response!;
        if (invitation.IsUsed) return new StatusResponse<bool>(false, "Invitation already used");
        if (invitation.ExprationDate < DateTime.Now) return new StatusResponse<bool>(false, "Invitation expired");

        var useInvitationResult = await Repository.UseInvitation(new UseInvitationCommand()
        {
            InvitationGuid = invitation.Guid,
            UsedDate = DateTime.Now,
            UserGuid = userGuid.Response!
        });
        if (!useInvitationResult.Status) return new StatusResponse<bool>(false, useInvitationResult.Message);
        if (!invitation.InvitedPersonGuid.HasValue) return new StatusResponse<bool>(false, "Invitation is not binded to any person. Functionality is not yet avalible");

        StatusResponse<bool> assigningResult;
        string userRoleToAdd;
        switch (invitation.SchoolRole)
        {
            case SchoolRoleEnum.Student:
                assigningResult = await Repository.AssignUserToStudent(userGuid.Response!, invitation.InvitedPersonGuid.Value);
                userRoleToAdd = UserRoles.Student;
                break;
            case SchoolRoleEnum.Teacher:
                assigningResult = await Repository.AssignUserToTeacher(userGuid.Response!, invitation.InvitedPersonGuid.Value);
                userRoleToAdd = UserRoles.Teacher;
                break;
            case SchoolRoleEnum.Admin:
                assigningResult = await Repository.AssignUserToAdministrator(userGuid.Response!, invitation.InvitedPersonGuid.Value);
                userRoleToAdd = UserRoles.SuperAdmin;
                break;
            default:
                return new StatusResponse<bool>(false, "Wrong role");
        }
        if (!assigningResult.Status) return new StatusResponse<bool>(false, assigningResult.Message);
        var addRoleResponse = await _identityLogic.Service.AddUserRole(userRoleToAdd);
        if (!addRoleResponse.Status) return new StatusResponse<bool>(false, addRoleResponse.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse<bool>(true);
    }

    public async Task<StatusResponse> AddNewClass(NewClassCommand command)
    {
        command.CreatedDate = DateTime.Now;
        var resp = await Repository.AddNewClass(command);
        if (!resp.Status) return new StatusResponse(false, resp.Message);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<StatusResponse> AddNewSchool(NewSchoolCommand newSchoolCommand)
    {
        throw new Exception("set new person before activating new school");
        /*Repository.BeginTransaction();
        var currentPerson = await _foundationQueries.Service.GetCurrentPersonGuid();
        if (!currentPerson.Status) return new StatusResponse(currentPerson.Message);
        var respSchool = await Repository.AddNewSchool(newSchoolCommand);
        if (!respSchool.Status) return new StatusResponse(respSchool.Message);
        await Repository.SaveChangesAsync();
        var respAddAdminToSchool = await Repository.AddAdministratorToSchool(currentPerson.Response, respSchool.Response);
        if (!respAddAdminToSchool.Status) return new StatusResponse(respAddAdminToSchool.Message);
        await Repository.SaveChangesAsync();
        Repository.CommitTransaction();
        return new StatusResponse(true);*/

    }

    public async Task<StatusResponse<bool>> AddNewStudent(NewStudentCommand command, Guid schoolGuid)
    {
        Repository.BeginTransaction();
        command.CreatorGuid = (await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid)).Response;
        var resp = await Repository.AddNewStudent(command);
        if (resp.Status is not true) return new StatusResponse<bool>(false);
        await Repository.SaveChangesAsync();
        var addToSchoolResponse = await AddPersonToSchool(schoolGuid, resp.Response);
        if (addToSchoolResponse.Status is not true) return new StatusResponse<bool>(addToSchoolResponse.Message);
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
        var resp = await Repository.AddStudentsToClass(classGuid, studentsGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> AddTeachersToClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        var resp = await Repository.AddTeachersToClass(classGuid, teachersGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteClass(Guid classGuid)
    {
        var resp = await Repository.DeleteClass(classGuid);
        if (!resp.Status) return new StatusResponse(false, resp.Message);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<StatusResponse> DeletePerson(Guid personGuid)
    {
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
        var resp = await Repository.DeleteStudentsFromClass(classGuid, studentsGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<StatusResponse> DeleteTeachersFromClass(Guid classGuid, IEnumerable<Guid> teachersGuids)
    {
        var resp = await Repository.DeleteTeachersFromClass(classGuid, teachersGuids);
        if (!resp.Status) return new StatusResponse(resp.Message);
        await Repository.SaveChangesAsync();
        return new StatusResponse(true);
    }

    public async Task<ResponseWithStatus<string[], bool>> GenerateMultipleSystemInvitation(Guid[] peopleGuid, SchoolRoleEnum role, Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return new ResponseWithStatus<string[], bool>(default, false, "Could not find current person");

        var response = await Task.WhenAll(peopleGuid.Select(async personGuid => await Repository.GenerateSystemInvitation(personGuid, currentPersonGuid.Response, role, schoolGuid)));
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<string[], bool>(response!, response is not null);
    }

    public async Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role, Guid schoolGuid)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid(schoolGuid);
        if (!currentPersonGuid.Status) return new ResponseWithStatus<string, bool>(default, false, "Could not find current person");
        var response = await Repository.GenerateSystemInvitation(personGuid, currentPersonGuid.Response, role, schoolGuid);
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
}
