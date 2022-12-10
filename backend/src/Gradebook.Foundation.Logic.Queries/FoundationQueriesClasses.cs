using Gradebook.Foundation.Common;
using Gradebook.Foundation.Common.Foundation.Queries.Definitions;

namespace Gradebook.Foundation.Logic.Queries;

public partial class FoundationQueries
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

        educationCyclesForClassDto.ActiveEducationCycleInstance = educationCycles.FirstOrDefault(e => e.Guid == activeEducationCycleGuid);
        educationCyclesForClassDto.EducationCycles = educationCycles.Select(e =>
        {
            e.EducationCycleStepInstances = educationCycleStepsInstances.Where(si => si.EducationCycleInstanceGuid == e.Guid).Select(ecsi =>
            {
                ecsi.EducationCycleStepSubjectInstances = educationCycleStepsSubjectsInstances.Where(ecssi => ecssi.EducationCycleStepInstanceGuid == ecsi.Guid).ToList();
                return ecsi;
            }).ToList();
            return e;
        }).ToList();

        return new ResponseWithStatus<EducationCyclesForClassDto>(educationCyclesForClassDto);
    }
}
