using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Commands.Definitions;
using Gradebook.Foundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Logic.Commands.Repositories;

public partial class FoundationCommandsRepository
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
            EducationCycleGuid = configuration.Guid,
            DateSince = configuration.DateSince,
            DateUntil = configuration.DateUntil,
            EducationCycleStepInstances = new List<EducationCycleStepInstance>()
        };

        foreach (var stage in configuration.Stages)
        {
            EducationCycleStepInstance stepInstance = new EducationCycleStepInstance()
            {
                EducationCycleStepGuid = stage.Guid,
                DateSince = stage.DateSince,
                DateUntil = stage.DateUntil,
                EducationCycleStepSubjectInstances = stage.Subjects.Select(sub =>
                {
                    var subjectInstance = new EducationCycleStepSubjectInstance()
                    {
                        EducationCycleStepSubjectGuid = sub.Guid,
                        AssignedTeacherGuid = sub.TeacherGuid,
                    };

                    return subjectInstance;
                }).ToList()
            };
            instance.EducationCycleStepInstances.Add(stepInstance);
        }
        await Context.AddAsync(instance);
        return new StatusResponse(true);
    }
}
