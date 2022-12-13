namespace Gradebook.Foundation.Common.Foundation.Commands.Definitions;

public class EducationCycleConfigurationCommand : Validatable
{
    public Guid EducationCycleGuid { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public List<EducationCycleConfigurationStageCommand> Stages { get; set; } = new();

    protected override StatusResponse Validate()
    {
        if (!Stages.Any()) return new StatusResponse(false);
        if (Stages.Any(e => !e.IsValid)) return new StatusResponse(false);
        if (Stages.Min(e => e.DateSince) < DateSince) return new StatusResponse(false);
        if (Stages.Max(e => e.DateUntil) > DateUntil) return new StatusResponse(false);

        var stagesWithBothDates = Stages.Where((e) => e.DateSince.HasValue && e.DateUntil.HasValue).ToList();
        bool overlaps = false;
        stagesWithBothDates.ForEach((element) =>
        {
            stagesWithBothDates.ForEach((element2) =>
            {
                if (element.Order == element2.Order) return;
                if (datesOverlap(element.DateSince!.Value, element.DateUntil!.Value, element2.DateSince!.Value, element2.DateUntil!.Value))
                    overlaps = true;
            });
        });
        if (overlaps) return new StatusResponse(false);

        DateTime? dateThatShouldNotBeLater = null;
        bool wrongDateOrder = false;

        stagesWithBothDates
            .OrderBy(e => e.Order)
            .ToList()
            .ForEach((el) =>
            {
                if (dateThatShouldNotBeLater.HasValue && dateThatShouldNotBeLater.Value > el.DateSince!.Value)
                    wrongDateOrder = true;
                dateThatShouldNotBeLater = el.DateSince;
            });
        if (wrongDateOrder) return new StatusResponse(false);
        return new StatusResponse(true);
    }

    private bool datesOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
    {
        if (startDate1 > startDate2)
            (startDate1, endDate1, startDate2, endDate2) = (startDate2, endDate2, startDate1, endDate1);
        return endDate1 > startDate2;
    }
}
