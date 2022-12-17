using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Extensions;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public partial class FoundationCommandsRepository : IFoundationCommandsClassesRepository
{
    public async Task<StatusResponse> SetActiveEducationCycleToClasses(IEnumerable<Guid> classesGuids, Guid educationCycleGuid)
    {
        var educationCycle = await Context.EducationCycles!.FirstOrDefaultAsync(e => e.Guid == educationCycleGuid);
        if (educationCycle is null) return new StatusResponse(404);
        foreach (var classGuid in classesGuids)
        {
            var _class = await Context.Classes!.FirstOrDefaultAsync(e => e.Guid == classGuid);
            if (_class is null) continue;
            _class.ActiveEducationCycle = educationCycle;
        }
        return new StatusResponse(200);
    }
    public async Task<StatusResponse> DeleteActiveEducationCycleFromClasses(IEnumerable<Guid> classesGuids)
    {
        foreach (var classGuid in classesGuids)
        {
            var _class = await Context.Classes!.Include(e => e.EducationCycleInstances).FirstOrDefaultAsync(e => e.Guid == classGuid);
            if (_class is null) continue;
            _class.ActiveEducationCycleGuid = null;
            var instancesToDeleteGuids = _class.EducationCycleInstances!.Select(e => e.Guid);
            foreach (var instanceToDeleteGuid in instancesToDeleteGuids)
            {
                var instance = await Context.EducationCycleInstances!.Include(e => e.EducationCycleStepInstances).FirstOrDefaultAsync(w => w.Guid == instanceToDeleteGuid);
                if (instance is null) return new StatusResponse(404, $"Education cycle instance not found. Guid: {instanceToDeleteGuid}");
                instance.IsDeleted = true;
                var stepInstancesGuids = instance.EducationCycleStepInstances!.Select(e => e.Guid);
                foreach (var stepInstanceGuid in stepInstancesGuids)
                {
                    var stepInstance = await Context.EducationCycleStepInstances!.Include(e => e.EducationCycleStepSubjectInstances).FirstOrDefaultAsync(e => e.Guid == stepInstanceGuid);
                    if (stepInstance is null) return new StatusResponse(404, $"Education cycle step instance not found. Guid: {stepInstanceGuid}");
                    stepInstance!.IsDeleted = true;
                    var stepSubjectsInstancesGuids = stepInstance.EducationCycleStepSubjectInstances!.Select(e => e.Guid);
                    foreach (var subjectInstanceGuid in stepSubjectsInstancesGuids)
                    {
                        var stepSubjectInstance = await Context.EducationCycleStepSubjectInstances!.FirstOrDefaultAsync(e => e.Guid == subjectInstanceGuid);
                        if (stepSubjectInstance is null) return new StatusResponse(404, $"Education cycle step subject instance not found. Guid: {subjectInstanceGuid}");
                        stepSubjectInstance!.IsDeleted = true;
                    }
                }
            }


        }
        return new StatusResponse(200);
    }
    public async Task<ResponseWithStatus<Guid>> ConfigureEducationCycleForClass(Guid classGuid, Guid creatorGuid, EducationCycleConfigurationCommand configuration)
    {
        var educationCycle = await Context.EducationCycles!.FirstOrDefaultAsync(e => e.Guid == configuration.EducationCycleGuid);
        if (educationCycle is null) return new ResponseWithStatus<Guid>(404);
        EducationCycleInstance instance = new()
        {
            CreatorGuid = creatorGuid,
            ClassGuid = classGuid,
            EducationCycle = educationCycle,
            DateSince = configuration.DateSince,
            DateUntil = configuration.DateUntil,
            EducationCycleStepInstances = new List<EducationCycleStepInstance>()
        };
        await Context.AddAsync(instance);

        return new ResponseWithStatus<Guid>(instance.Guid);
    }
    public async Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, IEnumerable<EducationCycleConfigurationStageCommand> stageCommands)
    {
        foreach (var command in stageCommands)
        {
            var resp = await ConfigureEducationCycleStageInstanceForEducationCycleInstance(educationCycleInstanceGuid, command);
            if (!resp.Status) return new StatusResponse(resp.StatusCode);
        }
        return new StatusResponse(true);
    }
    public async Task<ResponseWithStatus<Guid>> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, EducationCycleConfigurationStageCommand stageCommand)
    {
        var instance = await Context
            .EducationCycleInstances!
            .Include(e => e.EducationCycleStepInstances)
            .FirstOrDefaultAsync(e => e.Guid == educationCycleInstanceGuid);
        if (instance is null) return new ResponseWithStatus<Guid>(404);
        var educationCycleStep = await Context.EducationCycleSteps!.FirstOrDefaultAsync(e => e.Guid == stageCommand.EducationCycleStageGuid);
        if (educationCycleStep is null) return new ResponseWithStatus<Guid>(404);
        EducationCycleStepInstance stepInstance = new EducationCycleStepInstance()
        {
            EducationCycleInstanceGuid = instance.Guid,
            EducationCycleStepGuid = educationCycleStep.Guid,
            DateSince = stageCommand.DateSince,
            DateUntil = stageCommand.DateUntil,
        };
        Context.Add(stepInstance);
        await ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(instance.Guid, stageCommand.Subjects);

        return new ResponseWithStatus<Guid>(stepInstance.Guid);
    }
    public async Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, IEnumerable<EducationCycleConfigurationSubjectCommand> subjectCommands)
    {
        foreach (var command in subjectCommands)
        {
            var resp = await ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(educationCycleStepInstanceGuid, command);
            if (!resp.Status) return new StatusResponse(resp.StatusCode);
        }
        return new StatusResponse(true);
    }
    public async Task<ResponseWithStatus<Guid>> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, EducationCycleConfigurationSubjectCommand subjectCommand)
    {
        var instance = await Context
            .EducationCycleStepInstances!
            .Include(e => e.EducationCycleStepSubjectInstances)
            .FirstOrDefaultAsync(e => e.Guid == educationCycleStepInstanceGuid);
        if (instance is null) return new ResponseWithStatus<Guid>(404);
        var educationCycleStepSubject = await Context.EducationCycleStepSubjects!.FirstOrDefaultAsync(e => e.Guid == subjectCommand.EducationCycleStageSubjectGuid);
        if (educationCycleStepSubject is null) return new ResponseWithStatus<Guid>(404);
        var subjectInstance = new EducationCycleStepSubjectInstance()
        {
            EducationCycleStepInstanceGuid = instance.Guid,
            EducationCycleStepSubjectGuid = educationCycleStepSubject.Guid,
            AssignedTeacherGuid = subjectCommand.TeacherGuid,
        };

        Context.Add(subjectInstance);

        return new ResponseWithStatus<Guid>(subjectInstance.Guid);
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
}
