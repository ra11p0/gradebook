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

    public async Task<ResponseWithStatus<bool>> ActivatePerson(string activationCode)
    {
        var userGuid = await _identityLogic.Service.CurrentUserId();
        if(!userGuid.Status) return new ResponseWithStatus<bool>(false, userGuid.Message);

        var person = await _foundationQueries.Service.GetCurrentPersonGuid();
        if(person.Status) return new ResponseWithStatus<bool>(false, "Person already exists");

        var invitationResult = await _foundationQueries.Service.GetInvitationByActivationCode(activationCode);
        if(!invitationResult.Status) return new ResponseWithStatus<bool>(false, invitationResult.Message);

        var invitation = invitationResult.Response!;
        if(invitation.IsUsed) return new ResponseWithStatus<bool>(false, "Invitation already used");
        if(invitation.ExprationDate < DateTime.Now) return new ResponseWithStatus<bool>(false, "Invitation expired");

        var useInvitationResult = await Repository.UseInvitation(new UseInvitationCommand(){
            InvitationGuid = invitation.Guid,
            UsedDate = DateTime.Now,
            UserGuid = userGuid.Response!
        });
        if(!useInvitationResult.Status) return new ResponseWithStatus<bool>(false, useInvitationResult.Message);
        if(!invitation.InvitedPersonGuid.HasValue) return new ResponseWithStatus<bool>(false, "Invitation is not binded to any person. Functionality is not yet avalible");

        ResponseWithStatus<bool> assigningResult;
        switch (invitation.SchoolRole){
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
            return new ResponseWithStatus<bool>(false, "Wrong role");
        }
        if(!assigningResult.Status) return new ResponseWithStatus<bool>(false, assigningResult.Message);
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<bool>(true);
    }

    public async Task<ResponseWithStatus<bool>> AddNewStudent(NewStudentCommand command)
    {
        command.CreatorGuid = (await _foundationQueries.Service.GetCurrentPersonGuid()).Response;
        var resp = await Repository.AddNewStudent(command);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<ResponseWithStatus<bool>> AddNewTeacher(NewTeacherCommand command)
    {
        command.CreatorGuid = (await _foundationQueries.Service.GetCurrentPersonGuid()).Response;
        var resp = await Repository.AddNewTeacher(command);
        await Repository.SaveChangesAsync();
        return resp;
    }

    public async Task<ResponseWithStatus<bool>> AddPersonToSchool(Guid schoolGuid, Guid? personGuid = null)
    {
        if(personGuid is null){
            var person = await _foundationQueries.Service.GetCurrentPersonGuid();
            if(!person.Status) return new ResponseWithStatus<bool>(false, "Person does not exist");
            personGuid = person.Response;
        }
        var resp = await Repository.AddPersonToSchool(schoolGuid, personGuid.Value);
        if(resp.Status) await Repository.SaveChangesAsync();
        return new ResponseWithStatus<bool>(resp.Status, resp.Message);
    }

    public async Task<ResponseWithStatus<string, bool>> GenerateSystemInvitation(Guid personGuid, SchoolRoleEnum role)
    {
        var currentPersonGuid = await _foundationQueries.Service.GetCurrentPersonGuid();
        if(!currentPersonGuid.Status) return new ResponseWithStatus<string, bool>(default, false, "Could not find current person");
        var response = await Repository.GenerateSystemInvitation(personGuid, currentPersonGuid.Response, role);
        await Repository.SaveChangesAsync();
        return new ResponseWithStatus<string, bool>(response, !(response is null));
    }

    public async Task<ResponseWithStatus<bool>> NewAdministrator(NewAdministratorCommand command)
    {
        if(command.UserGuid is null){
            var currentUserId = await _identityLogic.Service.CurrentUserId();
            if(!currentUserId.Status) return new ResponseWithStatus<bool>(false);
            command.UserGuid = currentUserId.Response;
        }

        await _identityLogic.Service.AddUserRole(UserRoles.SuperAdmin);

        var resp = await Repository.AddNewAdministrator(command);
        if(resp.Status){
            await Repository.SaveChangesAsync();
            return new ResponseWithStatus<bool>(true);
        }
        return new ResponseWithStatus<bool>(false);
    }

    public async Task<ResponseWithStatus<bool>> NewAdministratorWithSchool(NewAdministratorCommand administratorCommand, NewSchoolCommand schoolCommand)
    {
        if(administratorCommand.UserGuid is null){
            var currentUserId = await _identityLogic.Service.CurrentUserId();
            if(!currentUserId.Status) return new ResponseWithStatus<bool>(false);
            administratorCommand.UserGuid = currentUserId.Response;
        }

        await _identityLogic.Service.AddUserRole(UserRoles.SuperAdmin);

        var respAdmin = await Repository.AddNewAdministrator(administratorCommand);
        var respSchool = await Repository.AddNewSchool(schoolCommand);
        if(respAdmin.Status && respSchool.Status)
            await Repository.SaveChangesAsync();
        var respAddAdminToSchool = await Repository.AddAdministratorToSchool(respAdmin.Response, respSchool.Response);
        
        if(respAddAdminToSchool.Status){
            await Repository.SaveChangesAsync();
            return new ResponseWithStatus<bool>(true);
        }
        return new ResponseWithStatus<bool>(false);
    }
}
