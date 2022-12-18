using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries : IFoundationClassesQueries
{
    public async Task<ResponseWithStatus<ClassDto, bool>> GetClassByGuid(Guid guid)
    {
        var resp = await Repository.GetClassByGuid(guid);
        if (resp is null) return new ResponseWithStatus<ClassDto, bool>(404, "Class does not exist");
        return new ResponseWithStatus<ClassDto, bool>(resp, true);
    }
    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesForPerson(Guid personGuid, int page)
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesForPerson(personGuid, pager);
        if (resp is null) return new ResponseWithStatus<IPagedList<ClassDto>>(404);
        return new ResponseWithStatus<IPagedList<ClassDto>>(resp, true);
    }
    public async Task<ResponseWithStatus<IPagedList<ClassDto>>> GetClassesInSchool(Guid schoolGuid, int page, string? query = "")
    {
        var pager = new Pager(page);
        var resp = await Repository.GetClassesInSchool(schoolGuid, pager, query);
        if (resp is null) return new ResponseWithStatus<IPagedList<ClassDto>>(404);
        return new ResponseWithStatus<IPagedList<ClassDto>>(resp, true);
    }

    public async Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetCurrentEducationCycleStepInstance(Guid classGuid)
    {
        var allStepsForClass = (await Repository.GetAllEducationCycleStepInstancesForClass(classGuid)).OrderBy(e => e.Order);
        var current = allStepsForClass.FirstOrDefault(e => !e.Finished && e.Started);
        if (current is null) current = allStepsForClass.FirstOrDefault(e => !e.Finished && !e.Started);
        if (current is null) return new ResponseWithStatus<EducationCycleStepInstanceDto>(404);
        return new ResponseWithStatus<EducationCycleStepInstanceDto>(current);
    }

    public async Task<ResponseWithStatus<Guid?>> GetCurrentEducationCycleStepInstanceGuid(Guid classGuid)
    {
        var instance = await GetCurrentEducationCycleStepInstance(classGuid);
        if (!instance.Status) return new ResponseWithStatus<Guid?>(instance.StatusCode);
        return new ResponseWithStatus<Guid?>(instance.Response!.Guid);
    }
    public async Task<ResponseWithStatus<EducationCyclesForClassDto>> GetEducationCyclesByClassGuid(Guid classGuid)
    {
        var activeEducationCycleGuid = await Repository.GetActiveEducationCycleGuidByClassGuid(classGuid);
        List<Guid> educationCyclesInstancesGuids = new();
        if (activeEducationCycleGuid is not null && activeEducationCycleGuid.HasValue)
        {
            var activeEducationCycleInstance = await Repository.GetEducationCycleInstanceForClass(classGuid, activeEducationCycleGuid.Value);
            if (activeEducationCycleInstance is not null && activeEducationCycleInstance.HasValue)
                educationCyclesInstancesGuids.Add(activeEducationCycleInstance.Value);
        }
        var nonActiveEducationCycles = await Repository.GetEducationCycleInstancesGuidsByClassGuid(classGuid, new Pager(0));
        educationCyclesInstancesGuids.AddRange(nonActiveEducationCycles);

        var educationCycles = await Repository.GetEducationCycleInstancesByGuids(educationCyclesInstancesGuids.Distinct());

        var educationCycleStepsInstances = await Repository.GetEducationCycleStepInstancesByEducationCycleInstancesGuids(educationCyclesInstancesGuids.Distinct());

        var educationCycleStepsSubjectsInstances =
            await Repository.GetEducationCycleStepSubjectInstancesByEducationCycleStepInstancesGuids(educationCycleStepsInstances.Select(e => e.Guid).Distinct());

        EducationCyclesForClassDto educationCyclesForClassDto = new();
        if (activeEducationCycleGuid.HasValue)
            educationCyclesForClassDto.ActiveEducationCycle = (await GetEducationCycle(activeEducationCycleGuid.Value)).Response;
        educationCyclesForClassDto.ActiveEducationCycleInstance = educationCycles.FirstOrDefault(e => e.EducationCycleGuid == activeEducationCycleGuid);
        educationCyclesForClassDto.EducationCyclesInstances = educationCycles.Select(e =>
        {
            e.EducationCycleStepInstances = educationCycleStepsInstances.Where(si => si.EducationCycleInstanceGuid == e.Guid).Select(ecsi =>
            {
                ecsi.EducationCycleStepSubjectInstances = educationCycleStepsSubjectsInstances.Where(ecssi => ecssi.EducationCycleStepInstanceGuid == ecsi.Guid).ToList();
                return ecsi;
            }).ToList();
            return e;
        }).ToList();


        var previousStepInstanceGuid = await GetPreviousEducationCycleStepInstance(classGuid);
        var nextStepInstanceGuid = await GetNextEducationCycleStepInstance(classGuid);
        var currentStepInstanceGuid = await GetCurrentEducationCycleStepInstance(classGuid);
        educationCyclesForClassDto.PreviousStepInstance = previousStepInstanceGuid.Response;
        educationCyclesForClassDto.NextStepInstance = nextStepInstanceGuid.Response;
        educationCyclesForClassDto.CurrentStepInstance = currentStepInstanceGuid.Response;


        return new ResponseWithStatus<EducationCyclesForClassDto>(educationCyclesForClassDto);
    }

    public async Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetNextEducationCycleStepInstance(Guid classGuid)
    {
        var allStepsForClass = (await Repository.GetAllEducationCycleStepInstancesForClass(classGuid)).OrderBy(e => e.Order);
        var currentCycleStepGuid = await GetCurrentEducationCycleStepInstanceGuid(classGuid);
        if (!currentCycleStepGuid.Status) return new ResponseWithStatus<EducationCycleStepInstanceDto>(currentCycleStepGuid.StatusCode);
        var current = allStepsForClass.FirstOrDefault(e => e.Guid == currentCycleStepGuid.Response);
        if (current is null) return new ResponseWithStatus<EducationCycleStepInstanceDto>(404);
        var instance = allStepsForClass.FirstOrDefault(e => e.EducationCycleInstanceGuid == current.EducationCycleInstanceGuid && e.Order == current.Order + 1);
        if (instance is null) return new ResponseWithStatus<EducationCycleStepInstanceDto>(404);
        return new ResponseWithStatus<EducationCycleStepInstanceDto>(instance);
    }

    public async Task<ResponseWithStatus<Guid?>> GetNextEducationCycleStepInstanceGuid(Guid classGuid)
    {
        var instance = await GetNextEducationCycleStepInstance(classGuid);
        if (!instance.Status) return new ResponseWithStatus<Guid?>(instance.StatusCode);
        return new ResponseWithStatus<Guid?>(instance.Response!.Guid);
    }

    public async Task<ResponseWithStatus<EducationCycleStepInstanceDto>> GetPreviousEducationCycleStepInstance(Guid classGuid)
    {
        var allStepsForClass = (await Repository.GetAllEducationCycleStepInstancesForClass(classGuid)).OrderBy(e => e.Order);
        var currentCycleStepGuid = await GetCurrentEducationCycleStepInstanceGuid(classGuid);
        if (!currentCycleStepGuid.Status) return new ResponseWithStatus<EducationCycleStepInstanceDto>(currentCycleStepGuid.StatusCode);
        var current = allStepsForClass.FirstOrDefault(e => e.Guid == currentCycleStepGuid.Response);
        if (current is null) return new ResponseWithStatus<EducationCycleStepInstanceDto>(404);
        var instance = allStepsForClass.FirstOrDefault(e => e.EducationCycleInstanceGuid == current.EducationCycleInstanceGuid && e.Order == current.Order - 1);
        if (instance is null) return new ResponseWithStatus<EducationCycleStepInstanceDto>(404);
        return new ResponseWithStatus<EducationCycleStepInstanceDto>(instance);
    }

    public async Task<ResponseWithStatus<Guid?>> GetPreviousEducationCycleStepInstanceGuid(Guid classGuid)
    {
        var instance = await GetPreviousEducationCycleStepInstance(classGuid);
        if (!instance.Status) return new ResponseWithStatus<Guid?>(instance.StatusCode);
        return new ResponseWithStatus<Guid?>(instance.Response!.Guid);
    }
    public async Task<ResponseWithStatus<bool>> IsClassOwner(Guid classGuid, Guid personGuid)
    {
        var resp = await Repository.IsClassOwner(classGuid, personGuid);
        return new ResponseWithStatus<bool>(resp, true);
    }
}
