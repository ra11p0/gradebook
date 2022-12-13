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
            var _class = await Context.Classes!.FirstOrDefaultAsync(e => e.Guid == classGuid);
            if (_class is null) continue;
            _class.ActiveEducationCycle = null;
        }
        return new StatusResponse(200);
    }
    public async Task<StatusResponse> ConfigureEducationCycleForClass(Guid classGuid, Guid creatorGuid, EducationCycleConfigurationCommand configuration)
    {
        EducationCycleInstance instance = new()
        {
            CreatorGuid = creatorGuid,
            ClassGuid = classGuid,
            EducationCycleGuid = configuration.EducationCycleGuid,
            DateSince = configuration.DateSince,
            DateUntil = configuration.DateUntil,
            EducationCycleStepInstances = new List<EducationCycleStepInstance>()
        };
        await Context.AddAsync(instance);
        await ConfigureEducationCycleStageInstanceForEducationCycleInstance(instance.Guid, configuration.Stages);

        return new StatusResponse(true);
    }
    public async Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, IEnumerable<EducationCycleConfigurationStageCommand> stageCommands)
    {
        foreach (var command in stageCommands)
        {
            var resp = await ConfigureEducationCycleStageInstanceForEducationCycleInstance(educationCycleInstanceGuid, command);
            if (!resp.Status) return resp;
        }
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> ConfigureEducationCycleStageInstanceForEducationCycleInstance(Guid educationCycleInstanceGuid, EducationCycleConfigurationStageCommand stageCommand)
    {
        var instance = await Context
            .EducationCycleInstances!
            .Include(e => e.EducationCycleStepInstances)
            .FirstOrDefaultAsync(e => e.Guid == educationCycleInstanceGuid);
        if (instance is null) return new StatusResponse(404);
        EducationCycleStepInstance stepInstance = new EducationCycleStepInstance()
        {
            EducationCycleStepGuid = stageCommand.EducationCycleStageGuid,
            DateSince = stageCommand.DateSince,
            DateUntil = stageCommand.DateUntil,
        };
        instance!.EducationCycleStepInstances!.Add(stepInstance);
        await ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(instance.Guid, stageCommand.Subjects);

        return new StatusResponse(true);
    }
    public async Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, IEnumerable<EducationCycleConfigurationSubjectCommand> subjectCommands)
    {
        foreach (var command in subjectCommands)
        {
            var resp = await ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(educationCycleStepInstanceGuid, command);
            if (!resp.Status) return resp;
        }
        return new StatusResponse(true);
    }
    public async Task<StatusResponse> ConfigureEducationCycleSubjectInstanceForEducationCycleStepInstance(Guid educationCycleStepInstanceGuid, EducationCycleConfigurationSubjectCommand subjectCommand)
    {
        var instance = await Context
            .EducationCycleStepInstances!
            .Include(e => e.EducationCycleStepSubjectInstances)
            .FirstOrDefaultAsync(e => e.Guid == educationCycleStepInstanceGuid);
        if (instance is null) return new StatusResponse(404);
        var subjectInstance = new EducationCycleStepSubjectInstance()
        {
            EducationCycleStepSubjectGuid = subjectCommand.EducationCycleStageSubjectGuid,
            AssignedTeacherGuid = subjectCommand.TeacherGuid,
        };

        instance!.EducationCycleStepSubjectInstances!.Add(subjectInstance);

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
}
