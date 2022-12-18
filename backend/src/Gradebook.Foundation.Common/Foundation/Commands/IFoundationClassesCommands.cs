namespace Gradebook.Foundation.Common.Foundation.Commands;

public interface IFoundationClassesCommands
{
    Task<StatusResponse> StartEducationCycleStepInstance(Guid classGuid);
    Task<StatusResponse> StopEducationCycleStepInstance(Guid classGuid);
    Task<StatusResponse> ForwardEducationCycleStepInstance(Guid classGuid);
}
